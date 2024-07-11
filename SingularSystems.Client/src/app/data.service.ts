import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataService {
private apiBaseUrl = 'https://localhost:7179/api/Products';

  constructor(private http: HttpClient) { }


  getProducts():Observable<any>{
    return this.http.get(this.apiBaseUrl + "/products")
  }

  getProductSummary(productId: any):Observable<any>{
    return this.http.get(this.apiBaseUrl + "/product-sales-summary?productId=" + productId)
  }
}
