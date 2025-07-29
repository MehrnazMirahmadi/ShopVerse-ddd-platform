using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ShopVerse.IdentityServer.Api.Configuration;
using ShopVerse.IdentityServer.Application;
using ShopVerse.IdentityServer.Application.Dtos;
using ShopVerse.IdentityServer.Application.Validators;
using ShopVerse.IdentityServer.Infrastructure;
using ShopVerse.IdentityServer.Infrastructure.Persistence;



var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddApplicationServices();
builder.Services.AddIdentityServerInfrastructure(builder.Configuration);
builder.Services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
builder.Services.AddScoped<IValidator<LoginUserDto>, LoginUserDtoValidator>();
builder.Services.AddControllers();



builder.Services.AddIdentityServer(options =>
{
    options.IssuerUri = "https://shopverse.identityserver.api:8095"; 
}) .AddDeveloperSigningCredential()
    .AddInMemoryApiScopes(ApiScopes.GetApiScopes())
    .AddInMemoryApiResources(ApiResources.GetApiResources())
    .AddInMemoryClients(Clients.GetClients(builder.Configuration));


var app = builder.Build();

//  Ensure database is migrated on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ShopVerseIdentityDbContext>();
    dbContext.Database.Migrate();
}

// Configure middleware
app.UseHttpsRedirection();

// These are important!
app.UseRouting();
app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
