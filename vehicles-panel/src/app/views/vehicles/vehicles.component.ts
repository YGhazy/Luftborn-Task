import { Component, OnInit, ViewChild } from '@angular/core';
import { Data, Router } from '@angular/router';
import { data } from 'jquery';
import { AuthenticationService } from '../../services/authentication.service';
import { VehicleService } from '../../services/Vehicle.service';
import * as signalR from '@microsoft/signalr';
import { environment } from '../../../environments/environment';
import { Howl, Howler } from 'howler';

@Component({
  selector: 'app-vehicles',
  templateUrl: './vehicles.component.html',
  styleUrls: ['./vehicles.component.scss']
})
export class VehiclesComponent implements OnInit {

  isLoading: boolean = true
  FilteredVehicles = []
  connection: any
  pingTone:any
  constructor(private VehicleService: VehicleService, private authService: AuthenticationService, private router: Router) { }

  ngOnInit(): void {

    if (this.authService.isInRole("Customer"))
      this.router.navigate(['/customer']);

    this.pingTone = new Howl({
      src: ['./assets/notification-tone.mp3']
    });

    this.GetAllVehicles()

    //start connection
    this.connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Information)
      .withUrl(environment.notificationsHub, { accessTokenFactory: () => this.authService.getToken() })
      .build();

    this.connection.start().then(function () {
    }).catch(function (err) {
      return console.error(err.toString());
    });

    this.connection.on("ping", () => {
      console.error("ping ");
      this.GetAllVehicles()
      this.pingTone.play();
    });

  }


  GetAllVehicles() {
    this.VehicleService.GetAllVehicles().subscribe(res => {
      if (res.succeeded) {

        this.FilteredVehicles = res.data;
        console.log(this.FilteredVehicles)
      }
    }, error => {
      this.isLoading = false
    });
  }

  Logout() {
    this.authService.logout()
    this.router.navigate(['/auth']);
  }


  checkDate(lastPing: Date) {
    if (new Date(lastPing).getFullYear()!==1) //avoid display initial date
      return true
    else
      return false
  }


}
