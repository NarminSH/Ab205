using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PurpleBuzzPr.Models;
using System.Drawing;

namespace PurpleBuzzPr.Utilities
{
    public static class FileManager
    {

        public static bool CheckType(this IFormFile formFile) 
            => formFile.ContentType.Contains("image");

        public static bool CheckSize(this IFormFile formFile, int size)
        {
            if (formFile.Length > size * 1024 * 1024)
            {
                return false;
            }
            return true;

        }

        public static string Upload(this IFormFile formFile,string envPath, string folder)
        {
            string path = envPath + folder;
            string newFilename = ChangeFileName(formFile.FileName);

            using (FileStream fileStream = new FileStream(path + newFilename, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return newFilename;
        }
        private static string ChangeFileName(string oldFilename)
        {
            string fileName = Path.GetFileNameWithoutExtension(oldFilename);
            if (fileName.Length > 50)
            {
                fileName = fileName.Substring(0, 49);
            }

            return Guid.NewGuid().ToString() + fileName + Path.GetExtension(oldFilename);
        }
        
    }
}
