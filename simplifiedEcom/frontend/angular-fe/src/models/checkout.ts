export interface OrderItem{
  productId: number;
  productName: number;
  quantity: number;
  unitPrice: number;
}

export interface OrderResponse{
  orderId: number;
  totalAmount: number;

  shippingName: string;
  shippingAddress: string;
  shippingCity: string;
  shippingPostalCode: string;

  items: OrderItem[];
}

export interface CheckoutRequest{
  userId: number;

  shippingName: string;
  shippingAddress: string;
  shippingCity: string;
  shippingPostalCode: string;
}