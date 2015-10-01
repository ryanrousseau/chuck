using Chuck.Core.Git;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Chuck.Helpers
{
    public class JsonHelper<T>
    {
        public static T FromFile(string filename)
        {
            T item;

            using (var sr = new StreamReader(filename))
            {
                item = JsonConvert.DeserializeObject<T>(sr.ReadToEnd());
                sr.Close();
            }

            return item;
        }

        public static void SaveToFile(T item, string filename)
        {
            using (var sw = new StreamWriter(filename))
            {
                sw.Write(JsonConvert.SerializeObject(item));
                sw.Close();
            }
        }
    }
}
