import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { UserInfos } from '../models/user-infos';
import { UpdateUtilisateurActifRequest } from '../models/update-utilisateur-actif-request';
import { UtilisateurRequest } from '../models/utilisateur-request';
import { Utilisateur } from '../models/utilisateur';

@Injectable({
  providedIn: 'root',
})
export class UtilisateurService {
  baseUrl: string = environment.baseUrl;

  private _utilisateurs$ = new BehaviorSubject<Utilisateur[]>([]);
  get utilisateurs$(): Observable<Utilisateur[]> {
    return this._utilisateurs$.asObservable();
  }

  private _utilisateur$ = new BehaviorSubject<UserInfos>({});
  get utilisateur$(): Observable<UserInfos> {
    return this._utilisateur$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllUtilisateursFromServer(): void {
    this.http
      .get<Utilisateur[]>(`${this.baseUrl}/utilisateur`)
      .pipe(
        tap((utilisateurs) => {
          this._utilisateurs$.next(utilisateurs);
        })
      )
      .subscribe();
  }

  getUtilisateurFromServer(id: number): void {
    if (id != 0) {
      this.http
        .get<Utilisateur>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((utilisateur) => {
            this._utilisateur$.next(utilisateur);
          })
        )
        .subscribe();
    } else {
      this._utilisateur$.next(new Utilisateur());
    }
  }

  getUtilisateur(id: number): void {
    if (id != 0) {
      this.http
        .get<UserInfos>(`${this.baseUrl}/utilisateur/${id}`)
        .pipe(
          tap((utilisateur) => {
            this._utilisateur$.next(utilisateur);
          })
        )
        .subscribe();
    } else {
      this._utilisateur$.next({});
    }
  }

  addOrUpdateUser(id: number, request: UtilisateurRequest): Observable<any> {
    if (id) {
      return this.updateUtilisateur(id, request);
    }
    return this.addUser(request);
  }

  updateUtilisateur(id: number, request: UtilisateurRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/utilisateur/${id}`, request);
  }

  updateActif(
    id: number,
    request: UpdateUtilisateurActifRequest
  ): Observable<any> {
    return this.http.put<any>(
      `${this.baseUrl}/utilisateur/activate/${id}`,
      request
    );
  }

  addUser(request: UtilisateurRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/utilisateur`, request);
  }
}
