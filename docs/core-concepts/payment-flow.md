# Payment Flow

Basic payment process with CibPay SDK.


## Payment Steps

### 1. Create Order

```csharp
var request = new CreateOrderRequest
{
    Amount = 99.99m,
    Currency = "AZN",
    Options = new Options
    {
        ReturnUrl = "https://yoursite.com/payment/return",
        AutoCharge = true
    }
};

var order = await client.Orders.CreateAsync(request);
```

### 2. Redirect to Payment

```csharp
// Redirect customer to CibPay payment page
var paymentUrl = $"{basePaymentUrl}{order.Data.Id}";
return Redirect(paymentUrl);
```

### 3. Handle Return

```csharp
[HttpGet("payment/return")]
public async Task<IActionResult> PaymentReturn(string orderId)
{
    var order = await client.Orders.GetAsync(orderId);
    
    return order.Data.Status switch
    {
        OrderStatuses.Charged => RedirectToAction("Success"),
        OrderStatuses.Declined => RedirectToAction("Failed"),
        _ => RedirectToAction("Pending")
    };
}
```

## Payment Options

### 3D Secure
```csharp
var options = new Options
{
    Force3d = 1, // Force 3D Secure authentication
    AutoCharge = true
};
```

### Manual vs Auto Charge
- **AutoCharge = true**: Automatically charge after authorization
- **AutoCharge = false**: Manual charge required after authorization


### Return URL Best Practices
- Use HTTPS for security
- Include your order reference in URL parameters
- Handle both success and failure scenarios


