import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private readonly baseURL:string = "https://localhost:7073/api/User/"

  constructor(private httpClient:HttpClient) { }

  public login(email:any, password:any)
  {
    const body={
      email: email,
      password: password
    }
    return this.httpClient.post(this.baseURL+"login", body);
  }

  public register(email:any, password:any, confirmPassword:any)
  {
    const body={
      email: email,
      password: password,
      confirmPassword: confirmPassword,
    }
    
    return this.httpClient.post(this.baseURL+"register", body);
  }

  public getUsers(){
    return this.httpClient.get(this.baseURL+"users");
  }

  public verify(id:number)
  {
    const body={
      id: id,
    }
    return this.httpClient.post(this.baseURL + "verify", body);
  }

  public decline(id:number)
  {
    const body={
      id: id,
    }
    return this.httpClient.post(this.baseURL + "decline", body);
  }


}
