import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { AuthenticationRoutingModule } from './authentication-routing.module';
import { ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ToastModule } from 'primeng/toast';
import { BlockUIModule } from 'primeng/blockui';
import { SharedModule } from '../Shared/shared.module';


@NgModule({
  declarations: [LoginComponent],
    imports: [
      CommonModule,
      ReactiveFormsModule,
      AuthenticationRoutingModule,
      NgbModule,
      ToastModule,
      BlockUIModule,
      SharedModule,
    ]
})
export class AuthenticationModule { }
