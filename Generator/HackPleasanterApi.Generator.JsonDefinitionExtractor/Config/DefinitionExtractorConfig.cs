using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Config
{

    /// <summary>
    /// 定義入出力設定
    /// </summary>
    public class DefinitionExtractorConfig
    {
        public class Definition
        {
            /// <summary>
            /// 入力関係
            /// </summary>
            public class Input
            {
                /// <summary>
                /// サイトエクスポート情報
                /// </summary>
                public string SiteExportDefinitionFile { get; set; }

            }

            /// <summary>
            /// 出力関係
            /// </summary>
            public class Output
            {
                /// <summary>
                /// サイト定義ファイル名
                /// </summary>
                public string SiteDefinitionFile { get; set; }


                /// <summary>
                /// インターフェース定義ファイル名
                /// </summary>
                public string InterfaceDefinitionFile { get; set; }

                /// <summary>
                /// 入力ファイルのエンコード設定
                /// </summary>
                public string Encoding { get; set; }

            }

        }

        public Definition.Input Input { get; set; }

        public Definition.Output Output { get; set; }

    }
}
