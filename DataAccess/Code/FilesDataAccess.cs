using System;
using System.Collections.Generic;
using System.Xml;
using DataAccess.Properties;
using System.Linq;

namespace DataAccess
{
    public sealed class FilesDataAccess : IFilesDataAccess
    {
        public string Get(string id)
        {
            return Resources.ResourceManager.GetString(id);
        }
    }
}