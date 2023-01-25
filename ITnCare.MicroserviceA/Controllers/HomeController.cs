using ITnCare.Service.Framework;
using Microsoft.AspNetCore.Mvc;

namespace ITnCare.MicroserviceA.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IServiceResolver _serviceResolver;

        public HomeController(ILogger<HomeController> logger, IServiceResolver serviceResolver)
        {
            _logger = logger;
            _serviceResolver = serviceResolver;
        }

        [HttpGet]
        public async Task<decimal> Get(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

            using(var httpClient = new HttpClient())
            {
                var microserviceBUri = await _serviceResolver.ResolveServiceUriAsync("ITnCare.MicroserviceB", "/home", cancellationToken);
                var microserviceBResponse = await httpClient.GetAsync(microserviceBUri);
                var microserviceBResult = await microserviceBResponse.Content.ReadAsStringAsync();

                var microserviceDUri = await _serviceResolver.ResolveServiceUriAsync("ITnCare.MicroserviceD", "/home", cancellationToken);
                var microserviceDResponse = await httpClient.GetAsync(microserviceDUri);
                var microserviceDResult = await microserviceDResponse.Content.ReadAsStringAsync();
            }

            return 75000;
        }
    }
}