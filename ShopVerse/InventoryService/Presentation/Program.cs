using Presentation;
using Presentation.GrpcServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddInfrastructureServices(builder.Configuration)
.AddApplicationServices().AddApiServices();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddGrpc();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<InventoryServiceImpl>();
app.MapGet("/", () => "This service is for gRPC only.");

app.Run();



