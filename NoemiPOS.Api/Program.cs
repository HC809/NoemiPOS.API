using Scalar.AspNetCore;
using NoemiPOS.Api.Extensions;
using NoemiPOS.Application;
using NoemiPOS.Infraestructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNoemiPortalLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000") 
                  .AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowCredentials();
        });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddApplication();
builder.Services.AddInfraestructure(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference();

    app.ApplyMigrations();
}

app.UseCors("AllowNoemiPortalLocalhost");

app.UseHttpsRedirection();

app.UseCustomExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;
