using System;

using NLog;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using HackPleasanterApi.Generator.CodeGenerator.CallableCommand;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.CallableCommand;

namespace EntranceCommand
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

            // JSON設定取り込み処理の実行処理コマンドを登録する
            rootCommand.Add(JsonDefinitionExtractorCommand.MakeCommand());

            // コード生成器の実行処理コマンドを登録する
            rootCommand.Add(CodeGeneratorCommand.MakeCommand());

            // 生成処理を開始
            logger.Debug("Start Invoke!");

            // Parse the incoming args and invoke the handler
            var x = rootCommand.Invoke(args);

            logger.Debug("End Invoke!");

            logger.Info($"Pleasanter インターフェースコード生成 コマンド　終了!!! ");
        }
    }
}
