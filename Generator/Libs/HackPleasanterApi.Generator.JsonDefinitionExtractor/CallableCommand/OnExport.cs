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
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using NLog;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.CallableCommand
{
    /// <summary>
    /// サイト構成ファイルからサイト定義を展開するコマンド
    /// </summary>
    internal class OnExport
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetCommandName()
        {
            return "Export";
        }

        /// <summary>
        /// 処理対象のコマンドを設定する
        /// </summary>
        /// <returns></returns>
        public static Command MakeCommand()
        {
            var cmd = new Command(OnExport.GetCommandName());
            cmd.AddArgument(new Argument<FileInfo>(
            "ConfigurationFileName",
            description: "出力されるデフォルトの設定ファイル名称"
            ));
            cmd.Description = "コード生成の設定に使用するデフォルトの設定ファイルを取得します。";

            cmd.Handler = CommandHandler.Create<DirectoryInfo, FileInfo>((WorkingDirectory, ConfigurationFileName) =>
            {
                logger.Debug($"On {GetCommandName()} Start!");
                logger.Debug($"On {GetCommandName()} WorkingDirectory: {WorkingDirectory}");
                logger.Debug($"On {GetCommandName()} OutFileName: {ConfigurationFileName}");

                var x = new OnExport();
                x.DoGenerate(WorkingDirectory, ConfigurationFileName);
                logger.Debug($"On {GetCommandName()} End!");
            });

            return cmd;
        }

        /// <summary>
        /// データの生成を行う
        /// </summary>
        /// <param name="workDir"></param>
        /// <param name="cfgFile"></param>
        private void DoGenerate(DirectoryInfo workDir, FileInfo cfgFile)
        {
            // XML形式としてデフォルト設定を生成する
            var file = Path.Combine(workDir.FullName, cfgFile.Name);
            logger.Info($"読み取り対象設定ファイル : {file}");

            (new Exporter()).DoExporter(workDir, file);

        }

    }
}

