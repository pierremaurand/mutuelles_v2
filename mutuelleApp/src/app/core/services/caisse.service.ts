import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Caisse } from '../models/caisse';

@Injectable({
  providedIn: 'root',
})
export class CaisseService {
  baseUrl: string = environment.baseUrl + '/caisse';

  private _caisses$ = new BehaviorSubject<Caisse[]>([]);
  get caisses$(): Observable<Caisse[]> {
    return this._caisses$.asObservable();
  }

  private _caisse$ = new BehaviorSubject<Caisse>({ id: 0, nom: '' });
  get caisse$(): Observable<Caisse> {
    return this._caisse$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllCaissesFromServer(): void {
    this.http
      .get<Caisse[]>(`${this.baseUrl}`)
      .pipe(
        tap((caisses) => {
          this._caisses$.next(caisses);
        })
      )
      .subscribe();
  }

  getCaisseFromServer(id: number): void {
    if (id != 0) {
      this.http
        .get<Caisse>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((caisse) => {
            this._caisse$.next(caisse);
          })
        )
        .subscribe();
    } else {
      this._caisse$.next({ id: 0, nom: '' });
    }
  }

  addOrUpdate(id: number, request: Caisse): Observable<any> {
    if (id) {
      return this.update(id, request);
    }
    return this.add(request);
  }

  update(id: number, request: Caisse): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, request);
  }

  add(request: Caisse): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, request);
  }
}
