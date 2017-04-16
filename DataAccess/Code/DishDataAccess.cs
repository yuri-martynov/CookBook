using System.Xml;
using DataAccess.Properties;

namespace DataAccess
{
    public sealed class DishDataAccess : IDishDataAccess
    {
        public IDish Get(string id)
        {
            var text = Resources.ResourceManager.GetString(id);
            var xml = new XmlDocument();
            xml.LoadXml(text);
            return new Dish()
            {
                Title = xml.DocumentElement.GetAttribute("title"),
                Recipe = xml.DocumentElement["recipe"].InnerXml
            };
        }

        private sealed class Dish : IDish
        {
            public string Title { get; set; }
            public string Recipe { get; set; }
        }
    }
}