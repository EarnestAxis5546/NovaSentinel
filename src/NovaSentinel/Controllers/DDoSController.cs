using Microsoft.AspNetCore.Mvc;
using NovaSentinel.Services;

namespace NovaSentinel.Controllers
{
    [Route("api/novasentinel")]
    [ApiController]
    public class DDoSController : ControllerBase
    {
        private readonly DDoSService _ddosService;

        public DDoSController(DDoSService ddosService)
        {
            _ddosService = ddosService;
        }

        [HttpGet("status")]
        public IActionResult GetStatus()
        {
            var stats = _ddosService.GetTrafficStats();
            return Ok(stats);
        }

        [HttpPost("block/{ip}")]
        public IActionResult BlockIp(string ip)
        {
            _ddosService.BlockIp(ip);
            return Ok($"IP {ip} blocked successfully.");
        }
    }
}