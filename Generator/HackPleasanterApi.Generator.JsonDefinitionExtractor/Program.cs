using HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Reader;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using HackPleasanterApi.Generator.Library.Models.CSV;
using HackPleasanterApi.Generator.Library.Service;
using Microsoft.Extensions.CommandLineUtils;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor
{
    class Program
    {
        /// <summary>
        /// 実装補助関数
        /// </summary>
        private class Helper
        {
            /// <summary>
            /// デフォルト設定名
            /// </summary>
            private static readonly string DefaultConfigurationName = "DefinitionExtractorConfig.xml";

            /// <summary>
            /// 定義出力を行う
            /// </summary>
            public class Export
            {

                public static string GetCommandName()
                {
                    return "Export";
                }

                public Export(CommandLineApplication command)
                {

                    // 説明（ヘルプの出力で使用される）
                    command.Description = "Pleasanterのテーブル定義を抽出する";

                    // コマンドについてのヘルプ出力のトリガーとなるオプションを指定
                    command.HelpOption("-?|-h|--help");

                    // コマンドの引数（名前と説明を引数で渡しているが、これはヘルプ出力で使用される）
                    var tArgs = command.Argument("[ConfigFileName]", "設定ファイル名");

                    command.OnExecute(() =>
                    {
                        return On(tArgs);
                    });

                }


                private int On(CommandArgument arg)
                {
                    var cfgName = DefaultConfigurationName;
                    if (0 != arg.Values.Count)
                    {
                        cfgName = arg.Value;
                    }

                    (new Exporter()).DoExporter(cfgName);

                    return 0;
                }

            }

            public class OnDefaultConfiguration
            {
                public static string GetCommandName()
                {
                    return "GetConfiguration";
                }

                public OnDefaultConfiguration(CommandLineApplication command)
                {

                    // 説明（ヘルプの出力で使用される）
                    command.Description = "デフォルト設定を生成する";

                    // コマンドについてのヘルプ出力のトリガーとなるオプションを指定
                    command.HelpOption("-?|-h|--help");

                    // コマンドの引数（名前と説明を引数で渡しているが、これはヘルプ出力で使用される）
                    var tArgs = command.Argument("[Hogeの引数]", "Hogeの引数の説明");

                    command.OnExecute(() =>
                    {
                        return On(tArgs);
                    });

                }

                private int On(CommandArgument arg)
                {
                    // デフォルト設定
                    var c = new DefinitionExtractorConfig
                    {
                        Output = new DefinitionExtractorConfig.Definition.Output
                        {
                            InterfaceDefinitionFile = "Interface.csv",
                            SiteDefinitionFile = "Sites.csv",
                        },
                        Input = new DefinitionExtractorConfig.Definition.Input
                        {
                            SiteExportDefinitionFile = "export.json"
                        }
                    };

                    // XML形式としてデフォルト設定を生成する
                    var cfgName = DefaultConfigurationName;
                    if (0 != arg.Values.Count)
                    {
                        cfgName = arg.Value;
                    }
                    HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Serialize(c, cfgName);

                    return 0;

                }
            }
        }


        static void Main(string[] args)
        {
            // 対象データを読み取る

            var app = new CommandLineApplication(throwOnUnexpectedArg: false)
            {
                // アプリケーション名（ヘルプの出力で使用される）
                Name = "JsonDefinitionExtractor",
            };

            // ヘルプ出力のトリガーとなるオプションを指定
            app.HelpOption("-?|-h|--help");

            app.OnExecute(() =>
            {
                return 0;
            });

            // 追加コマンド

            // エクスポート
            app.Command(Helper.Export.GetCommandName(), (command) =>
            {
                new Helper.Export(command);
            });

            // デフォルト設定生成コマンド
            app.Command(Helper.OnDefaultConfiguration.GetCommandName(), (command) =>
            {
                new Helper.OnDefaultConfiguration(command);
            });

            app.Execute(args);

        }
    }
}
