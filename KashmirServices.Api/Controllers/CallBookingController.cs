using KashmirServices.Api.Controllers.Common;
using KashmirServices.Application.Abstractions.IServices;
using KashmirServices.Application.RRModels;
using KashmirServices.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace KashmirServices.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CallBookingController : ApiController
{
    private readonly ICallBookingService service;

    public CallBookingController(
        ICallBookingService service)
    {
        this.service = service;
    }



    [HttpPost]
    public async Task<IResult> Post([FromBody] CallBookingRequest model)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.Add(model);

        return this.APIResult(result);
    }


    [HttpGet("my-bookings")]
    public async Task<IResult> GetMyBooking()
    {
        var result = await service.GetMyBookings();

        return this.APIResult(result);
    }


    [HttpGet("booking-cancel/{id:guid}")]
    public async Task<IResult> CancelBooking(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.CancelBooking(id);

        return this.APIResult(result);
    }


    [HttpGet("booking-reminder/{id:guid}")]
    public async Task<IResult> BookingReminder(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.BookingReminder(id);

        return this.APIResult(result);
    }



    [HttpGet("all-bookings")]
    public async Task<IResult> GetAllBooking()
    {
        var result = await service.GetAllBookings();

        return this.APIResult(result);
    }


    [HttpPost("re-schedule/booking")]
    public async Task<IResult> ScheduleBooking(CustomerReScheduleCallBookingRequest model)
    {
        var result = await service.ReScheduleCallBookingByCustomer(model);

        return this.APIResult(result);
    }



    [HttpPost("add/Callbooking-with-new-address")]
    public async Task<IResult> PostWithAddress([FromBody] CallBookingWithAddressRequest model)
    {
        if (!ModelState.IsValid)
        {
            return Results.BadRequest(ModelState);
        }

        var result = await service.Add(model);

        return this.APIResult(result);
    }
}
