export interface CartItem{
    cartItemId: number;
    productId: number;
    
    imageUrl: string;
    description: string;
    price: number;
    quantity: number;
    total: number;
}