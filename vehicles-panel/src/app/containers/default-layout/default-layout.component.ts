import { Component, ViewChild } from '@angular/core';
import { navItems } from '../../_nav';
import { Router } from '@angular/router';
import { AuthenticationService } from '../../services/authentication.service';
import { langHelper } from 'src/app/services/helpers-and-utilities/language-helper';
import { ConnectionService } from 'ng-connection-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-dashboard',
  templateUrl: './default-layout.component.html',
  styleUrls: ['./default-layout.component.scss']
})
export class DefaultLayoutComponent {
  public sidebarMinimized = false;
  public navItems = navItems;
  langVar;
  currentLanguage;
  roleName: any;
  isLoading: boolean = true;
  //Authorization
  //Internet connection variables
  isConnected = true;
  noInternetConnection: boolean;

  //@ViewChild(ModalComponent) modalComponent: ModalComponent;
  constructor(private router: Router, private langHelper: langHelper,
    private AuthService: AuthenticationService,
    private connectionService: ConnectionService,  private toastr: ToastrService) {

    this.langVar = this.langHelper.initializeMode();
    this.currentLanguage = this.langHelper.currentLang;

    //handling layout rtl
    let html = document.getElementById("html");
    html.dir = this.langVar.dir;
    if (html.classList.length == 0 && this.langVar.dir == 'rtl') {
      // console.log(html.classList)
      html.classList.add(this.langVar.arabicClass);
    }

    this.connectionService.monitor().subscribe(isConnected => {
      this.isConnected = isConnected;
      if (this.isConnected) {
        this.noInternetConnection = false;
        this.DisplaySuccessToast(this.langVar.connection.connected);
      }
      else {
        this.noInternetConnection = true;
        this.DisplayErrorToast(this.langVar.connection.disconnected);
      }
    });
  }

  ngOnInit(): void {
    console.log(this.AuthService.getUserRole())
  }



  Logout() {
    this.AuthService.removeToken();
    this.router.navigate(["auth"]);
  }


  toggleMinimize(e) {
    this.sidebarMinimized = e;
  }

  switchlanguage() {
    this.langHelper.switchLanguage();
    location.reload()
  }

  //Toast
  DisplaySuccessToast(message: string) {
    this.toastr.clear();
    this.toastr.success(message, '', {
      disableTimeOut: false,
      positionClass: 'toast-top-center',
    });
  }

  DisplayErrorToast(message: string) {
    this.toastr.error(message, '', {
      disableTimeOut: true,
      positionClass: 'toast-top-center',
    });
  }

}
