import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Banque } from '../models/banque';

@Injectable({
  providedIn: 'root',
})
export class BanqueService {
  baseUrl: string = environment.baseUrl + '/banque';

  private _banques$ = new BehaviorSubject<Banque[]>([]);
  get banques$(): Observable<Banque[]> {
    return this._banques$.asObservable();
  }

  private _banque$ = new BehaviorSubject<Banque>({ id: 0, nom: '' });
  get banque$(): Observable<Banque> {
    return this._banque$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllBanquesFromServer(): void {
    this.http
      .get<Banque[]>(`${this.baseUrl}`)
      .pipe(
        tap((banques) => {
          this._banques$.next(banques);
        })
      )
      .subscribe();
  }

  getBanqueFromServer(id: number): void {
    if (id != 0) {
      this.http
        .get<Banque>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((banque) => {
            this._banque$.next(banque);
          })
        )
        .subscribe();
    } else {
      this._banque$.next({ id: 0, nom: '' });
    }
  }

  addOrUpdate(id: number, request: Banque): Observable<any> {
    if (id != 0) {
      return this.update(id, request);
    }
    return this.add(request);
  }

  update(id: number, request: Banque): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, request);
  }

  add(request: Banque): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, request);
  }
}
