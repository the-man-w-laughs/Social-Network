using SocialNetwork.BLL.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Services.File
{
    internal class FileService : IFileService
    {
        public string DeleteFile(string fullPath)
        {
            throw new NotImplementedException();
        }

        public string GetFileType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();

            switch (extension)
            {
                // Images
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".gif":
                    return "image/gif";

                // Videos
                case ".mp4":
                    return "video/mp4";
                case ".avi":
                    return "video/x-msvideo";
                case ".mov":
                    return "video/quicktime";

                // Audios
                case ".mp3":
                    return "audio/mpeg";
                case ".wav":
                    return "audio/wav";
                case ".ogg":
                    return "audio/ogg";

                // Default
                default:
                    return "application/octet-stream";
            }
        }

        public string ModifyFilePath(string fullPath)
        {
            string directoryPath = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileName(fullPath);

            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            string fileExtension = Path.GetExtension(fileName);
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
            string newFileName = $"{fileNameWithoutExtension}_{timestamp}{fileExtension}";
            string filePath = Path.Combine(directoryPath, newFileName);
            return filePath;
        }
    }
}
