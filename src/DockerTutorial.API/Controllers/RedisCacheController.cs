

using DockerTutorial.API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace BackendAPI.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class RedisCacheController : ControllerBase
{
    private readonly IDistributedCache _cache;

    public RedisCacheController(IDistributedCache cache)
    {
        _cache = cache;
    }

    [HttpPost]
    public async Task<IActionResult> Set(RedisSetViewModel  request)
    {
        var byteText = Encoding.UTF8.GetBytes(request.Value);
        await _cache.SetAsync(request.Key, byteText);
        
        return Ok();
    }
    
    [HttpGet]
    [Route("{key}")]
    public async Task<IActionResult> Get(string key)
    {
       var  cachedTimeUTC = "Cached Time Expired";
        var encodedCachedTimeUTC = await _cache.GetAsync(key);

        if (encodedCachedTimeUTC != null)
        {
            cachedTimeUTC = Encoding.UTF8.GetString(encodedCachedTimeUTC);
        }
        
        return Ok(cachedTimeUTC);
    }
}