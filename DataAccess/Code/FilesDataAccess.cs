using DataAccess.Properties;

namespace DataAccess
{
    public sealed class FilesDataAccess : IFilesDataAccess
    {
        public string Get(string id) => Resources.ResourceManager.GetString(id);
    }
}