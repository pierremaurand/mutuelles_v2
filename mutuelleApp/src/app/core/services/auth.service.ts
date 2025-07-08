import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthRequest } from '../models/auth-request';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { AuthResponse } from '../models/auth-response';
import { environment } from '../../../environments/environment';
import { UserInfos } from '../models/user-infos';
import { ChangePasswordRequest } from '../models/change-password-request';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl: string = environment.baseUrl + '/auth';

  private _userInfos$ = new BehaviorSubject<UserInfos>({});
  get userInfos$(): Observable<UserInfos> {
    return this._userInfos$.asObservable();
  }

  constructor(private http: HttpClient) {}

  login(request: AuthRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, request);
  }

  getUserInfosFromServer(): void {
    this.http
      .get<UserInfos>(`${this.baseUrl}/infos`)
      .pipe(
        tap((infos) => {
          console.log(infos);
          this._userInfos$.next(infos);
        })
      )
      .subscribe();
  }

  changePassword(
    id: number,
    request: ChangePasswordRequest
  ): Observable<number> {
    return this.http.put<number>(
      `${this.baseUrl}/changepassword/${id}`,
      request
    );
  }

  updateInfos(id: number, request: UserInfos): Observable<UserInfos> {
    return this.http
      .put<UserInfos>(`${this.baseUrl}/updateinfos/${id}`, request)
      .pipe(
        tap((infos) => {
          console.log(infos);
          this._userInfos$.next(infos);
        })
      );
  }
}
