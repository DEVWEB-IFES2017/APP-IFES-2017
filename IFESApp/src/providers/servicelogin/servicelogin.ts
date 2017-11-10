import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import { SQLiteObject } from '@ionic-native/sqlite';
import { DatabaseProvider } from '../database/database';

@Injectable()
export class ServiceLogin {
  private API_URL = 'http://ifes.azurewebsites.net';
  model: Usuario;
  constructor(private dbProvider: DatabaseProvider,public http: Http) 
  { 
    this.model = new Usuario();
  }
 
    login(email: string, senha: string) {      
    return new Promise((resolve, reject) => {
      var Usuario = {email: email, senha: senha};
      console.log("Inicio Post");

      let headers = new Headers({ 'Content-Type': 'application/json' });
      let options = new RequestOptions({ headers: headers });

      this.http.post(this.API_URL + '/api/usuariosapi', Usuario,options)
        .subscribe((result: any) => {
          resolve(result.json());
          console.log("Fim Post");

          this.model = result.json();
          
          console.log(this.model);

          if (this.model.email == Usuario.email){
            this.insert(this.model) 
          }
        },
        (error) => {
          reject(error.json());
        });
    });
  } 

  public insert(usuario: Usuario) {
    console.log("Inserindo no banco...");
    console.log(usuario);
    
    return this.dbProvider.getDB().then((db: SQLiteObject) => {
        let sql = 'insert into usuario (id, nome, email) values (?, ?, ?)';
        let data = [usuario.idusuario, usuario.nome, usuario.email];
        console.log("Inseriu...");
 
        return db.executeSql(sql, data).catch((e) => console.error(e));
    }).catch((e) => console.error(e));
  }  

  public Usuarioget():Usuario {
    let usuario = new Usuario();          
    usuario.idusuario = 0;

    this.dbProvider.getDB().then((db: SQLiteObject) => {
      //let data = [];
      let sql = 'select * from usuario where id not is null';

      return db.executeSql(sql, null).then((data: any) => {
          if (data.rows.length > 0) {
              let item = data.rows.item(0);
              usuario.idusuario = item.id;
              usuario.nome = item.nome;
              usuario.email = item.email;
              return usuario;
          }
          return usuario;
      }).catch((e) => console.error(e));
  }).catch((e) => console.error(e));
    return usuario;
  }
}

export class Usuario {
  idusuario: number;
  nome:string;
  email: string;
  senha: string;
}