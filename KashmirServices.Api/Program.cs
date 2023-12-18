using KashmirServices.Api.DI;
using KashmirServices.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiServices(builder.Configuration, builder.Environment);

var app = builder.Build();

app.UseCors(option =>
{
    option.SetIsOriginAllowed(_ => true)
    .AllowAnyHeader()
    .AllowAnyMethod();
});

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
