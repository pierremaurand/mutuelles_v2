import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Membre } from '../models/membre';
import { MembreRequest } from '../models/membre-request';

@Injectable({
  providedIn: 'root',
})
export class MembreService {
  baseUrl: string = environment.baseUrl + '/membre';
  private membres: Membre[] = [];

  private _membres$ = new BehaviorSubject<Membre[]>([]);
  get membres$(): Observable<Membre[]> {
    return this._membres$.asObservable();
  }

  private _membre$ = new BehaviorSubject<Membre>(new Membre());
  get membre$(): Observable<Membre> {
    return this._membre$.asObservable();
  }

  constructor(private http: HttpClient) {}

  getAllMembresFromServer(): void {
    this.http
      .get<Membre[]>(`${this.baseUrl}`)
      .pipe(
        tap((membres) => {
          this.membres = membres;
          this._membres$.next(membres);
        })
      )
      .subscribe();
  }

  getMembreFromServer(id: number): void {
    if (id) {
      this.http
        .get<Membre>(`${this.baseUrl}/${id}`)
        .pipe(
          tap((membre) => {
            this._membre$.next(membre);
          })
        )
        .subscribe();
    } else {
      this._membre$.next(new Membre());
    }
  }

  addOrUpdate(id: number, request: MembreRequest): Observable<any> {
    if (id) {
      return this.update(id, request);
    }
    return this.add(request);
  }

  update(id: number, request: MembreRequest): Observable<any> {
    return this.http.put<any>(`${this.baseUrl}/${id}`, request);
  }

  add(request: MembreRequest): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, request);
  }

  getMembreById(id: number): Membre {
    return this.membres.find((membre) => membre.id === id) || new Membre();
  }
}
