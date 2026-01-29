import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductsComponent } from './pages/products/products.component';
import { TransactionsComponent } from './pages/transactions/transactions.component';
import { NotfoundComponent } from './shared/notfound/notfound.component';
import { EditProductComponent } from './pages/edit-product/edit-product.component';

export const routes: Routes = [
  { path: '', component: ProductsComponent },
  { path: 'products', component: ProductsComponent },
  { path: 'products/edit', component: EditProductComponent },
  { path: 'products/create', component: EditProductComponent },
  { path: 'transactions', component: TransactionsComponent },

  // 404 para rutas erroneas
  { path: '**', component: NotfoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }