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
using System.Text.Json;
using CsvHelper;
using HackPleasanterApi.Generator.CodeGenerator;
using HackPleasanterApi.Generator.CodeGenerator.Configs;
using HackPleasanterApi.Generator.CodeGenerator.Loder;
using NLog;

namespace HackPleasanterApi.Generator.SimpleCommand.Logic
{
	public class CodeGanarator
	{
		public CodeGanarator()
		{
		}

        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();


        /// <summary>
        /// データの生成を行う
        /// </summary>
        /// <param name="workDir"></param>
        /// <param name="cfgFile"></param>
        private static void DoGenerate(DirectoryInfo workDir, FileInfo cfgFile, string configPath)
        {
            // XML形式としてデフォルト設定を生成する
            var cfgName = Path.Combine(workDir.FullName, cfgFile.Name);
            logger.Debug($"OutFile Path : {cfgName}");

            //設定を読み込む
            var c = HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Deserialize<GeneratorConfig>(configPath);

            // 出力ディレクトリは作業対象ディレクトリをベースパスとして動作させる
            c.OutputConfig.OutputDirectory = Path.Combine(workDir.FullName, c.OutputConfig.OutputDirectory);
            logger.Info($"生成結果出力ディレクトリ : ${c.OutputConfig.OutputDirectory}");

            // テンプレートパスは作業ディレクトリからの相対パスに存在するものとする
            foreach (var t in c.TemplateFiles)
            {
                t.TemplateFileName = Path.Combine(workDir.FullName, t.TemplateFileName);
                logger.Debug($"テンプレートファイルのパスを置き換え : ${t.TemplateFileName}");
            }

            // 入力ファイルに関して作業パスを相対パスに存在するものに変換する
            {
                c.InputFiles.InterfaceDefinitionFile = Path.Combine(workDir.FullName, c.InputFiles.InterfaceDefinitionFile);
                logger.Debug($"インターフェース定義ファイルのパスを置き換え : ${c.InputFiles.InterfaceDefinitionFile}");
            }

            {
                c.InputFiles.SiteDefinitionFile = Path.Combine(workDir.FullName, c.InputFiles.SiteDefinitionFile);
                logger.Debug($"サイト定義ファイルのパスを置き換え : ${c.InputFiles.SiteDefinitionFile}");
            }

#if true

            var l = (new CSVLoader()).DoLoad(c);

            logger.Info($"CSVから読み取られた対象とするサイト数 : ${l.Count()}");
            logger.Debug($"読み取り対象データ ダンプ : ${JsonSerializer.Serialize(l)}");

            var ct = new GenerationContext
            {
                Sites = l
            };

            // コード定義を生成する
            (new HackPleasanterApi.Generator.CodeGenerator.Generators.Generator()).DoGenerae(c, ct);

#endif
        }

    }
}

