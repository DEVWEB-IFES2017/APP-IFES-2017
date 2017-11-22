import { Component } from '@angular/core';
import { Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { TabsPage } from '../pages/tabs/tabs';
import { LoginPage } from '../pages/login/login';
import { ServiceLogin } from '../providers/servicelogin/servicelogin';
import { Usuario } from '../providers/servicelogin/servicelogin';

@Component({
  templateUrl: 'app.html'
})
export class IFESApp {
  rootPage: any = LoginPage;
  usuario: Usuario;

  constructor(platform: Platform, statusBar: StatusBar, splashScreen: SplashScreen, servicelogin: ServiceLogin) {
    platform.ready().then(() => {
      this.usuario = new Usuario();
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      statusBar.styleDefault();
      this.usuario.idusuario = 0;
      //Criando o banco de dados

      console.log("carregando usuario...");

      servicelogin.Usuarioget().then(user => {
        this.usuario = user;
        console.log("cod. usuario:", this.usuario.idusuario);
        this.openHomePage(splashScreen);
      });

    });
  }
  private openHomePage(splashScreen: SplashScreen) {

    if (this.usuario.idusuario == 0) {
      this.rootPage = LoginPage;
    }
    else {
      this.rootPage = TabsPage;
    }
    splashScreen.hide();
  }

}
