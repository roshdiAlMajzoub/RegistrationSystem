import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  public registerForm = this.formBuilder.group(
    {
      email:['', Validators.required],
      password:['', Validators.required],
      confirmPassword:['', Validators.required],
    }
  )

  constructor(private formBuilder:FormBuilder, private userService:UserService, private router:Router) { }

  errorMessage:string ="";

  ngOnInit(): void {
  }

  onSubmit()
  {
    console.log("On submit");
    let email= this.registerForm.controls["email"].value;
    let password= this.registerForm.controls["password"].value;
    let confirmPassword= this.registerForm.controls["confirmPassword"].value;
    this.userService.register(email, password, confirmPassword).subscribe((data:any)=>{
      if (data.responseCode==1)
      {
        console.log("response", data);
        this.router.navigate(["/login"]);
      }else{
        this.errorMessage = data.responseMessage;
      }
    
    }, err => {
      console.log("error",err);
      
    } )
  }

}
