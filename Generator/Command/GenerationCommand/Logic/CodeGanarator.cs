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
using System.IO;
using System.Text.Json;
using CsvHelper;
using HackPleasanterApi.Generator.CodeGenerator;
using HackPleasanterApi.Generator.CodeGenerator.Configs;
using HackPleasanterApi.Generator.CodeGenerator.Loder;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;
using NLog;

namespace HackPleasanterApi.Generator.GenerationCommand.Logic
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


        public void Generate(
            string workPath,
            Tuple<string, DefinitionExtractorConfig> exportCfg,
            HackPleasanterApi.Generator.GenerationCommand.TemplatesFiles.IDownloaderBase baseObj)
        {
            //テンプレートのパスを取得する
            Func<string> LF_getTemplatePath = () =>
            {
                if (false == string.IsNullOrWhiteSpace(baseObj.settingsBase?.DebugSettings?.TemplatePath))
                {
                    return baseObj?.settingsBase?.DebugSettings?.TemplatePath ?? "";
                }
                else
                {
                    return baseObj.DownLoad();
                }
            };


            // テンプレートを取得する
            //var tp = baseObj.DownLoad();
            var tp = LF_getTemplatePath();

            //   var tp = "/Volumes/WorkSSD/work/baseTT/HackPleasanterApi.ScriptTs";

            // テンプレート用のパスを取得する
            var outPath = Path.Combine(workPath, baseObj.repoName);
            Directory.CreateDirectory(outPath);

            //設定ファイルがあるパスを取得する
            var files = Directory.GetFiles(tp, "*", SearchOption.AllDirectories)
            .Where(x => x.Contains("CodeGeneratorConfig.xml"))
            .ToList();


            System.IO.FileInfo codeGeneratorConfigPath = new System.IO.FileInfo(files[0]);

            GenerateMaterials(new DirectoryInfo(outPath), codeGeneratorConfigPath, exportCfg);

            // 後処理を行う
            try
            {
                baseObj.PostProcessing(outPath).Wait();
            }
            catch (Exception exp)
            {
                logger.Debug($"後処理に失敗 : ${exp.Message}");
            }

        }

        /// <summary>
        /// データの生成を行う
        /// </summary>
        /// <param name="workDir"></param>
        /// <param name="codeGeneratorConfigPath"></param>
        private void GenerateMaterials(DirectoryInfo workDir, FileInfo codeGeneratorConfigPath, Tuple<string, DefinitionExtractorConfig> eportConfig)
        {
            //設定を読み込む
            var c = HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Deserialize<GeneratorConfig>(codeGeneratorConfigPath.FullName);

            // 出力ディレクトリは作業対象ディレクトリをベースパスとして動作させる
            c.OutputConfig.OutputDirectory = Path.Combine(workDir.FullName, c.OutputConfig.OutputDirectory);
            logger.Info($"生成結果出力ディレクトリ : ${c.OutputConfig.OutputDirectory}");

            // テンプレートパスは作業ディレクトリからの相対パスに存在するものとする
            foreach (var t in c.TemplateFiles)
            {
                System.IO.FileInfo f = new System.IO.FileInfo(t.TemplateFileName);
                t.TemplateFileName = Path.Combine(codeGeneratorConfigPath.DirectoryName!, f.Name);
                logger.Debug($"テンプレートファイルのパスを置き換え : ${t.TemplateFileName}");
            }

            // 入力ファイルに関して作業パスを相対パスに存在するものに変換する
            {
                c.InputFiles.InterfaceDefinitionFile = Path.Combine(eportConfig.Item1, eportConfig.Item2.Output.InterfaceDefinitionFile);
                logger.Debug($"インターフェース定義ファイルのパスを置き換え : ${c.InputFiles.InterfaceDefinitionFile}");
            }

            {
                c.InputFiles.SiteDefinitionFile = Path.Combine(eportConfig.Item1, eportConfig.Item2.Output.SiteDefinitionFile);
                logger.Debug($"サイト定義ファイルのパスを置き換え : ${c.InputFiles.SiteDefinitionFile}");
            }

            var l = (new HackPleasanterApi.Generator.CodeGenerator.Loder.CSVLoader()).DoLoad(c);

            logger.Info($"CSVから読み取られた対象とするサイト数 : ${l.Count()}");
            logger.Debug($"読み取り対象データ ダンプ : ${JsonSerializer.Serialize(l)}");

            var ct = new HackPleasanterApi.Generator.CodeGenerator.GenerationContext
            {
                Sites = l
            };

            // コード定義を生成する
            (new HackPleasanterApi.Generator.CodeGenerator.Generators.Generator()).DoGenerae(c, ct);
        }

    }
}

