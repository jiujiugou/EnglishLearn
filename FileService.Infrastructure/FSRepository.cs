using FileService.Domain;
using FileService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileService.Infrastructure
{
    internal class FSRepository : IFSRepository
    {
        private readonly FSDbContext dbContext;
        public Task<UploadedItem?> FindFileAsync(long fileSize, string sha256Hash)
        {
            return dbContext.UploadItems.FirstOrDefaultAsync(s=>s.FileSizeInBytes == fileSize
            &&s.FileSHA256Hash==sha256Hash);
        }
    }
}
