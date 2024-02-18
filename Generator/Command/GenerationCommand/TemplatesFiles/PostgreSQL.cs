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
using System.Runtime.Serialization;
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles.Settings;
using Zx;

namespace HackPleasanterApi.Generator.GenerationCommand.TemplatesFiles
{
    public class PostgreSQL : DownloaderBase<PostgreSQLSettings>
    {
        public PostgreSQL(string refLocalPath, PostgreSQLSettings ver) : base("https://github.com/yamada28go/HackPleasanterApi.PostgreSQL", refLocalPath, ver)
        {
        }

        protected override async Task PostProcessingImp(string path)
        {
            //   throw new NotImplementedException();
            await Task.CompletedTask;

            // 指定したフォルダ内のすべてのファイルを取得
            foreach (string file in Directory.EnumerateFiles(path, "*.sql", SearchOption.AllDirectories))
            {

                logger.Debug($"整形対象 : ${file}");
                var r = await $"sql-formatter {file} --fix -l postgresql";
                logger.Debug($"cmd out : ${r}");

            }
        }


    }
}

