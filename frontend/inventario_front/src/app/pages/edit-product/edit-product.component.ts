import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../../interfaces/IProducts';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { Category } from '../../interfaces/ICategory';

@Component({
  selector: 'app-edit-product',
  imports: [CommonModule, FormsModule, HttpClientModule],
  templateUrl: './edit-product.component.html',
  styleUrl: './edit-product.component.css'
})
export class EditProductComponent implements OnInit {

  product: Product | null = null; // Si recibe producto sirve para editar, si no recibe producto sirve para crear
  newProduct: Product | any;
  categoryMap: Category | any;

  constructor(private route: ActivatedRoute, private router: Router, private http: HttpClient) {
    const navigation = this.router.getCurrentNavigation();
    this.product = navigation?.extras.state?.['product'] || null;
  }

  ngOnInit(): void {
    if (this.product !== null) {
      this.newProduct = { ...this.product };
    } else {
      this.newProduct = {
        productId: 0,
        name: '',
        description: '',
        entryDate: '',
        category: {
          categoryId: 0,
          name: ''
        },
        price: 0,
        stock: 0,
        imageUrl: ''
      };
    }
    this.loadCategories()
  }

  loadCategories() {
    this.http.get<{ id: number; name: string }[]>('https://localhost:7159/api/Products/categories')
      .subscribe({
        next: (data) => {
          this.categoryMap = data;
        },
        error: (err) => console.error('Error al cargar categorías', err)
      });
  }

  onSubmit() {
    //Normalización de datos
    const timestamp = Date.now();
    const date = new Date(timestamp);

    this.newProduct.entryDate = date.toISOString()

    if (this.product !== null) {
      // Edita producto
      this.newProduct.category = null //evita conflicto con EntityFramework

      this.http.put(`https://localhost:7159/api/Products/${this.newProduct.productId}`, this.newProduct)
        .subscribe({
          next: () => { alert('Producto actualizado'), this.product = null, this.newProduct = null, this.router.navigate(['/products']) },
          error: () => alert('Error al actualizar producto')
        });
    } else {
      // crea producto
      this.newProduct.categoryFk = parseInt(this.newProduct.category.categoryId)
      this.newProduct.category = null //evita conflicto con EntityFramework

      this.http.post('https://localhost:7159/api/Products', this.newProduct, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
        .subscribe({
          next: () => { alert('Producto creado'), this.product = null, this.newProduct = null, this.router.navigate(['/products']) },
          error: () => alert('Error al crear producto')
        });
    }
  }

}
