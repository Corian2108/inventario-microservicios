import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule, HttpParams } from '@angular/common/http';
import { NgFor } from '@angular/common';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Transaction } from '../../interfaces/ITransaction';
import { Product } from '../../interfaces/IProducts';

@Component({
  selector: 'app-transactions',
  imports: [NgFor, CommonModule, HttpClientModule, FormsModule],
  templateUrl: './transactions.component.html',
  styleUrl: './transactions.component.css'
})

export class TransactionsComponent implements OnInit {

  apiUrl = 'https://localhost:7255/api'
  apiUrlproducts = 'https://localhost:7159/api'

  transactions: Transaction[] = []
  products: Product[] = [];

  startDate: string = ''
  endDate: string = ''
  selectedType = 0

  newTransaction: Transaction = {
    transactionId: 0,
    productId: 0,
    type: 0,
    quantity: 0,
    transactionDate: '',
    detail: '',
    unitPrice: 0,
    totalPrice: 0,
    typeName: '',
    productName: ''
  }

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.getTransactionsFiltered('', '', 2)
    this.getProducts()
  }

  getTransactionById(id: number) {
    return this.http.get<Transaction>(
      `${this.apiUrl}/Transactions/${id}`
    ).subscribe({
      next: (data) => { this.transactions = [data], console.log(this.transactions) },
      error: (err) => console.error(err)
    });
  }

  getProducts() {
    //devuelve todos los productos existentes para pintar en la tabla
    this.http.get<Product[]>(`${this.apiUrlproducts}/Products`)
      .subscribe({
        next: (data) => { this.products = data, console.log(this.products) },
        error: (err) => console.error(err)
      });
  }

  getTransactionsFiltered(
    startDate?: string,
    endDate?: string,
    type?: number
  ): void {

    let params = new HttpParams();

    if (startDate) {
      params = params.set('startDate', startDate);
    }

    if (endDate) {
      params = params.set('endDate', endDate);
    }

    if (type !== undefined && type > 0) {
      params = params.set('type', type);
    }

    this.http.get<Transaction[]>(`${this.apiUrl}/Transactions/`, { params })
      .subscribe({
        next: (res) => {
          this.transactions = res;
          this.transactions.forEach(t => {
            for (let prod of this.products) {
              if (prod.productId == t.transactionId) {
                t.productName = prod.name
                break
              }
            }
          });

          if (res.length === 0) {
            alert('No se encontraron transacciones con los filtros aplicados');
          }
        },
        error: () => {
          alert('Error al obtener las transacciones');
        }
      });
  }

  recalculatePrices(): void {
    const product = this.products.find(p => p.productId == this.newTransaction.productId);

    if (!product) {
      this.newTransaction.unitPrice = 0;
      this.newTransaction.totalPrice = 0;
      return;
    }

    this.newTransaction.unitPrice = product.price;
    this.newTransaction.totalPrice =
      product.price * this.newTransaction.quantity;
  }

  saveTransaction(): void {

    if (
      !this.newTransaction.type ||
      !this.newTransaction.productId ||
      !this.newTransaction.quantity ||
      this.newTransaction.quantity <= 0
    ) {
      alert('Debe completar tipo, producto y cantidad válida');
      return;
    }

    let url = '';

    if (this.newTransaction.type === 1) {
      url = `${this.apiUrl}/purchase`;
    } else if (this.newTransaction.type === 2) {
      url = `${this.apiUrl}/sale`;
    } else {
      alert('Tipo de transacción inválido');
      return;
    }

    this.http.post(url, this.newTransaction)
      .subscribe({
        next: () => {
          alert('Transacción registrada correctamente');

          this.newTransaction = {
            transactionId: 0,
            productId: 0,
            type: 0,
            quantity: 0,
            transactionDate: '',
            detail: '',
            unitPrice: 0,
            totalPrice: 0,
            typeName: '',
            productName: ''
          };

          this.getTransactionsFiltered();
        },
        error: () => {
          alert('Error al registrar la transacción');
        }
      });
  }

}
