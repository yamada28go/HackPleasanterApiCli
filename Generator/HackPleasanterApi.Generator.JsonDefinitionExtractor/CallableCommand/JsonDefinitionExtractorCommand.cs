﻿/**
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
using NLog;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.CallableCommand
{
    public class JsonDefinitionExtractorCommand
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static string GetCommandName()
        {
            return "JsonDefinitionExtractor";
        }

        /// <summary>
        /// 処理対象のコマンドを設定する
        /// </summary>
        /// <returns></returns>
        public static Command MakeCommand()
        {
            var cmd = new Command(JsonDefinitionExtractorCommand.GetCommandName());
            cmd.Description = "プリザンターとのサイト構成からインターフェース定義を抽出する。";

            // 配下のコマンドを追加する
            cmd.AddCommand(OnExport.MakeCommand());
            cmd.AddCommand(OnGetConfiguration.MakeCommand());

            return cmd;
        }

    }
}

