using System.ComponentModel.DataAnnotations;

namespace KashmirServices.Domain.Enums;

public enum UserRole
{
    Admin = 1,
    Manager = 2,
    ServiceEngineer = 3,
    Customer = 4,
}

public enum UserStatus
{
    Active = 1,
    InActive = 2,
}

public enum Gender
{
    NotKnown=0,
    Male = 1,
    Female = 2
}


public enum AppOrderStatus
{
    Created = 1,
    Attempted = 2,
    Paid = 3
}

public enum PaymentMethod
{
    Card = 1,
    UPI = 2,
    NetBanking = 3,
    Wallet = 4,
    //EMI = 5
}


public enum AppPaymentStatus
{
    Created = 1,
    Authorized = 2,
    Captured = 3,
    Refunded = 4,
    Failed = 5
}


public enum RpRefundStatus
{
    Pending = 1,
    Processed = 2,
    Failed = 3
}


public enum Module
{
    User = 1,
    Category=2,
    Service=3,
    AppFile=4
}

public enum CallBookingStatus
{
    Pending = 1,
    Rejected = 2,
    Unassigned = 3,
    Assigned = 4,
    InProgress = 5,
    Completed = 6,
    Cancelled=7,
}

public enum EngineerStatus
{
    Pending = 1,
    Rejected = 2,
    Accepted = 3,
    Completed = 4, 
}

public enum CallStatus
{
    Open=1,
    Close=2,
    reallocated=3,
    cancelled=4
}

