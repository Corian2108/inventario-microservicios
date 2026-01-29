import { Category } from "./ICategory";

export interface Product {
    batch: string;
    category: Category;
    categoryFk: string;
    description: string;
    entryDate: Date;
    imageUrl?: string;
    name: string;
    price: number;
    productId: number;
    stock: number;
}
