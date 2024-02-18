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
using System.CommandLine;
using System.CommandLine.Invocation;
using HackPleasanterApi.Generator.GenerationCommand.Helper;
using HackPleasanterApi.Generator.GenerationCommand.TemplatesFiles;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles.Settings;
using NLog;

namespace HackPleasanterApi.Generator.GenerationCommand.CallableCommand
{
    public class GenerationCommandDef
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetCommandName()
        {
            return "Generation";
        }

        /// <summary>
        /// 処理対象のコマンドを設定する
        /// </summary>
        /// <returns></returns>
        public static Command MakeCommand()
        {
            var cmd = new Command(GenerationCommandDef.GetCommandName());
            cmd.AddArgument(new Argument<FileInfo>(
            "ExportFileName",
            description: "Pleasanterから生成されたサイトファイル名"
            ));
            cmd.Description = "Pleasanterから生成されたサイトファイルからグルーコードを生成します。";

            // オプションを定義する
            var option = new Option<string>(
                new[] { "--config", "-c" },// オプション名
                getDefaultValue: () => String.Empty,
                description: "生成動作設定用の動作設定ファイル");  // 説明
            cmd.AddOption(option);

            cmd.Handler = CommandHandler.Create<DirectoryInfo, FileInfo, string?>((WorkingDirectory, ExportFileName, config) =>
            {
                logger.Debug($"On {GenerationCommandDef.GetCommandName()} Start!");
                logger.Debug($"On {GenerationCommandDef.GetCommandName()} WorkingDirectory: {WorkingDirectory}");
                logger.Debug($"On {GenerationCommandDef.GetCommandName()} ExportFileName: {ExportFileName}");

                // tempディレクトリで処理する
                TempResource.FolderContext(tempPath =>
                {

                    // 出力対象のファイルパス
                    var workDir = WorkingDirectory.FullName;

                    // 設定情報を抽出する
                    // 抽出するファイル名はworkDirから相対パスで取得する
                    var ex = new Logic.DefinitionExtractor(workDir, ExportFileName.Name, tempPath);
                    var exportCfg = ex.DoExport();

                    // デフォルトの設定値を作成
                    var tc = LoadGenerationSettings(WorkingDirectory, config);

                    // 生成器を作る
                    var g = CreateDownloaders(tc, tempPath);

                    // 個別のコードを生成する
                    foreach (var i in g)
                    {
                        // 生成処理を実行
                        var gen = new HackPleasanterApi.Generator.GenerationCommand.Logic.CodeGanarator();
                        gen.Generate(workDir, exportCfg, i);
                    }
                });

                logger.Debug($"On {GenerationCommandDef.GetCommandName()} End!");
            });

            return cmd;
        }

        private static GenerationSettings LoadGenerationSettings(DirectoryInfo workingDirectory, string? path)
        {

            try
            {
                if (false == string.IsNullOrWhiteSpace(path))
                {

                    var filePath = Path.Combine(workingDirectory.FullName, path);
                    logger.Info($"生成動作設定用の動作設定ファイルを読み取ります。 : {filePath}");
                    var r = HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Deserialize<GenerationSettings>
                        (filePath);
                    return r;
                }

            }
            catch (Exception exp)
            {
                logger.Error($"生成動作設定用の動作設定ファイルの読み取りに失敗しました。 : {path}");
                logger.Error(exp.Message);
                throw;
            }
            return GenerationSettings.MakeDefault();

        }

        private static List<IDownloaderBase> CreateDownloaders(GenerationSettings tc, string tempPath)
        {
            // IDownloaderBase オブジェクトのリストを初期化します。
            var downloaders = new List<IDownloaderBase>();

            // csharpSettings が null でなければ、Csharp ダウンローダーを作成して追加します。
            if (tc.CsharpSettings != null)
            {
                downloaders.Add(new Csharp(tempPath, tc.CsharpSettings));
            }

            // ScriptTsSettings が null でなければ、ScriptTs ダウンローダーを作成して追加します。
            if (tc.ScriptTsSettings != null)
            {
                downloaders.Add(new ScriptTs(tempPath, tc.ScriptTsSettings));
            }

            // PostgreSQLSettings が null でなければ、PostgreSQL ダウンローダーを作成して追加します。
            if (tc.PostgreSQLSettings != null)
            {
                downloaders.Add(new PostgreSQL(tempPath, tc.PostgreSQLSettings));
            }

            // ダウンローダーのリストを返します。
            return downloaders;
        }


    }
}

