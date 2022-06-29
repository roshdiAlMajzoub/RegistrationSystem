import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginComponent } from '../login/login.component';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {

  constructor(private router:Router) { }
// userHello= this.loginComponent.userHello;

  isAuthorized = localStorage.getItem("User-Auth");
 
  email = localStorage.getItem("User-Email");

  ngOnInit(): void {
    if (this.isAuthorized=="not authorized" || this.isAuthorized=="")
      {
        this.router.navigate(["/login"]);
      }
    
  }

}
