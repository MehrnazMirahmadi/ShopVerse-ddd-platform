using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopVerse.IdentityServer.Api.Configuration;
using ShopVerse.IdentityServer.Application;
using ShopVerse.IdentityServer.Application.Dtos;
using ShopVerse.IdentityServer.Application.Validators;
using ShopVerse.IdentityServer.Infrastructure;
using ShopVerse.IdentityServer.Infrastructure.Identity;
using ShopVerse.IdentityServer.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddIdentityServerInfrastructure(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();

builder.Services.AddControllers();


builder.Services.AddIdentityServer()
    .AddInMemoryClients(Clients.GetClients())
    .AddInMemoryApiScopes(ApiScopes.GetApiScopes())
    .AddInMemoryIdentityResources(Resources.GetIdentityResources())
    .AddAspNetIdentity<ApplicationUser>();

// Add other services like EF DbContext, Identity, etc.

var app = builder.Build();

app.UseIdentityServer();



// Configure the HTTP request pipeline.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShopVerseIdentityDbContext>();
    dbContext.Database.Migrate();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
