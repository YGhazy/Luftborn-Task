import { Injectable } from '@angular/core';
import { FormControl, Validators, FormBuilder } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})

export class FormBuilderHelper {

  controllers;
  emailValidationPattern = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  constructor(private formBuilder: FormBuilder) {

    this.controllers = {
      name: [Validators.required, Validators.min(1), Validators.max(50), Validators.pattern("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]*$")], //arabic & english letters
      fullName: [Validators.required, Validators.min(1), Validators.max(50), Validators.pattern("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]*$")], //arabic & english letters
      mobileNumber: [Validators.required, Validators.pattern("^([0]{1}?[1]{1}?[0-2-5]{1}?[0-9]{8})$")],
      email: [Validators.required, Validators.pattern(this.emailValidationPattern)],
      calendar: [Validators.required],
      selectedHeader: [Validators.required],
      age: [Validators.required],
      level: [Validators.required],
      Answer: [Validators.required],
      Question: [Validators.required],
      requiredTitle: [""],
      isSubjectOrParty: [""],
      requireddescription: [""],
      approvingParty: [""],
      verifiyingCommitee: [""],
      openingCommitee: [""],
      Party: [""],
      selected_GD_or_Quest_or_Criteria: [""],
      type: [Validators.required],
      note: [Validators.required],
      title: [Validators.required],
      option: [Validators.required],
      file: [Validators.required],
      ruleName: [Validators.required],
      editName: [Validators.required, Validators.min(1), Validators.max(50), Validators.pattern("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]*$")], //arabic & english letters
      editDescription: [Validators.required],
      address: [Validators.required],
      image: [Validators.required],
      percentage: [Validators.required, Validators.maxLength(3)],
      userName: [Validators.required, Validators.min(1), Validators.max(50)], // Validators.pattern("^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z]+[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FFa-zA-Z-_]*$")// deprecated
      newPassword: [Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")],
      password: [Validators.required, Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")],
      description: [Validators.required],
      confirmPassword: [Validators.required, Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")],
      ENValidation: [Validators.required, Validators.pattern('[a-zA-Z ]*'), Validators.max(40)],
      ARValidation: [Validators.required, Validators.pattern('^[\u0600-\u06ff ]+$'), Validators.max(40)],
      reCaptcha: [Validators.required],
      //EndPoint validators
      initialEndPoint: [Validators.required],
      endPointURL: [Validators.required],
      //Sourcing Event details
      startDate: [Validators.required],
      endDate: [Validators.required],
      seType: [Validators.required],
      //Create general data detail
      attachment: [Validators.required],
      //Request committee session
      screenShareType: [Validators.required],
      otp: [Validators.required],
    }
  }

  CreateFormBuilder(controllerNames) {
    for (let entry of Object.entries(controllerNames)) {
      if (this.controllers[entry[0]][0] != '') {
        let x = [entry[1], this.controllers[entry[0]]]
        controllerNames[entry[0]] = x
      }
      else controllerNames[entry[0]] = [""]

    }
    return (this.formBuilder.group(controllerNames))
  }

  CustomizeFormbuilderValidator(controllerNames, customValidation) {
    for (let entry of Object.entries(controllerNames)) {

      let x = [entry[1], this.controllers[entry[0]]]
      entry[1] = x;
    }
    return (this.formBuilder.group(controllerNames, { validator: customValidation }))
  }

}
