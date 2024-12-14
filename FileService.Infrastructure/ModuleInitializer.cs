﻿using Commons;
using FileService.Domain;
using FileService.Infrastructure.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure
{
    class ModuleInitializer : IModuleInitializer
    {
        public void Initialize(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IStorageClient, SMBStorageClient>();
            //services.AddScoped<IStorageClient, UpYunStorageClient>();
            services.AddScoped<IStorageClient, MockCloudStorageClient>();
            services.AddScoped<IFSRepository, FSRepository>();
            services.AddScoped<FSDomainService>();
            services.AddHttpClient();
        }
    }
}
