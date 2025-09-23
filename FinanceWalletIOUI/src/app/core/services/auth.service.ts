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
  
  clearExpiredToken(): void {
    const token = localStorage.getItem(Constant.KEY_NAME.TOKEN_KEY);
    if (!token) return;

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const expiry = payload.exp * 1000; // `exp` in seconds → ms
      if (Date.now() >= expiry) {
        localStorage.removeItem(Constant.KEY_NAME.TOKEN_KEY);
      }
    } catch {
      // If JWT is corrupted, also remove it
      localStorage.removeItem(Constant.KEY_NAME.TOKEN_KEY);
    }
  }
}