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

using System.Collections.Generic;
using System.Linq;

using HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Reader;
using HackPleasanterApi.Generator.Library.Models.CSV;
using HackPleasanterApi.Generator.Library.Service;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;
using System.IO;
using NLog;
using System.Text;
using System.Text.RegularExpressions;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor
{
    /// <summary>
    /// 主要処理部分
    /// 
    /// </summary>
    public  class Exporter
    {

        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();


        private DefinitionExtractorConfig LoadConfig(string ConfgiPath)
        {
            var r = HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Deserialize<DefinitionExtractorConfig>(ConfgiPath);
            return r;
        }
        private class JsonDefinition
        {
            public IEnumerable<SiteDefinition> SiteDefinitionConverter;
            public IEnumerable<InterfaceDefinition> InterfaceDefinitionConverter;
        }

        /// <summary>
        /// JSON定義を生成する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private JsonDefinition ReadJsonDefinition(string path)
        {
            // 対象となるファイル群を読み取る
            var r = new JsonReader();
            var def = r.ReadAll<ExportJsonDefinition.Rootobject>(path);

            // データを変換して出力する
            var si = (new SiteDefinitionConverter()).Convert(def);

            var ifd = (new InterfaceDefinitionConverter()).Convert(def);

            return new JsonDefinition
            {
                InterfaceDefinitionConverter = ifd,
                SiteDefinitionConverter = si
            };
        }

        public void DoExporter(DirectoryInfo workDir, string configPath)
        {
            //設定ファイルを読み取る
            var config = LoadConfig(configPath);

            DoExporter(workDir, config);
        }



        public void DoExporter(DirectoryInfo workDir, DefinitionExtractorConfig config)
        {

            // サイト定義JSONを読み込む
            var readFileName = Path.Combine(workDir.FullName, config.Input.SiteExportDefinitionFile);
            logger.Info($"読み込み対象のサイト構成ファイル名 : {readFileName}");
            var r = ReadJsonDefinition(readFileName);

            // 変数名を説明で上書きするオプションを使っている場合
            // 変換を実行する
            if (true == config.Output.UseDescriptionAsVariableName)
            {
                r.InterfaceDefinitionConverter = r.InterfaceDefinitionConverter
                    .Where(e => null != e)
                    .Where(e =>
                    false == string.IsNullOrWhiteSpace(e.LabelText) ||
                    false == string.IsNullOrWhiteSpace(e.Description) || 
                    false == string.IsNullOrWhiteSpace(e.ColumnName)
                    )
                    .Select(x =>
                    {
                        // 値が存在しない場合、以下の順番で選択する
                        if (false == string.IsNullOrWhiteSpace(x.LabelText))
                        {
                            x.VariableName = HackPleasanterApi.Generator.Libraryrary.Utility.CharacterType.ReplaceInvalidChars(x.LabelText);
                        }
                        else if (false == string.IsNullOrWhiteSpace(x.Description))
                        {
                            x.VariableName = HackPleasanterApi.Generator.Libraryrary.Utility.CharacterType.ReplaceInvalidChars(x.Description);
                        }
                        else
                        {
                            x.VariableName = HackPleasanterApi.Generator.Libraryrary.Utility.CharacterType.ReplaceInvalidChars(x.ColumnName);
                        }
                            return x;
                    })
                    .Where(e => false == string.IsNullOrWhiteSpace(e.VariableName))
                    .ToList();

            }

            if (true == config.Output.UseSiteTitleAsVariableName)
            {
                r.SiteDefinitionConverter = r.SiteDefinitionConverter
                    .Select(x => {
                        x.SiteVariableName = HackPleasanterApi.Generator.Libraryrary.Utility.CharacterType.ReplaceInvalidChars(x.Title);
                        return x;
                    })
                    .ToList();
            }

            // 全サイトを出力対象として指定する
            if (true == config.Output.ExportAllSites) {
                {
                    r.SiteDefinitionConverter = r.SiteDefinitionConverter
                        .Select(x => {
                            x.IsTarget = true;
                            return x; })
                        .ToList();
                }

                {
                    r.InterfaceDefinitionConverter = r.InterfaceDefinitionConverter
                        .Select(x => {
                            x.IsTarget = true;
                            return x;
                        })
                        .ToList();
                }
            }

            // 読み込み結果をCSVで出力する
            {
                var of = Path.Combine(workDir.FullName, config.Output.SiteDefinitionFile);
                logger.Info($"出力対象のインターフェース定義名 : {of}");
                (new CSVExport()).WriteSiteDefinition(r.SiteDefinitionConverter, of,config.Output.Encoding);
            }
            {
                var of = Path.Combine(workDir.FullName, config.Output.InterfaceDefinitionFile);
                logger.Info($"出力対象のサイト定義名 : {of}");
                (new CSVExport()).WriteInterfaceDefinition(r.InterfaceDefinitionConverter, of, config.Output.Encoding);
            }
        }
    }
}
