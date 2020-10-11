using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.DefinitionExtractor.Config
{

    /// <summary>
    /// 定義入出力設定
    /// </summary>
    public class DefinitionExtractorConfig
    {
        public class Definition
        {
            /// <summary>
            /// PostgreSQL関係の設定
            /// </summary>
            public class PostgreSQL
            {
                public string Host;
                public int Port;
                public string Database;
                public string Username;
                public string Password;
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

        public Definition.PostgreSQL PostgreSQL { get; set; }

        public Definition.Output Output { get; set; }

    }
}
