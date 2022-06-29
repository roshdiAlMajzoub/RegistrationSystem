import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { UserService } from '../service/user.service';
import { Router, ROUTES } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginForm = this.formBuilder.group(
    {
      email:['', Validators.required],
      password:['', Validators.required]
    }
  )
  

  constructor(private formBuilder:FormBuilder, private userService:UserService, private router:Router) { }


  errorMessage:string ="";

  ngOnInit(): void {
    localStorage.setItem('User-Auth', "not authorized"); 
  }

// userHello:string="hey";

  onSubmit()
  {
    console.log("On submit")
    let email= this.loginForm.controls["email"].value;
    let password= this.loginForm.controls["password"].value;
    return this.userService.login(email, password).subscribe((data:any)=>{
      console.log("response", data);
      if (data.responseCode==1)
      {
        localStorage.setItem('User-Email', data.responseMessage); 
        localStorage.setItem('User-Auth', "authorized"); 
        this.router.navigate(["/welcome"]);
      }else {
        localStorage.setItem('User-Auth', "not authorized"); 
        this.errorMessage = data.responseMessage;
      }
    
    }, err => {
      console.log("error",err);
      
    } )
  }

}
