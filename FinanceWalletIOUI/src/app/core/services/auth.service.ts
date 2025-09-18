import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { environment } from "../../../environments/environment.development";
import { Constant } from "../constants/constant";
import { LoginDto, RegisterDto } from "../models/auth.model";


@Injectable({
  providedIn: 'root',
})

export class AuthService {
  constructor(private http: HttpClient) { }

  private baseUrl = environment.apiUrl + Constant.MODULE_NAME.AUTH;

  SignIn(dto: LoginDto) {
    const requestUrl = this.baseUrl + Constant.USER_REQUEST.LOGIN;
    return this.http.post(requestUrl, dto);
  }

  SignUp(dto: RegisterDto) {
    const requestUrl = this.baseUrl + Constant.USER_REQUEST.REGISTER;
    return this.http.post(requestUrl, dto);
  }

  SignOut() {
    const requestUrl = this.baseUrl + Constant.USER_REQUEST.LOGOUT;
    return this.http.post(requestUrl, null);
  }
}