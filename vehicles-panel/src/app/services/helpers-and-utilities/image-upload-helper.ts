import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Arabic } from './ar';
import { English } from './en';

@Injectable({
  providedIn: 'root'
})

export class ImageUploadHelper {

  constructor(private router: Router) {
  }

  ngOnInit() {
  }

  //encodes file type to base 64
  EncodeFileAsBase64(file){
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onloadend = () => {
      var base64data = reader.result;
      return base64data;
    }
    
  }
}
