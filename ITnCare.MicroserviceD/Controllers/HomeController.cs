using Microsoft.AspNetCore.Mvc;

namespace ITnCare.MicroserviceD.Controllers
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
            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);

            using (var httpClient = new HttpClient())
            {
                await httpClient.GetAsync("https://microsoft.com/");
            }

            return Guid.NewGuid().ToString();
        }
    }
}