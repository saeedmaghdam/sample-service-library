using ITnCare.OM.Incoming.InputModels;
using ITnCare.OM.Incoming.RequestModels.Commission;
using ITnCare.Service;
using ITnCare.Service.Framework;
using Newtonsoft.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddITnCareService(builder.Configuration, itncareBuilder =>
{
    itncareBuilder.AddServiceDiscovery();
    itncareBuilder.AddServiceResolver();
    itncareBuilder.AddHealthCheck();
    itncareBuilder.AddTracing();
    itncareBuilder.AddCaching("ITnCare.CM");
});

builder.Services.AddHttpClient("ITnCareServices", client =>
{
    client.DefaultRequestHeaders.Add("Accept", "application/json");
}).UseAdvancedCircuitBreakerPolicy(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseITnCareService();

app.MapPost("/verify", async (VerifyInputModel model, IHttpClientFactory httpClientFactory, IServiceResolver serviceResolver, CancellationToken cancellationToken) =>
{
    using var httpClient = httpClientFactory.CreateClient("ITnCareServices");

    var customerOriginUri = await serviceResolver.ResolveServiceUriAsync("ITnCare.CM", $"/origin", cancellationToken);
    var customerOriginUrl = $"{customerOriginUri}?tradingCode={model.TradingCode}";
    var customerOriginResponse = await httpClient.GetAsync(customerOriginUrl, cancellationToken);
    var customerOriginResult = await customerOriginResponse.Content.ReadAsStringAsync();

    var calculateUri = await serviceResolver.ResolveServiceUriAsync("ITnCare.Commission", $"/calculate", cancellationToken);
    var calculateRequestModel = new CalculateModel
    {
        OrderSide = model.OrderSide,
        Price = model.Price,
        Quantity = model.Quantity
    };
    var calculateRequestModelJsonModel = JsonConvert.SerializeObject(calculateRequestModel);
    var calculateRequestContent = new StringContent(calculateRequestModelJsonModel, Encoding.UTF8, "application/json");
    var calculateResponse = await httpClient.PostAsync(calculateUri, calculateRequestContent, cancellationToken);

    //var values = new Dictionary<string, string>
    //{
    //    {"TradingCode", model.TradingCode},
    //    {"OrderSide", model.OrderSide.ToString()},
    //    {"Price", model.Price.ToString()},
    //    {"Quantity", model.Quantity.ToString()}
    //};
    //var calculateResponse = await httpClient.PostAsJsonAsync(calculateUri, values, cancellationToken);
    var calculateResult = await calculateResponse.Content.ReadAsStringAsync();

    return calculateResult;
})
.WithName("Verify");

app.Run();