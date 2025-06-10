using Infrastructure.Extentions;
using Presentation;
//using Inventory.Presentation.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration)
.AddApplicationServices(builder.Configuration)
.AddApiServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHealthChecks()
//    .AddSqlServer(builder.Configuration.GetConnectionString("Database")!);
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapHealthChecks("/health");
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
app.Run();



