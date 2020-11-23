using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Reader
{
    /// <summary>
    /// JSON定義を読み込む
    /// </summary>
    class JsonReader
    {
        private string ReadAllLine(string filePath, string encodingName)
        {
            using (StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(encodingName)))
            {
                string allLine = sr.ReadToEnd();
                return allLine;
            }
        }

        public T ReadAll<T>(string filePath)
        {
            string jsonStr = ReadAllLine(filePath, "utf-8");
            var jsonData = JsonSerializer.Deserialize<T>(jsonStr);

            return jsonData;
        }


    }
}
