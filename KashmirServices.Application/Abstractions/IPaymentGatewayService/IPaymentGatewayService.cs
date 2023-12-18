using EMedAppointmentSystem.Domain.Entities;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Entities;

namespace KashmirServices.Application.Abstractions.IPaymentGatewayService;

public interface IPaymentGatewayService
{
    public AppOrder CreateOrder(AppOrder model);

    public Task<AppPayment> CapturePayment(PaymentDetailsRequest model);


    public Task<IEnumerable<PaymentRefundResponse>> RefundPayment(IEnumerable<string> rpPaymnetIds);

}
