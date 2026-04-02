using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Infrastructure.Data
{
    public class YandexStorageSettings
    {
        public string? AccessKey { get; set; }
        public string? SecretKey { get; set; }
        public string? Bucket { get; set; }
        public string? Endpoint { get; set; }
    }
}
