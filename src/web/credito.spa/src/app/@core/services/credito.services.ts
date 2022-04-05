import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "../../../environments/environment";
import { Credito } from "../models/credito";

@Injectable()
export class CreditoService {
  protected url: string = `${environment.url}api/creditos`;

  constructor(private http: HttpClient) {}

  getCreditos(): Observable<any[]> {
    return this.http.get<any[]>(this.url);
  }

  createCredito(movimentation: Credito): Observable<any> {
    return this.http.post<any>(this.url, movimentation);
  }

  updateCredito(movimentation: Credito): Observable<any> {
    return this.http.put<any>(this.url, movimentation);
  }

  deleteCredito(id: string): Observable<any> {
    return this.http.delete<any>(`${this.url}?id=${id}`);
  }
}
