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
using System.IO;
using HackPleasanterApi.Generator.CodeGenerator.CallableCommand;
using HackPleasanterApi.Generator.CodeGenerator.Configs;
using HackPleasanterApi.Generator.CodeGenerator.Loder;
using HackPleasanterApi.Generator.SimpleCommand.Helper;
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles;
using Microsoft.Extensions.Logging;
using NLog;

namespace HackPleasanterApi.Generator.SimpleCommand.CallableCommand
{
    public class SimpleCommandDef
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetCommandName()
        {
            return "SimpleCommand";
        }

        /// <summary>
        /// 処理対象のコマンドを設定する
        /// </summary>
        /// <returns></returns>
        public static Command MakeCommand()
        {
            var cmd = new Command(SimpleCommandDef.GetCommandName());
            cmd.AddArgument(new Argument<FileInfo>(
            "ExportFileName",
            description: "Pleasanterから生成されたサイトファイル名"
            ));
            cmd.Description = "Pleasanterから生成されたサイトファイルからグルーコードを生成します。";

            cmd.Handler = CommandHandler.Create<DirectoryInfo, FileInfo>((WorkingDirectory, ExportFileName) =>
            {
                logger.Debug($"On {SimpleCommandDef.GetCommandName()} Start!");
                logger.Debug($"On {SimpleCommandDef.GetCommandName()} WorkingDirectory: {WorkingDirectory}");
                logger.Debug($"On {SimpleCommandDef.GetCommandName()} ExportFileName: {ExportFileName}");

                // tempディレクトリで処理する
                TempResource.FolderContext(tempPath =>
                {

                    // 出力対象のファイルパス
                    var workDir = WorkingDirectory.FullName;

                    // 設定情報を抽出する
                    // 抽出するファイル名はworkDirから相対パスで取得する
                    var ex = new Logic.DefinitionExtractor(workDir, ExportFileName.Name, tempPath);
                    var exportCfg = ex.DoExport();

                    // テンプレート種別ごとに生成を行う
                    {
                        // TypeScrypt
                        {
                            var pg = new ScriptTs(tempPath, "0.2");
                            var g = new HackPleasanterApi.Generator.SimpleCommand.Logic.CodeGanarator();
                            g.d(workDir, exportCfg, pg);
                        }


                        // C#
                        {
                            var pg = new Csharp(tempPath, "0.2");
                            var g = new HackPleasanterApi.Generator.SimpleCommand.Logic.CodeGanarator();
                            g.d(workDir, exportCfg, pg);
                        }

                        // PostgreSQL
                        {
                            var pg = new PostgreSQL(tempPath, "0.2");
                            var g = new HackPleasanterApi.Generator.SimpleCommand.Logic.CodeGanarator();
                            g.d(workDir, exportCfg, pg);
                        }
                    }
                });

                logger.Debug($"On {SimpleCommandDef.GetCommandName()} End!");
            });

            return cmd;
        }

    }
}

