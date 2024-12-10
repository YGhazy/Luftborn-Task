import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { ReCaptchaV3Service } from 'ng-recaptcha';
import { ErrorType } from 'src/app/enums/error-type';
import { ModalResponse } from 'src/app/enums/modal-response';
import { LoginModel } from 'src/app/models/auth-models/LoginModel';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { FormBuilderHelper } from 'src/app/services/helpers-and-utilities/formBuilderHelper';
import { langHelper } from 'src/app/services/helpers-and-utilities/language-helper';

@Component({
  selector: 'app-dashboard',
  templateUrl: 'login.component.html',
  styleUrls: ['login.component.scss']
})
export class LoginComponent {

  //Language and formGroup
  langVar;
  currentLang
  loginForm;
  //View/hide password
  inputType: string = "password";
  pView: string = "-slash";
  storedUserName: string;

  //Validation bools
  isAttemptingLogin: boolean = false;
  isLoading: boolean = false
  rememberME: boolean = false

  constructor(private router: Router, private formBuilderHelper: FormBuilderHelper, private langHelper: langHelper,
    private authenticationService: AuthenticationService) {
    this.langVar = this.langHelper.initializeMode();
    this.currentLang = this.langHelper.currentLang;
    this.loginForm = this.formBuilderHelper.CreateFormBuilder({ userName: '', password: '' });
  }

  ngOnInit(): void { }


  //User login
  Login() {
    this.isLoading = true
    const model: LoginModel =
    {
      UserName: this.loginForm.value.userName,
      password: this.loginForm.value.password
    }
    console.log(this.rememberME)
    this.authenticationService.login(model).subscribe(res => {
      if (res.succeeded) {
        console.log(res)
        if (this.rememberME) {
          //save token in localStorage
          this.authenticationService.setToken(res.data.token)
        }
        else {
          //save token in sessionStorage
          this.authenticationService.setSessionToken(res.data.token)
        }
        this.isLoading = false
        let role = this.authenticationService.getUserRole();
        if (role == "Administrator")
          this.router.navigate(['/vehicles']);
        else
          this.router.navigate(['/customer']);
      }
    }, error => {
      this.isLoading = false
    });
  }

  //View/hide password field
  viewPassword() {
    if (this.inputType == 'text') {
      this.inputType = 'password';
      this.pView = "-slash";
    }
    else if (this.inputType == 'password') {
      this.inputType = 'text';
      this.pView = "";
    }
  }

  //trigger change remember me logged in checkbox
  rememberMe(event) {
    event.target.checked ? this.rememberME = true : this.rememberME = false;
  }

  //Form validation controls

  get loginFormControls() {
    return this.loginForm.controls;
  }
}
