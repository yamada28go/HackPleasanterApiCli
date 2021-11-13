/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 * */

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
