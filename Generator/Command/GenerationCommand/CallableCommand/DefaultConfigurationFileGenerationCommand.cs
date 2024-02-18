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
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles.Settings;
using NLog;

namespace HackPleasanterApi.Generator.GenerationCommand.CallableCommand
{
    public class DefaultConfigurationFileGenerationCommandDef
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetCommandName()
        {
            return "DefaultConfigurationFileGeneration";
        }

        /// <summary>
        /// 処理対象のコマンドを設定する
        /// </summary>
        /// <returns></returns>
        public static Command MakeCommand()
        {
            var cmd = new Command(DefaultConfigurationFileGenerationCommandDef.GetCommandName());
            cmd.AddArgument(new Argument<FileInfo>(
            "ConfigurationFileName",
            description: "出力されるコンフィグファイル名"
            ));
            cmd.Description = "コマンドの動作設定用のコンフィグファイルのひな形を生成します。";

            // オプションを定義する
            var option = new Option<bool>(
                new[] { "--full", "-f" },// オプション名
                getDefaultValue: () => false,
                description: "デバッグ用設定を含む全内容で生成");  // 説明
            cmd.AddOption(option);


            cmd.Handler = CommandHandler.Create<DirectoryInfo, FileInfo, bool>((WorkingDirectory, ConfigurationFileName, full) =>
            {
                logger.Debug($"On {DefaultConfigurationFileGenerationCommandDef.GetCommandName()} Start!");
                logger.Debug($"On {DefaultConfigurationFileGenerationCommandDef.GetCommandName()} WorkingDirectory: {WorkingDirectory}");
                logger.Debug($"On {DefaultConfigurationFileGenerationCommandDef.GetCommandName()} ConfigurationFileName: {ConfigurationFileName}");

                GenerationSettings tc;

                if (true == full)
                {
                    // デフォルトの設定値を作成
                    tc = GenerationSettings.MakeFullSetDefault();
                }
                else
                {
                    // デフォルトの設定値を作成
                    tc = GenerationSettings.MakeDefault();
                }


                // XML形式としてデフォルト設定を生成する
                var file = Path.Combine(WorkingDirectory.FullName, ConfigurationFileName.Name);
                logger.Info($"出力されるデフォルト設定ファイル : {file}");
                HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Serialize(tc, file);

                logger.Debug($"On {DefaultConfigurationFileGenerationCommandDef.GetCommandName()} End!");
            });

            return cmd;
        }

    }
}

