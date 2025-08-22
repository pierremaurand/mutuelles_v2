import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthRequest } from '../models/auth-request';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { AuthResponse } from '../models/auth-response';
import { environment } from '../../../environments/environment';
import { UserInfos } from '../models/user-infos';
import { ChangePasswordRequest } from '../models/change-password-request';
import { UpdatePhotoRequest } from '../models/update-photo-request';

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
    return this.http.post<AuthResponse>(this.baseUrl, request);
  }

  refreshToken(): Observable<AuthResponse> {
    return this.http.get<AuthResponse>(`${this.baseUrl}/refresh-token`);
  }

  getUserInfosFromServer(): void {
    this.http
      .get<UserInfos>(this.baseUrl)
      .pipe(
        tap((infos) => {
          this._userInfos$.next(infos);
        })
      )
      .subscribe();
  }

  changePassword(id: number, request: ChangePasswordRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/password/${id}`, request);
  }

  updatePhoto(id: number, request: UpdatePhotoRequest): Observable<UserInfos> {
    return this.http.put<UserInfos>(`${this.baseUrl}/photo/${id}`, request);
  }
}
