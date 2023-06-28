namespace SocialNetwork.BLL.Contracts
{
    public interface IFileService
    {
        public string ModifyFilePath(string fullPath);
        public string DeleteFile(string fullPath);
        public string GetFileType(string fileName);
    }
}
