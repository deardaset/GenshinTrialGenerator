using AutoMapper;
using GenshinTrialGenerator.Application.DTOs;
using GenshinTrialGenerator.Application.DTOs.Boss;
using GenshinTrialGenerator.Application.Interfaces.BossServices;
using GenshinTrialGenerator.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Services.BossServices
{
    public class GetAllBossesService(IBossRepository repository, IMapper mapper) : IGetAllBossesService
    {
        public async Task<PagedResponse<BossDto>> RunAsync(GetDataOptionsRequest request)
        {
            var (bosses, total) = await repository.GetAllBossesAsync(request.Page, request.PageSize, request.Search, request.Sort, request.Element);

            var bossesDto = mapper.Map<List<BossDto>>(bosses);

            return new PagedResponse<BossDto>
            {
                Items = bossesDto,
                TotalCount = total
            };
        }
    }
}
