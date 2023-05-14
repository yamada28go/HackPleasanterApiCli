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

using System.Text;
using System.Text.Json;
using CsvHelper;
using HackPleasanterApi.Generator.CodeGenerator.Configs;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;
using HackPleasanterApi.Generator.SimpleCommand.Helper;
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles;
using LibGit2Sharp;
using NLog;

namespace HackPleasanterApi.Generator.SimpleCommand;

class Program
{
    static void Main(string[] args)
    {




        TempResource.FolderContext(path =>
        {



            path = "/Volumes/WorkSSD/work/testTTT/workDir";
#if false
            var pg = new PostgreSQL();
            pg.DownLoad(path,"0.0.1");

#endif

            Console.WriteLine("Hello, World!");


#if false
            var repoUrl = "https://github.com/yamada28go/HackPleasanterApi.PostgreSQL";
            var repoPath = path;

            var co = new CloneOptions();
            //co.CredentialsProvider = (_url, _user, _cred) => new UsernamePasswordCredentials { Username = "ここにユーザー名を入力", Password = "ここにパスワードを入力" };

            co.BranchName = "release/0.0.1";

            Repository.Clone(repoUrl, repoPath, co);


            // Open the repository
            using (var repo = new Repository("https://github.com/yamada28go/HackPleasanterApi.PostgreSQL"))
            {
                // Check out the "master" branch
                //                repo.Checkout("master");


                Console.WriteLine("Hello, World!");

            }
#endif

#if false

            Console.WriteLine("Hello, World!");

            var exportPath = Path.Combine(path, "Export");
            Directory.CreateDirectory(exportPath);

            // 設定ファイル名を作成
            var config = new DefinitionExtractorConfig {
                Input = new DefinitionExtractorConfig.Definition.Input {

                    SiteExportDefinitionFile = "export.json"
                },

                Output = new DefinitionExtractorConfig.Definition.Output {

                    UseDescriptionAsVariableName = true,
                    ExportAllSites = true,
                    InterfaceDefinitionFile = Path.Combine(exportPath, "Interface.csv"),
SiteDefinitionFile = Path.Combine(exportPath, "Site.csv"),
                    Encoding="Utf-8"

                }

            };

            DirectoryInfo workDir = new DirectoryInfo("/Volumes/WorkSSD/work/testTTT/");

            var sx = new HackPleasanterApi.Generator.JsonDefinitionExtractor.Exporter();
            sx.DoExporter(workDir, config);


#endif

            var workDir = "/Volumes/WorkSSD/work/testTTT/";

            // 設定情報を抽出する
            var ex = new Logic.DefinitionExtractor(path,"export.json", workDir);
           var exportCfg =  ex.DoExport();

            var pg = new ScriptTs();
            var tp = pg.DownLoad(path, "0.0.1");


            //設定ファイルがあるパスを取得する
            var files = Directory.GetFiles(tp, "*", SearchOption.AllDirectories)
            .Where(x=>x.Contains("CodeGeneratorConfig.xml"))
            .ToList();


            System.IO.FileInfo codeGeneratorConfigPath = new System.IO.FileInfo(files[0]);

            DoGenerate(new DirectoryInfo(workDir), codeGeneratorConfigPath , exportCfg);
        });


    }


    /// <summary>
    /// ロガー
    /// </summary>
    private static Logger logger = LogManager.GetCurrentClassLogger();


    /// <summary>
    /// データの生成を行う
    /// </summary>
    /// <param name="workDir"></param>
    /// <param name="codeGeneratorConfigPath"></param>
    private static  void DoGenerate(DirectoryInfo workDir, FileInfo codeGeneratorConfigPath , Tuple<string, DefinitionExtractorConfig> eportConfig)
    {
#if false
        // XML形式としてデフォルト設定を生成する
        var cfgName = Path.Combine(workDir.FullName, cfgFile.Name);
        logger.Debug($"OutFile Path : {cfgName}");
#endif

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

#if true

        var l = (new HackPleasanterApi.Generator.CodeGenerator.Loder.CSVLoader()).DoLoad(c);

        logger.Info($"CSVから読み取られた対象とするサイト数 : ${l.Count()}");
        logger.Debug($"読み取り対象データ ダンプ : ${JsonSerializer.Serialize(l)}");

        var ct = new HackPleasanterApi.Generator.CodeGenerator.GenerationContext
        {
            Sites = l
        };

        // コード定義を生成する
        (new HackPleasanterApi.Generator.CodeGenerator.Generators.Generator()).DoGenerae(c, ct);

#endif
    }
}

