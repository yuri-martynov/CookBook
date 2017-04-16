using System.IO;
using Db.Properties;

namespace Db
{
    public static class Files
    {
        public static Stream getStream(string id) => Resources.ResourceManager.GetStream(id);
    }
}