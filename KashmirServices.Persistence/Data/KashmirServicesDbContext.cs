using Dapper;
using EMedAppointmentSystem.Domain.Entities;
using KashmirServices.Domain.Entities;
using KashmirServices.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace KashmirServices.Persistence;

public class KashmirServicesDbContext : DbContext
{
    public KashmirServicesDbContext(DbContextOptions<KashmirServicesDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var relationShip in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
        {
            relationShip.DeleteBehavior = DeleteBehavior.Restrict;

        }
        modelBuilder.SeedData();
    }


    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<TimeOnly>()
            .HaveConversion<CustomTimeOnlyConverter>()
            .HaveColumnType("time");
        SqlMapper.AddTypeHandler(new SqlTimeOnlyTypeHandler());

        base.ConfigureConventions(builder);
    }



    #region Entity Sets

    public DbSet<User> Users { get; set; }

    public DbSet<JobRole> JobRoles { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ServiceType> ServiceTypes { get; set; }

    public DbSet<AssignedEngineer> AssignedEngineers { get; set; }

    public DbSet<CallBooking> CallBookings { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<AppFile> AppFiles { get; set; }

    public DbSet<Contact> Contacts { get; set; }

    public DbSet<Brand> Brands { get; set; }

    public DbSet<Parts> Parts { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceDetails> InvoiceDetails { get; set; }

    public DbSet<Visit> Visits { get; set; }

    public DbSet<AppPayment> AppPayments { get; set; }

    public DbSet<AppOrder> AppOrders { get; set; }
    #endregion
}

#region seed data
public static class ModelBuilderExtentions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = Guid.Parse("764cbe0e-9b2e-4d95-af49-01b45a0fca5b"),
            Username = "admin",
            FullName = "admin",
            Password = "$2a$11$TYYBxfFaSET3Oizd0CXEleNVtkdE7FEE.p60NpoAT13WT38X2OP5q",
            Salt = "$2a$11$TYYBxfFaSET3Oizd0CXEle",
            Email = "samiaullah1@gmail.com",
            PhoneNumber = "8825084050",
            Gender = Domain.Enums.Gender.Male,
            UserRole = Domain.Enums.UserRole.Admin,
            UserStatus = Domain.Enums.UserStatus.Active,
            ConfirmationCode = "",
            IsEmailVerified = true,
            CreatedBy = Guid.NewGuid(),
            UpdatedBy = Guid.NewGuid(),
            CreatedOn = DateTime.Now,
            UpdatedOn = DateTime.Now
        });
    }


}

#endregion


