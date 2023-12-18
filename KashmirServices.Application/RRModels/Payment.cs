namespace KashmirServices.Application.RRModels;

public class PaymentDetailsRequest
{
    public string razorpay_payment_id { get; set; } = string.Empty;

    public string razorpay_order_id { get; set; } = string.Empty;

    public string razorpay_signature { get; set; } = string.Empty;
}

public class PaymentDetailsResponse
{
    public bool IsPaymentSuccessfull { get; set; }

}


public class PaymentRefundRequest
{
    public List<string> RpPaymentId { get; set; } = null!;

}

public class PaymentRefundResponse
{
    public string TransactionId { get; set; } = string.Empty;

    public bool isRefunded { get; set; }

}

public class UserPaymentRefundRequest
{
    public Guid OrderId { get; set; }
}

