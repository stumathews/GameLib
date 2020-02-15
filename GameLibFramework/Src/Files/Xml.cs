using System.IO;
using System.Xml.Serialization;

namespace GamLib.Files
{
    public class Xml
    {
        public T DeserializeFile<T>(FileStream fileStream)
        {
            return (T)new XmlSerializer(typeof(T)).Deserialize(fileStream);
        }
    }
}