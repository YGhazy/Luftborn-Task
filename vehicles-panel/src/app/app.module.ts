import { CommonModule } from '@angular/common';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
    AppAsideModule,
    AppBreadcrumbModule,
    AppFooterModule, AppHeaderModule,
    AppSidebarModule
} from '@coreui/angular';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { DataTablesModule } from 'angular-datatables';
import { ChartsModule } from 'ng2-charts';
// Import 3rd party components
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ModalModule } from 'ngx-bootstrap/modal';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { ToastrModule } from 'ngx-toastr';
import { DropdownModule } from 'primeng/dropdown';
import { TableModule } from 'primeng/table';
import { AppComponent } from './app.component';
// Import routing module
import { AppRoutingModule } from './app.routing';
import { DefaultLayoutComponent } from './containers';
import { TokenInterceptor } from './services/http-services/token-interceptor';
import { P404Component } from './views/error/404.component';
import { P500Component } from './views/error/500.component';
import { RECAPTCHA_SETTINGS, RecaptchaSettings } from "ng-recaptcha";
import { RecaptchaModule, RecaptchaFormsModule } from "ng-recaptcha";
import {ConnectionServiceModule} from 'ng-connection-service';
import { SharedModule } from './views/Shared/shared.module';
import { VehiclesComponent } from './views/vehicles/vehicles.component';
import { ButtonModule } from 'primeng/button';
import { CustomerComponent } from './views/customer/customer.component';

const APP_CONTAINERS = [
  DefaultLayoutComponent,

];

@NgModule({
  imports: [
    NgbModule,
    HttpModule,
    TableModule,
    FormsModule,
    CommonModule,
    ButtonModule,
    ChartsModule,
    SharedModule,
    BrowserModule,
    AppAsideModule,
    DropdownModule,
    AppFooterModule,
    AppHeaderModule,
    HttpClientModule,
    DataTablesModule,
    AppSidebarModule,
    AppRoutingModule,
    // RecaptchaV3Module, //Google Recaptcha import
    RecaptchaModule,
    RecaptchaFormsModule,
    ReactiveFormsModule,
    PerfectScrollbarModule,
    BrowserAnimationsModule,
    ConnectionServiceModule,  
    ToastrModule.forRoot(),
    TabsModule.forRoot(),
    ModalModule.forRoot(),
    PopoverModule.forRoot(),
    BsDropdownModule.forRoot(),
    AppBreadcrumbModule.forRoot(),
    
  ],
  declarations: [
    AppComponent,
    P404Component,
    P500Component,
    ...APP_CONTAINERS,
    VehiclesComponent,
    CustomerComponent,
  ],
  providers: [
    { 
      provide: RECAPTCHA_SETTINGS,
      useValue: { siteKey: "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI" } as RecaptchaSettings, //re-captcha test_key
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    }
  ], 
 
  bootstrap: [ AppComponent ]
})
export class AppModule { }
