import { Component } from '@angular/core';
import { Platform } from 'ionic-angular';
import { StatusBar } from '@ionic-native/status-bar';
import { SplashScreen } from '@ionic-native/splash-screen';
import { DatabaseProvider } from '../providers/database/database';
import { TabsPage } from '../pages/tabs/tabs';
import { LoginPage } from '../pages/login/login';
import { ServiceLogin } from '../providers/servicelogin/servicelogin';
import { Usuario } from '../providers/servicelogin/servicelogin';

@Component({
  templateUrl: 'app.html'
})
export class MyApp {
  rootPage:any = LoginPage;
  $usuario : Usuario;
  
  constructor(platform: Platform, statusBar: StatusBar, splashScreen: SplashScreen, dbProvider: DatabaseProvider, servicelogin:ServiceLogin) {
    platform.ready().then(() => {
      // Okay, so the platform is ready and our plugins are available.
      // Here you can do any higher level native things you might need.
      statusBar.styleDefault();
 
      //Criando o banco de dados
      dbProvider.createDatabase()
        .then(() => {
          // fechando a SplashScreen somente quando o banco for criado
          this.openHomePage(splashScreen);
        })
        .catch(() => {
          // ou se houver erro na criação do banco
          this.openHomePage(splashScreen);
        });
        console.log("carregando usuario...");
        this.$usuario = servicelogin.Usuarioget(); 
        console.log(this.$usuario.idusuario);

    });
  }
  private openHomePage(splashScreen: SplashScreen) {

    if (this.$usuario.idusuario == 0 ) {
      this.rootPage = LoginPage;
    }
    else {
      this.rootPage = TabsPage;
    }
    splashScreen.hide();
  }

}
