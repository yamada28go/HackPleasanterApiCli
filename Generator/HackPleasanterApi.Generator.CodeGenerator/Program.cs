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

using HackPleasanterApi.Generator.CodeGenerator.CallableCommand;
using NLog;
using System.CommandLine;
using System.IO;
using System.Linq;

namespace HackPleasanterApi.Generator.CodeGenerator
{
    class Program
    {
        /// <summary>
        /// ロガー
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            logger.Info($"Pleasanter インターフェースコード生成 コマンド　起動!!! ");
            logger.Debug("Start CodeGenerator!");
            if (null != args && 0 != args.Length)
            {
                logger.Debug($"arg : {args?.Aggregate((x, y) => x + ", " + y)}");
            }

            // [Memo]
            // Argument , Optionの引数名とCommandHandler.Create関数で指定する
            // 関数パラメータの引数名は合致していないと正しく動作しないので、
            // 注意が必要
            //
            // 参考
            // https://qiita.com/TsuyoshiUshio@github/items/02902f4f46f0aa37e4b1

            // Create a root command with some options
            var rootCommand = new RootCommand
            {
                        new Argument<DirectoryInfo>(
                            "WorkingDirectory",
                            description: "コマンドの作業ディレクトリ"
                            ),
            };

            rootCommand.Description = "Pleasanter インターフェースコード生成";

            // 生成コマンドを指定する
            rootCommand.Add(OnGenerate.MakeCommand());

            // デフオルトの設定ファイルを生成する
            rootCommand.Add(OnDefaultConfiguration.MakeCommand());

            // 生成処理を開始
            logger.Debug("Start Invoke!");

            // Parse the incoming args and invoke the handler
            var x = rootCommand.Invoke(args);

            logger.Debug("End Invoke!");

            logger.Info($"Pleasanter インターフェースコード生成 コマンド　終了!!! ");
        }
    }
}
