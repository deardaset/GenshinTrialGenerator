using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Core.Interfaces
{
    public interface IStorageService
    {
        public Task<string> UploadAsync(Stream stream, string fileName, string contentType, string folder);
        public Task DeleteAsync(string fileUrl);
    }
}
