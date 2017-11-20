import { Component } from '@angular/core';
import { IonicPage, NavController, NavParams , ToastController } from 'ionic-angular';
import { ServiceLogin } from '../../providers/servicelogin/servicelogin';
import { TabsPage } from '../../pages/tabs/tabs';
/**
 * Generated class for the LoginPage page.
 *
 * See https://ionicframework.com/docs/components/#navigation for more info on
 * Ionic pages and navigation.
 */

@IonicPage()
@Component({
  selector: 'page-login',
  templateUrl: 'login.html',
})

export class LoginPage {
  model: Usuario;
  
  constructor(public navCtrl: NavController, public navParams: NavParams, private toast: ToastController, private servicelogin: ServiceLogin) {
    this.model = new Usuario();
  }

  login() {
    console.log("inicio login");
    this.servicelogin.login(this.model.email, this.model.senha).then((result: any) => 
      {
        
        this.toast.create({ message: 'Seja bem vindo ' + result.nome, position: 'botton', duration: 3000 }).present();
        //this.toast.create({ message: 'Usuário logado com sucesso. E-Mail: ' + result.email, position: 'botton', duration: 3000 }).present();
        //Salvar o token no Ionic Storage para usar em futuras requisições.
        //Redirecionar o usuario para outra tela usando o navCtrl
        //this.navCtrl.pop();
        this.navCtrl.setRoot(TabsPage);
      }).catch((error: any) => 
      {
        this.toast.create({ message: 'Falha ao efetuar login, Usuário e Senha inválidos!!', position: 'botton', duration: 3000 }).present();
      });
  }
  ionViewDidLoad() {
    console.log('ionViewDidLoad LoginPage');
  }

}

export class Usuario {
  email: string;
  senha: string;
  nome: string;
}
