using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace GamLib.Files
{
    public class Reading
    {
        private readonly Xml _xml = new Xml();

        public List<string> ReadLines(Stream fileStream)
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(fileStream))
            {
                var line = reader.ReadLine();
                var width = line.Length;
                while (line != null)
                {
                    lines.Add(line);
                    if (line.Length != width)
                        throw new Exception($"The length of line {lines.Count} is different from all preceeding lines.");
                    line = reader.ReadLine();
                }
            }

            return lines;

        }

        public string ReadFileAsString(FileStream fileStream)
        {
            var result = new StringBuilder();
            try
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    result.Append(reader.ReadToEnd());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR: File could not be read!");
                Console.WriteLine("Exception Message: " + e.Message);
            }
            return result.ToString();
        }
    }
}
