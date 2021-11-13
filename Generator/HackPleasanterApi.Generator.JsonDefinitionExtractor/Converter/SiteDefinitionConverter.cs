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

using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.Library.Models.CSV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter
{
    /// <summary>
    /// サイト定義を生成する
    /// </summary>
    class SiteDefinitionConverter
    {
        /// <summary>
        /// サイト定義形式に変換
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public IEnumerable<SiteDefinition> Convert(ExportJsonDefinition.Rootobject src)
        {
            return src.Sites.Select(x =>
            {
                return new SiteDefinition
                {
                    SiteId = x.SiteId,
                    Title = x.Title
                };
            })
            .GroupBy(e => e.SiteId)
            .Select(e => e.FirstOrDefault())
            .Where(e => e != null)
            .OrderBy(e => e.SiteId);
        }
    }
}
