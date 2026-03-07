using GenshinTrialGenerator.Application.DTOs.Boss;
using GenshinTrialGenerator.Application.Interfaces.BossServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GenshinTrialGenerator.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BossController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateBossAsync([FromServices] ICreateBossService service, [FromForm] CreateBossRequest request)
        {
            var result = await service.RunAsync(request);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBossesAsync([FromServices] IGetAllBossesService service)
        {
            var result = await service.RunAsync();
            return Ok(result);
        }

        [HttpPut]
        [Route("{guid}")]
        public async Task<IActionResult> UpdateBossAsync([FromRoute] Guid guid, [FromServices] IUpdateBossService service, [FromForm] UpdateBossRequest request)
        {
            var result = await service.RunAsync(guid, request);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{guid}")]
        public async Task<IActionResult> DeleteBossAsync([FromRoute] Guid guid, [FromServices] IDeleteBossService service)
        {
            await service.RunAsync(guid);
            return Ok();
        }
    }
}
