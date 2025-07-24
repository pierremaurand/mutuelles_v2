import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { CreditRequest } from '../models/credit-request';
import { Credit } from '../models/credit';
import { Membre } from '../models/membre';
import { Echeance } from '../models/echeance';

@Injectable({
  providedIn: 'root',
})
export class CreditService {
  baseUrl: string = environment.baseUrl + '/credit';
  private membres: Membre[] = [];

  private _credits$ = new BehaviorSubject<Credit[]>([]);
  get credits$(): Observable<Credit[]> {
    return this._credits$.asObservable();
  }

  private _credit$ = new BehaviorSubject<Credit>(new Credit());
  get credit$(): Observable<Credit> {
    return this._credit$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllCreditsFromServer(): void {
    this.http
      .get<Credit[]>(`${this.baseUrl}`)
      .pipe(
        tap((credits) => {
          this._credits$.next(credits);
        })
      )
      .subscribe();
  }

  getCreditFromServer(id: number): void {
    if (id != 0) {
      this.http
        .get<Credit>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((credit) => {
            this._credit$.next(credit);
          })
        )
        .subscribe();
    } else {
      this._credit$.next(new Credit());
    }
  }

  update(id: number, request: CreditRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, request);
  }

  add(credit: CreditRequest, echeancier: Echeance[]): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, {
      credit: credit,
      echeancier: echeancier,
    });
  }

  anticipationPaiement(id: number, echeancier: Echeance[]): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/anticipation/${id}`, echeancier);
  }

  setMembres(membres: Membre[]): void {
    this.membres = membres;
  }

  getMembreById(id: number): Membre {
    const membre = this.membres.find((m) => m.id === id);
    return membre ? membre : new Membre();
  }
}
