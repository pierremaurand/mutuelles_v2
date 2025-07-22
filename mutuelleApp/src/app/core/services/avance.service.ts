import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Avance } from '../models/avance';
import { AvanceRequest } from '../models/avance-request';

@Injectable({
  providedIn: 'root',
})
export class AvanceService {
  baseUrl: string = environment.baseUrl + '/avance';

  private _avances$ = new BehaviorSubject<Avance[]>([]);
  get avances$(): Observable<Avance[]> {
    return this._avances$.asObservable();
  }

  private _avance$ = new BehaviorSubject<Avance>({ id: 0, nom: '' });
  get avance$(): Observable<Avance> {
    return this._avance$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllAvancesFromServer(): void {
    this.http
      .get<Avance[]>(`${this.baseUrl}`)
      .pipe(
        tap((avances) => {
          this._avances$.next(avances);
        })
      )
      .subscribe();
  }

  getAvanceFromServer(id: number): void {
    if (id != 0) {
      this.http
        .get<Avance>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((avance) => {
            this._avance$.next(avance);
          })
        )
        .subscribe();
    } else {
      this._avance$.next({ id: 0, nom: '' });
    }
  }

  addOrUpdate(id: number, request: AvanceRequest): Observable<any> {
    if (id != 0) {
      return this.update(id, request);
    }
    return this.add(request);
  }

  update(id: number, request: AvanceRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, request);
  }

  add(request: AvanceRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, request);
  }
}
