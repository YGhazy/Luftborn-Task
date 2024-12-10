import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from "@auth0/angular-jwt";
import { Observable } from 'rxjs';
import { ApiResponse } from '../models/auth-models/api-response';
import { ApplicationUser } from '../models/auth-models/ApplicationUser';
import { LoginModel } from '../models/auth-models/LoginModel';
import { RegisterModel } from '../models/auth-models/RegisterModel';

import { API_CONSTANTS } from './common/api-constants';
import { BaseService } from './common/base-service';


@Injectable({
    providedIn: 'root'
})
export class VehicleService extends BaseService {
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }
    GetAllVehicles(): Observable<ApiResponse> {
      return this.get(API_CONSTANTS.GetAllVehicle);
    }



}
