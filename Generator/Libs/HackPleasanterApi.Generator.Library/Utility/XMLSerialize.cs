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
using System.Xml.Serialization;

namespace HackPleasanterApi.Generator.Library.Utility
{
    /// <summary>
    /// XML形式でシリアライズするユーティリティクラス
    /// </summary>
    public class XMLSerialize
    {
        /// <summary>
        /// 指定されたXML定義をシリアライズする
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="xmlFile"></param>
        public static void Serialize<T>(T obj, string xmlFile)
        {
            var xmlSerializer1 = new XmlSerializer(typeof(T));
            using (var streamWriter = new StreamWriter(xmlFile, false, Encoding.UTF8))
            {
                xmlSerializer1.Serialize(streamWriter, obj);
                streamWriter.Flush();
            }
        }

        /// <summary>
        /// 指定されたXML定義をデシリアライズ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="xmlFile"></param>
        public static T Deserialize<T>(string xmlFile) where T : new()
        {
            var xmlSerializer2 = new XmlSerializer(typeof(T));
            T result;
            var xmlSettings = new System.Xml.XmlReaderSettings()
            {
                CheckCharacters = false,
            };
            using (var streamReader = new StreamReader(xmlFile, Encoding.UTF8))
            using (var xmlReader
                    = System.Xml.XmlReader.Create(streamReader, xmlSettings))
            {
                result = (T)xmlSerializer2.Deserialize(xmlReader);
            }

            return result;
        }

    }
}
