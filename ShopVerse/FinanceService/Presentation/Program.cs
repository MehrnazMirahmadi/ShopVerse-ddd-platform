using Finance.Application;
using Finance.Infrastructure;
using Finance.Infrastructure.Persistence.Context;
using Finance.Infrastructure.Persistence.Seed;
using Finance.Presentation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//  FinanceDbContext 
builder.Services
    .AddFinanceInfrastructure(builder.Configuration)
    .AddApplicationServices(builder.Configuration)  
    .AddApiServices();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

//  Migration  Development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<FinanceDbContext>();

   
    dbContext.Database.Migrate();

 
    SeedData.Initialize(dbContext);

    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
