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
using System.IO;
using System.CommandLine;
using NLog;
using System.CommandLine.Invocation;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.CallableCommand;
using HackPleasanterApi.Generator.CodeGenerator.CallableCommand;

namespace HackPleasanterApi.Generator.DebugCommand.CallableCommand
{
    public class DebugCommandDef
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetCommandName()
        {
            return "DebugCommand";
        }

        /// <summary>
        /// 処理対象のコマンドを設定する
        /// </summary>
        /// <returns></returns>
        public static Command MakeCommand()
        {
            var cmd = new Command(DebugCommandDef.GetCommandName());
            cmd.Description = "[デバッグ用] Generationの内部動作で使用される個別コマンドを指定して実行します。";

            //// JSON設定取り込み処理の実行処理コマンドを登録する
            cmd.Add(JsonDefinitionExtractorCommand.MakeCommand());

            //// コード生成器の実行処理コマンドを登録する
            cmd.Add(CodeGeneratorCommand.MakeCommand());

            //cmd.Handler = CommandHandler.Create<DirectoryInfo, FileInfo>((WorkingDirectory, ExportFileName) =>
            //{
            //    logger.Debug($"On {DebugCommandDef.GetCommandName()} Start!");
            //    logger.Debug($"On {DebugCommandDef.GetCommandName()} WorkingDirectory: {WorkingDirectory}");
            //    logger.Debug($"On {DebugCommandDef.GetCommandName()} ExportFileName: {ExportFileName}");


            //    logger.Debug($"On {DebugCommandDef.GetCommandName()} End!");
            //});

            return cmd;
        }

    }
}

