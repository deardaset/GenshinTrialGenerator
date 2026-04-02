using Amazon.S3;
using Amazon.S3.Model;
using GenshinTrialGenerator.Core.Interfaces;
using GenshinTrialGenerator.Infrastructure.Data;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Infrastructure.Repositories
{
    public class StorageService : IStorageService
    {
        private readonly IAmazonS3 _s3;
        private readonly YandexStorageSettings _settings;
        public StorageService(IOptions<YandexStorageSettings> options)
        {
            _settings = options.Value;

            _s3 = new AmazonS3Client(
                _settings.AccessKey,
                _settings.SecretKey,
                new AmazonS3Config
                {
                    ServiceURL = _settings.Endpoint,
                    ForcePathStyle = true
                });
        }
        public async Task<string> UploadAsync(Stream stream, string fileName, string contentType, string folder)
        {
            var ext = Path.GetExtension(fileName).ToLower();
            var key = $"{folder}/{Guid.NewGuid()}{ext}";

            await _s3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = _settings.Bucket,
                Key = key,
                InputStream = stream,
                ContentType = contentType,
                CannedACL = S3CannedACL.PublicRead
            });

            return $"{_settings.Endpoint}/{_settings.Bucket}/{key}";
        }

        public async Task DeleteAsync(string fileUrl)
        {
            var key = fileUrl.Replace($"{_settings.Endpoint}/{_settings.Bucket}/", "");

            await _s3.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _settings.Bucket,
                Key = key
            });
        }
    }
}
