import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  constructor(private authService:AuthService){}
  status="";
  ngOnInit(): void {
    this.authService.userManager.getUser().then((user)=>
    {
      if(user)
      {
        console.log(user);
        this.status="Hoşgeldiniz";
      }
      else
      {
        this.status="Giriş Yapılmadı";
      }
    });
  }
  
  login():void{
    this.authService.userManager.signinRedirect();

  }

  logout():void{
    this.authService.userManager.signoutRedirect();
  }
}
