using ITnCare.Gateway.RequestModels.Commission;
using ITnCare.Service;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;

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

builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot().AddConsul();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/calculate-commission", () =>
{
});

app.MapPost("/commission/calculate", (CalculateModel model) =>
{
});

app.UseOcelot().Wait();
app.UseITnCareService();

app.Run();