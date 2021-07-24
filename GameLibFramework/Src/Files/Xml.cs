using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace GameLib.Files
{
    public class Xml
    {
        public static T DeserializeFile<T>(string sourceFileName)
        {

            // Create an instance of the XmlSerializer specifying type and namespace.
            XmlSerializer serializer = new
            XmlSerializer(typeof(T));

            // A FileStream is needed to read the XML document.
            FileStream fs = new FileStream(sourceFileName, FileMode.Open);
            XmlReader reader = XmlReader.Create(fs);

            // Declare an object variable of the type to be deserialized.
            T i;

            // Use the Deserialize method to restore the object's state.
            i = (T)serializer.Deserialize(reader);
            fs.Close();
            return i;
        }

        public static void SerializeObject<T>(string destFileName, T i)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            
            // Create an XmlTextWriter using a FileStream.
            Stream fs = new FileStream(destFileName, FileMode.Create);
            XmlWriter writer = new XmlTextWriter(fs, Encoding.Unicode);
            // Serialize using the XmlTextWriter.
            serializer.Serialize(writer, i);
            writer.Close();
        }
    }
}