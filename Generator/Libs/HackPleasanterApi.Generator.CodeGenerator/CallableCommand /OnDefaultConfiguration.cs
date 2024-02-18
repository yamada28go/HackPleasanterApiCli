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
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using HackPleasanterApi.Generator.CodeGenerator.Configs;
using NLog;

namespace HackPleasanterApi.Generator.CodeGenerator.CallableCommand
{
    /// <summary>
    /// デフォルト設定ファイル生成コマンド
    /// </summary>
    internal class OnDefaultConfiguration
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetCommandName()
        {
            return "DefaultConfiguration";
        }

        /// <summary>
        /// 処理対象のコマンドを設定する
        /// </summary>
        /// <returns></returns>
        public static Command MakeCommand()
        {
            var cmd = new Command(OnDefaultConfiguration.GetCommandName());
            cmd.AddArgument(new Argument<FileInfo>(
            "OutFileName",
            description: "出力されるデフォルトの設定ファイル名称"
            ));
            cmd.Description = "コード生成の設定に使用するデフォルトの設定ファイルを取得します。";

            cmd.Handler = CommandHandler.Create<DirectoryInfo, FileInfo>((WorkingDirectory, OutFileName) =>
            {
                logger.Debug($"On {OnDefaultConfiguration.GetCommandName()} Start!");
                logger.Debug($"On {OnDefaultConfiguration.GetCommandName()} WorkingDirectory: {WorkingDirectory}");
                logger.Debug($"On {OnDefaultConfiguration.GetCommandName()} OutFileName: {OutFileName}");

                var x = new OnDefaultConfiguration();
                x.On(WorkingDirectory, OutFileName);
                logger.Debug("On OnDefaultConfiguration End!");
            });

            return cmd;
        }

        /// <summary>
        /// コマンド実行時のパラメータ
        /// </summary>
        /// <param name="workDir"></param>
        /// <param name="outFile"></param>
        /// <returns></returns>
        private int On(DirectoryInfo workDir, FileInfo outFile)
        {
            // デフォルト設定
            var c = new GeneratorConfig
            {
                InputFiles = new GeneratorConfig.Definition.InputFiles
                {
                    InterfaceDefinitionFile = @"Interface.csv",
                    SiteDefinitionFile = @"Sites.csv",
                    Encoding = "Shift_JIS"
                },
                TemplateFiles = new List<GeneratorConfig.Definition.TemplateFiles> {
                            new GeneratorConfig.Definition.TemplateFiles
                            {
                                OutputSubdirectoryName = "Services",
                                TemplateFileName = @"Templates/CSharp/ServiceTemplate.txt",
                                Encoding = "Shift_JIS",
                                OutputExtension = @"cs",
                                HeadPrefix ="",
                                EndPrefix = "Service"
                            },
                            new GeneratorConfig.Definition.TemplateFiles
                            {
                                OutputSubdirectoryName = "Models",
                                TemplateFileName = @"Templates/CSharp/ModelTemplate.txt",
                                Encoding = "Shift_JIS",
                                OutputExtension = @"cs",
                                HeadPrefix ="",
                                EndPrefix = "Model"
                            }
                        },
                OutputConfig = new GeneratorConfig.Definition.OutputConfig
                {
                    OutputDirectory = @"Generated"
                },
                CodeConfig = new GeneratorConfig.Definition.CodeConfig
                {
                    NameSpace = "",
                }
            };

            // XML形式としてデフォルト設定を生成する
            var file = Path.Combine(workDir.FullName, outFile.Name);
            logger.Info($"出力されるデフォルト設定ファイル : {file}");
            HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Serialize(c, file);

            return 0;

        }
    }
}

