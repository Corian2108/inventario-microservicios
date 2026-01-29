export interface Transaction {
    transactionId: number
    productId: number
    type: number
    quantity: number
    transactionDate: string
    detail: string
    unitPrice: number
    totalPrice: number
    typeName: string

    productName: string
}
