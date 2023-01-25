using ITnCare.CM;
using ITnCare.CM.Bootstrapper;
using ITnCare.CM.Framework;
using ITnCare.CM.Services;
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
    itncareBuilder.AddCaching();
});

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddSingleton<ICacheHelper, CacheHelper>();
builder.Services.AddSingleton<CacheBootstrapper>();

builder.Services.AddHostedService<Worker>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseITnCareService();

app.MapGet("/origin", async (string tradingCode, ICacheHelper cacheHelper, CancellationToken cancellationToken) =>
{
    var origin = await cacheHelper.GetTradingCodeOrigin(tradingCode, cancellationToken);
    if (origin is null)
        throw new Exception("Customer not found.");

    return origin.Value;
})
.WithName("Origin");

app.Run();