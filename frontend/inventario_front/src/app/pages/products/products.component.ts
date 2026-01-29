import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Product } from '../../interfaces/IProducts'; 
import { NgFor, NgIf } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [NgFor, NgIf, CommonModule, HttpClientModule],
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})

export class ProductsComponent implements OnInit {

  products: Product[] = [];

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.getProducts();
  }

  getProducts() {
    //devuelve todos los productos existentes para pintar en la tabla
    this.http.get<Product[]>('https://localhost:7159/api/products')
      .subscribe({
        next: (data) => {this.products = data, console.log(this.products)},
        error: (err) => console.error(err)
      });
  }

  editProduct(product: Product){
    //abre la ventana de edición pasandole el producto como argumento
    console.log(product)
    this.router.navigate(['/products/edit'], { state: { product } });
  }

  createProduct(){
    //abre la ventana de edición pasandole el producto como argumento
    this.router.navigate(['/products/create']);
  }

  deleteProduct(id: number){
    //captura la id del producto para eliminar
    console.log(id)
    if(confirm('¿Seguro que quieres eliminar este producto?')) {
    this.http.delete(`https://localhost:7159/api/Products/${id}`)
      .subscribe({
        next: () => {
          alert('Producto eliminado');
          // actualizar la lista localmente
          this.products = this.products.filter(p => p.productId !== id);
        },
        error: (err) => alert(`Error al eliminar el producto: ${err}`)
      });
  }
  }

}
