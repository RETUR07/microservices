using Microsoft.AspNetCore.Http;
using ChatAPI.Entities.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChatAPI.Application.Services
{
    public static class BlobConverters
    {
        public static async Task<Blob> FormFileToBlobAsync(this IFormFile formfile)
        {
            using (var ms = new MemoryStream())
            {
                if (formfile == null) return null;

                Blob blob = new();

                await formfile.CopyToAsync(ms);
                blob.Filename = formfile.FileName;
                blob.ContentType = formfile.ContentType;
                return blob;
            }
        }

        public static Blob FormFileToBlob(this IFormFile formfile)
        {
            using (var ms = new MemoryStream())
            {
                if (formfile == null) return null;

                Blob blob = new();

                formfile.CopyTo(ms);
                blob.Filename = formfile.FileName;
                blob.ContentType = formfile.ContentType;
                return blob;
            }
        }

        public static async Task<List<Blob>> FormFilesToBlobsAsync(this IEnumerable<IFormFile> formfiles)
        {
            List<Blob> blobs = new();
            foreach (IFormFile ff in formfiles)
                if (ff != null)
                {
                    blobs.Add(await ff.FormFileToBlobAsync());
                }
            return blobs;
        }

        public static List<Blob> FormFilesToBlobs(this IEnumerable<IFormFile> formfiles)
        {
            List<Blob> blobs = new();
            foreach (IFormFile ff in formfiles)
                if (ff != null)
                {
                    blobs.Add(ff.FormFileToBlob());
                }
            return blobs;
        }

        public static FileContentResult BlobToFileContentResult(this Blob blob, byte[] buffer)
        {
            var returnfile = new FileContentResult(buffer, blob.ContentType)
            {
                FileDownloadName = blob.Filename
            };
            return returnfile;
        }
    }
}
