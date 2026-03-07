using GenshinTrialGenerator.Application.DTOs.Hero;
using GenshinTrialGenerator.Application.Interfaces.HeroServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenshinTrialGenerator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateHeroAsync([FromServices] ICreateHeroService service, [FromForm] CreateHeroRequest request)
        {
            var result = await service.RunAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHeroes([FromServices] IGetAllHeroesService service)
        {
            var result = await service.RunAsync();
            return Ok(result);
        }

        [HttpPut]
        [Route("{guid}")]
        public async Task<IActionResult> UpdateHeroAsync([FromRoute] Guid guid, [FromServices] IUpdateHeroService service, UpdateHeroRequest request)
        {
            var result = await service.RunAsync(guid, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{guid}")]
        public async Task<IActionResult> DeleteHeroAsync([FromRoute] Guid guid, [FromServices] IDeleteHeroService service)
        {
            await service.RunAsync(guid);
            return Ok();
        }
    }
}
