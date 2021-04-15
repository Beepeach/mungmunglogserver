using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;

namespace mungmunglogServer.Controllers
{
    public class BlobController : Controller
    {
        public static async Task<string> UploadImage(IFormFile file)
        {
            try
            {
                var account = new CloudStorageAccount(new StorageCredentials("mungmunglogstorage", "sHVj01H5c6bd4JCeyCAPZGn4/TI/kOa03DKmUuKC40kv1U167Uaew9RhuxTuDjNXE6ChF53TNthKQCElDYucsQ=="), true);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("main");

                var fileName = Guid.NewGuid().ToString() + file.FileName.Substring(file.FileName.LastIndexOf("."));
                var blob = container.GetBlockBlobReference(fileName);
                await blob.UploadFromStreamAsync(file.OpenReadStream());

                return blob.Uri.AbsoluteUri;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        public static async Task Delete(string path)
        {
            try
            {
                var account = new CloudStorageAccount(new StorageCredentials("mungmunglogstorage", "sHVj01H5c6bd4JCeyCAPZGn4/TI/kOa03DKmUuKC40kv1U167Uaew9RhuxTuDjNXE6ChF53TNthKQCElDYucsQ=="), true);
                var client = account.CreateCloudBlobClient();
                var container = client.GetContainerReference("main");

                var id = path.Substring(path.LastIndexOf("/") + 1);
                var blob = container.GetBlockBlobReference(id);

                await blob.DeleteIfExistsAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}