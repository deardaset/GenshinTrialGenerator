using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.Interfaces.BossServices
{
    public interface IDeleteBossService
    {
        public Task RunAsync(Guid guid);
    }
}
