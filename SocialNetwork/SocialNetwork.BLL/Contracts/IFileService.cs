using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.Contracts
{
    public interface IFileService
    {
        public string ModifyFilePath(string fullPath);
        public string DeleteFile(string fullPath);
        public string GetFileType(string fileName);
    }
}
