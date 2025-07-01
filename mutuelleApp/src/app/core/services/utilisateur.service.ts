import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Utilisateur } from '../models/utilisateur';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { SafeUrl } from '@angular/platform-browser';
import { UploadImage } from '../models/upload-image';
import { UserInfos } from '../models/user-infos';

@Injectable({
  providedIn: 'root',
})
export class UtilisateurService {
  baseUrl: string = environment.baseUrl;

  private _utilisateurs$ = new BehaviorSubject<Utilisateur[]>([]);
  get utilisateurs$(): Observable<Utilisateur[]> {
    return this._utilisateurs$.asObservable();
  }
  constructor(private http: HttpClient) {}

  getAllUtilisateurFromServer(): void {
    this.http
      .get<Utilisateur[]>(`${this.baseUrl}/utilisateur`)
      .pipe(
        tap((utilisateurs) => {
          console.log(utilisateurs);
          this._utilisateurs$.next(utilisateurs);
        })
      )
      .subscribe();
  }

  addImage(id: number, request: UploadImage): Observable<UserInfos> {
    return this.http.put<UserInfos>(
      `${this.baseUrl}/utilisateur/${id}`,
      request
    );
  }
}
