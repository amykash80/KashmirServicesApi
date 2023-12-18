using System.Security.Cryptography;
using System.Text;
using EMedAppointmentSystem.Domain.Entities;

using KashmirServices.Application.Abstractions.IPaymentGatewayService;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Entities;
using KashmirServices.Domain.Enums;
using Microsoft.Extensions.Options;
using Razorpay.Api;

namespace KashmirService.Infrastructure.RazorPayServices;

public class RazorPayService : IPaymentGatewayService
{
    private readonly PaymentOptions options;

    public RazorPayService(IOptions<PaymentOptions> options)
    {
        this.options = options.Value;
    }

    public async Task<AppPayment> CapturePayment(PaymentDetailsRequest model)
    {
        RazorpayClient client = new(options.Key, options.Secret);

        Payment payment = await Task.Run(() => client.Payment.Fetch(model.razorpay_payment_id));

        using HMACSHA256 hMACSHA256 = new HMACSHA256(Encoding.UTF8.GetBytes(options.Secret));


        var hash = hMACSHA256.ComputeHash(Encoding.UTF8.GetBytes(
            string.Concat(
                model.razorpay_order_id,
                "|",
                model.razorpay_payment_id)));


        bool isSuccess = BitConverter.ToString(hash).Replace("-", "").ToLower() == model.razorpay_signature;


        return new AppPayment
        {
            Amount = (Convert.ToInt32(payment["amount"]) / 100),
            RpOrderId = model.razorpay_order_id,
            Currency = payment["currency"],
            AppPaymentStatus = GetPaymentStatus(payment["stauts"]),
            TransactionId = model.razorpay_payment_id,
            PaymentMethod = payment["method"],
        };

    }

    public AppOrder CreateOrder(AppOrder model)
    {
        Dictionary<string, object> input = new();


        input.Add("amount", Convert.ToDecimal(model.TotalAmount * 100));
        input.Add("currency", model.Currency);
        input.Add("receipt", model.Receipt);
        input.Add("partial_payment", model.IsPartial);

        RazorpayClient client = new(options.Key, options.Secret);

        Order order = client.Order.Create(input);

        return new AppOrder
        {
            OrderId = order["id"],
            TotalAmount = order["amount"],
            AmountPaid = order["amount_paid"],
            AmountDue = order["amount_due"],
            Receipt = order["receipt"],
            OrderStatus = GetOrderStatus(order["status"]),
            CreatedAt = order["created_at"],
            IsPartial = model.IsPartial,
        };
    }

    public async Task<IEnumerable<PaymentRefundResponse>> RefundPayment(IEnumerable<string> transactionIds)
    {
        var client = new RazorpayClient(options.Key, options.Secret);

        List<PaymentRefundResponse> refundResponse = new();

        foreach (string transactionId in transactionIds)
        {
            Payment payment = await Task.Run(() => client.Payment.Fetch(transactionId));
            Dictionary<string, object> data = new();
            Refund refund = await Task.Run(() => payment.Refund(data));

            refundResponse.Add(new PaymentRefundResponse
            {
                isRefunded = Convert.ToString(refund["status"]) == RpRefundStatus.Processed.ToString().ToLower(),
                TransactionId = refund["payment_id"]
            });
        }
        return refundResponse;
    }


    #region private methods
    private static AppOrderStatus GetOrderStatus(object orderStatus)
    {
        var status = Convert.ToString(orderStatus);

        if (status == AppOrderStatus.Created.ToString().ToLower())
            return AppOrderStatus.Created;
        else if (status == AppOrderStatus.Paid.ToString().ToLower())
            return AppOrderStatus.Paid;
        else if (status == AppOrderStatus.Attempted.ToString().ToLower())
            return AppOrderStatus.Attempted;

        return AppOrderStatus.Attempted;
    }
    private static AppPaymentStatus GetPaymentStatus(object paymentStatus)
    {
        string appPaymentStatus = Convert.ToString(paymentStatus)!;

        if (appPaymentStatus == AppPaymentStatus.Captured.ToString())
            return AppPaymentStatus.Captured;
        else if (appPaymentStatus == AppPaymentStatus.Created.ToString())
            return AppPaymentStatus.Created;
        else if (appPaymentStatus == AppPaymentStatus.Authorized.ToString())
            return AppPaymentStatus.Authorized;
        else if (appPaymentStatus == AppPaymentStatus.Failed.ToString())
            return AppPaymentStatus.Failed;

        return AppPaymentStatus.Failed;
    }
    #endregion

}
