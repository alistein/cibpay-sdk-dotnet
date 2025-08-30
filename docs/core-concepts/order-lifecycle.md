# Order Lifecycle

Orders go through different statuses during payment processing.

## Order Statuses

### Success Flow
- **New** - Order created, waiting for customer
- **Prepared** - Customer is authenticating with bank
- **Authorized** - Payment approved, funds held
- **Charged** - Payment completed, funds transferred

### Final Statuses
- **Reversed** - Authorization cancelled
- **Refunded** - Money returned to customer
- **Declined** - Bank declined the payment
- **Rejected** - System rejected the order
- **Fraud** - Flagged as fraudulent
- **Error** - Technical error occurred

## Checking Order Status

```csharp
var order = await client.Orders.GetAsync(orderId);

switch (order.Data.Status)
{
    case OrderStatuses.Charged:
        // Payment successful - deliver goods
        break;
    case OrderStatuses.Declined:
        // Payment failed - notify customer
        break;
    case OrderStatuses.Authorized:
        // Payment approved - ready to charge
        break;
    // Handle other statuses...
}
```



