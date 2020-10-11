using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.CodeGenerator.Configs
{
    /// <summary>
    /// コード設定機の設定
    /// </summary>
    public class GeneratorConfig
    {
        /// <summary>
        /// 設定関係
        /// </summary>
        public class Definition
        {

            /// <summary>
            /// 入力ファイル定義
            /// </summary>
            public class InputFiles
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

            /// <summary>
            /// テンプレートファイル定義
            /// </summary>
            public class TemplateFiles
            {

                /// <summary>
                /// サービス用のテンプレート
                /// </summary>
                public string TemplateService { get; set; }


                /// <summary>
                /// Dto用のテンプレート
                /// </summary>
                public string TemplateModel { get; set; }

                /// <summary>
                /// テンプレートファイルのファイルのエンコード設定
                /// </summary>
                public string Encoding { get; set; }

            }

            #region 出力ディレクト関係

            /// <summary>
            /// 出力設定
            /// </summary>
            public class OutputConfig
            {

                /// <summary>
                /// 出力ディレクトパス
                /// </summary>
                public string OutputDirectory { get; set; }

                /// <summary>
                /// 出力ファイル拡張子
                /// </summary>
                public string OutputExtension { get; set; }

                /// <summary>
                /// 出力ファイルのファイルのエンコード設定
                /// </summary>
                public string Encoding { get; set; }

            }


            #endregion


            #region コード生成関係

            /// <summary>
            /// コード関係の設定
            /// </summary>
            public class CodeConfig
            {

                /// <summary>
                /// 生成される名前空間
                /// </summary>
                public string NameSpace { get; set; }

            }

        }

        #endregion

        #region 実態定義

        /// <summary>
        /// 入力ファイル
        /// </summary>
        public Definition.InputFiles InputFiles;

        /// <summary>
        /// 出力設定
        /// </summary>
        public Definition.OutputConfig OutputConfig;

        /// <summary>
        /// テンプレート設定
        /// </summary>
        public Definition.TemplateFiles TemplateFiles;

        /// <summary>
        ///　生成コード設定
        /// </summary>
        public Definition.CodeConfig CodeConfig;

        #endregion

    }
}
