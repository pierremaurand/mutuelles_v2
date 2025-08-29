import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { MouvementRequest } from '../models/mouvement-request';
import { Mouvement } from '../models/mouvement';
import { Membre } from '../models/membre';
import { Echeance } from '../models/echeance';

@Injectable({
  providedIn: 'root',
})
export class MouvementService {
  baseUrl: string = environment.baseUrl + '/mouvement';
  private membres: Membre[] = [];

  private _mouvements$ = new BehaviorSubject<Mouvement[]>([]);
  get mouvements$(): Observable<Mouvement[]> {
    return this._mouvements$.asObservable();
  }

  private _mouvement$ = new BehaviorSubject<Mouvement>(new Mouvement());
  get mouvement$(): Observable<Mouvement> {
    return this._mouvement$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllMouvementsFromServer(): void {
    this.http
      .get<Mouvement[]>(`${this.baseUrl}`)
      .pipe(
        tap((mouvements) => {
          this._mouvements$.next(mouvements);
        })
      )
      .subscribe();
  }

  getMouvementFromServer(id: number): void {
    if (id != 0) {
      this.http
        .get<Mouvement>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((mouvement) => {
            this._mouvement$.next(mouvement);
          })
        )
        .subscribe();
    } else {
      this._mouvement$.next(new Mouvement());
    }
  }

  update(id: number, request: MouvementRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, request);
  }

  add(mouvement: MouvementRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, mouvement);
  }

  addAll(mouvements: MouvementRequest[]): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/add-all`, mouvements);
  }
}
