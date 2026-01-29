import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Product } from '../../interfaces/IProducts'; 
import { NgFor, NgIf } from '@angular/common';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [NgFor, NgIf, CommonModule, HttpClientModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})

export class ProductsComponent implements OnInit {

  products: Product[] = [];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    this.http.get<Product[]>('https://localhost:7159/api/products')
      .subscribe({
        next: (data) => {this.products = data, console.log(this.products)},
        error: (err) => console.error(err)
      });
  }

}
