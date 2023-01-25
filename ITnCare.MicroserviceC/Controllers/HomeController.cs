using Microsoft.AspNetCore.Mvc;

namespace ITnCare.MicroserviceC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<string> Get(CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);

            using (var httpClient = new HttpClient())
            {
                await httpClient.GetAsync("https://google.com/");
            }

            return Guid.NewGuid().ToString();
        }
    }
}