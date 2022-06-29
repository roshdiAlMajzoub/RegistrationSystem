import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../service/user.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  constructor(private userService:UserService, private router:Router) { }

  usersList:any=[];
  userHello:boolean=false;
  ngOnInit(): void {
    
    if (localStorage.getItem("User-Auth") == "not authorized"){
      this.router.navigate(["/login"]);
      
    }
    else if (localStorage.getItem("User-Email") == "admin@admin.com"){
      this.userHello= true;
    }
      
    this.refreshUsersList();
  }

  


  refreshUsersList()
  {
    this.userService.getUsers().subscribe((data:any) =>{
      console.log(data);
      this.usersList = data.dataSet;
    })
  }

  // Verifieying and declining  user 

  verify(id:number)
  {
    console.log(id);
    this.userService.verify(id).subscribe((data:any) =>{
      console.log(data);
      window.location.reload();
    }, err =>{
      console.log("error" , err);
      window.location.reload();
    })
  }

  decline(id:number)
  {
    console.log(id);
    this.userService.decline(id).subscribe((data:any) =>{
      console.log(data);
      window.location.reload();
    }, err =>{
      console.log("error" , err);
      window.location.reload();
    })
  }

  logout(){
    localStorage.setItem("User-Auth", "");
    this.router.navigate(["/login"])
  }

}
