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
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;
using System.IO;

namespace HackPleasanterApi.Generator.SimpleCommand.Logic
{
    public class DefinitionExtractor
    {

        private string srcFilePath;
        private string srcFileName;
        private string exportDirPath;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="srcFileName"> 読み込み対象となるjsonファイル名 </param>
        /// <param name="exportDirPath"> 出力対象のパス名 </param>
        /// <param name=""></param>
		public DefinitionExtractor(
            string srcFilePath,
            string srcFileName,
            string exportDirPath

            )
        {
            this.srcFilePath = srcFilePath;
            this.srcFileName = srcFileName;
            this.exportDirPath = exportDirPath;

        }

        public Tuple<string, DefinitionExtractorConfig> DoExport()
        {

            var exportPath = Path.Combine(this.srcFilePath, "Export");
            Directory.CreateDirectory(exportPath);

            // 設定ファイル名を作成
            var config = new DefinitionExtractorConfig
            {
                Input = new DefinitionExtractorConfig.Definition.Input
                {

                    SiteExportDefinitionFile = this.srcFileName
                },

                Output = new DefinitionExtractorConfig.Definition.Output
                {

                    UseDescriptionAsVariableName = true,
                    ExportAllSites = true,
                    InterfaceDefinitionFile = Path.Combine(exportPath, "Interface.csv"),
                    SiteDefinitionFile = Path.Combine(exportPath, "Site.csv"),
                    Encoding = "Utf-8"

                }

            };

            DirectoryInfo workDir = new DirectoryInfo(this.exportDirPath);

            var sx = new HackPleasanterApi.Generator.JsonDefinitionExtractor.Exporter();
            sx.DoExporter(workDir, config);

            return Tuple.Create(this.exportDirPath, config);
        }


    }
}

