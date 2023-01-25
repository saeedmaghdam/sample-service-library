using ITnCare.Service.Framework;
using Microsoft.AspNetCore.Mvc;

namespace ITnCare.MicroserviceB.Controllers
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
        public async Task<string> Get(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), cancellationToken);

            using (var httpClient = new HttpClient())
            {
                //var microserviceCUri = await _serviceDiscovery.ResolveServiceUriAsync("ITnCare.MicroserviceC", "/home", cancellationToken);
                //var microserviceCResponse = await httpClient.GetAsync(microserviceCUri);
                //var microserviceCResult = await microserviceCResponse.Content.ReadAsStringAsync();

                //var microserviceDUri = await _serviceDiscovery.ResolveServiceUriAsync("ITnCare.MicroserviceD", "/home", cancellationToken);
                //var microserviceDResponse = await httpClient.GetAsync(microserviceDUri);
                //var microserviceDResult = await microserviceDResponse.Content.ReadAsStringAsync();
            }

            return Guid.NewGuid().ToString();
        }
    }
}