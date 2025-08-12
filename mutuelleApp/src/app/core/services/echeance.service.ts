import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Echeance } from '../models/echeance';
import { Membre } from '../models/membre';

@Injectable({
  providedIn: 'root',
})
export class EcheanceService {
  baseUrl: string = environment.baseUrl + '/echeance';

  private _echeances$ = new BehaviorSubject<Echeance[]>([]);
  get echeances$(): Observable<Echeance[]> {
    return this._echeances$.asObservable();
  }

  private _echeance$ = new BehaviorSubject<Echeance>(new Echeance());
  get echeance$(): Observable<Echeance> {
    return this._echeance$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllEcheancesFromServer(): void {
    this.http
      .get<Echeance[]>(`${this.baseUrl}`)
      .pipe(
        tap((echeances) => {
          console.log('echeances', echeances);
          this._echeances$.next(echeances);
        })
      )
      .subscribe();
  }

  getEcheanceFromServer(id: number): void {
    if (id != 0) {
      this.http
        .get<Echeance>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((echeance) => {
            this._echeance$.next(echeance);
          })
        )
        .subscribe();
    } else {
      this._echeance$.next(new Echeance());
    }
  }
}
