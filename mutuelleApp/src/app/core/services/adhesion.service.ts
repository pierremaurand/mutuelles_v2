import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { AdhesionRequest } from '../models/adhesion-request';
import { Adhesion } from '../models/adhesion';
import { Membre } from '../models/membre';

@Injectable({
  providedIn: 'root',
})
export class AdhesionService {
  baseUrl: string = environment.baseUrl + '/adhesion';
  private membres: Membre[] = [];

  private _adhesions$ = new BehaviorSubject<Adhesion[]>([]);
  get adhesions$(): Observable<Adhesion[]> {
    return this._adhesions$.asObservable();
  }

  private _adhesion$ = new BehaviorSubject<Adhesion>(new Adhesion());
  get adhesion$(): Observable<Adhesion> {
    return this._adhesion$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllAdhesionsFromServer(): void {
    this.http
      .get<Adhesion[]>(`${this.baseUrl}`)
      .pipe(
        tap((adhesions) => {
          this._adhesions$.next(adhesions);
        })
      )
      .subscribe();
  }

  getAdhesionFromServer(id: number): void {
    if (id != 0) {
      this.http
        .get<Adhesion>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((adhesion) => {
            this._adhesion$.next(adhesion);
          })
        )
        .subscribe();
    } else {
      this._adhesion$.next(new Adhesion());
    }
  }

  update(id: number, request: AdhesionRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, request);
  }

  add(request: AdhesionRequest[]): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, request);
  }

  setMembres(membres: Membre[]): void {
    this.membres = membres;
  }

  getMembreById(id: number): Membre {
    const membre = this.membres.find((m) => m.id === id);
    return membre ? membre : new Membre();
  }
}
