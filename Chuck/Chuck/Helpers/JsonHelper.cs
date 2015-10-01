using System.IO;
using Newtonsoft.Json;

namespace Chuck.Helpers
{
    /// <summary>
    ///     Generic[T] JSON helper to avoid duplication
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonHelper<T>
    {
        /// <summary>
        ///     Load an object of type T from a JSON file
        /// </summary>
        /// <param name="filename">the JSON file location</param>
        /// <returns></returns>
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

        /// <summary>
        ///     Save to a JSON file an object of type T.
        /// </summary>
        /// <param name="item">The object to save</param>
        /// <param name="filename">the JSON file location</param>
        public static void SaveToFile(T item, string filename)
        {
            using (var sw = new StreamWriter(filename))
            {
                sw.Write(JsonConvert.SerializeObject(item,new JsonSerializerSettings(){ ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));
                sw.Close();
            }
        }
    }
}
