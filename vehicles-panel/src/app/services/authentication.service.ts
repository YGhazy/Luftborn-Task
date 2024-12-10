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
export class AuthenticationService extends BaseService {

    userProfile: ApplicationUser;

    constructor(private httpClient: HttpClient) {
        super(httpClient);
    }

    jwtHelper = new JwtHelperService();

    setToken(token: string) {
        localStorage.setItem("access_token", token);
    }

  setSessionToken(token: string) {
    sessionStorage.setItem("access_token", token);
  }

  getToken(): string {
    const token = localStorage.getItem("access_token");
    if (token) {
      return token;
    }
    else {
      const token = sessionStorage.getItem("access_token");
      if (token) {
        return token;
      }
    }
  }

  removeToken(): void {
    localStorage.removeItem("access_token");
    sessionStorage.removeItem("access_token");
  }

    getUserRole(): string {
        const token = this.getToken();
        if (token) {
            const { role } = this.jwtHelper.decodeToken(token);
            return role;
        }
    }

    isAuthenticated(): boolean {
        try {
            const token = this.getToken();
            if (token && !this.jwtHelper.isTokenExpired(token)) {
                return true;
            }
            return false;
        }
        catch {
            return false;
        }
    }

    isInRole(roleName: string): boolean {
        const token = this.getToken();
        if (token) {
            let roles: string[];
            const { role } = this.jwtHelper.decodeToken(token);
            roles = role;
            const res = roles.includes(roleName);
            if (res) {
                return true;
            }
        }
        return false;
    }


    login(model: LoginModel): Observable<ApiResponse> {
        return this.post(API_CONSTANTS.Login, model);
    }

    logout() {
        this.removeToken();
    }

}
