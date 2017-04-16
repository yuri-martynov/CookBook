using Db.Properties;

namespace Db
{
    public static class Files
    {
        public static string get(string id) => Resources.ResourceManager.GetString(id);
    }
}