using ITnCare.Commission.Framework;
using ITnCare.Commission.InputModels;
using ITnCare.Commission.Services;
using ITnCare.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddITnCareService(builder.Configuration, itncareBuilder =>
{
    itncareBuilder.AddServiceDiscovery();
    itncareBuilder.AddHealthCheck();
    itncareBuilder.AddTracing();
});

builder.Services.AddSingleton<ICommissionCalculator, CommissionCalculator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseITnCareService();

app.MapPost("/calculate", (CalculateInputModel calculateInput, ICommissionCalculator _commissionCalculator) =>
{
    var commission = _commissionCalculator.Calculate(calculateInput.OrderSide, calculateInput.Price, calculateInput.Quantity, calculateInput.CustomerOrigin);
    return commission;
})
.WithName("Calculate");

app.Run();