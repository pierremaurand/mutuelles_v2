import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Credit } from '../models/credit';
import { CreditRequest } from '../models/credit-request';

@Injectable({
  providedIn: 'root',
})
export class CreditService {
  baseUrl: string = environment.baseUrl + '/credit';

  private _credits$ = new BehaviorSubject<Credit[]>([]);
  get credits$(): Observable<Credit[]> {
    return this._credits$.asObservable();
  }

  private _credit$ = new BehaviorSubject<Credit>({ id: 0, nom: '' });
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
      this._credit$.next({ id: 0, nom: '' });
    }
  }

  addOrUpdate(id: number, request: CreditRequest): Observable<any> {
    if (id != 0) {
      return this.update(id, request);
    }
    return this.add(request);
  }

  update(id: number, request: CreditRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, request);
  }

  add(request: CreditRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, request);
  }
}
