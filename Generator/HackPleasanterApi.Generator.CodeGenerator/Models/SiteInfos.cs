using HackPleasanterApi.Generator.Library.Models.CSV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HackPleasanterApi.Generator.CodeGenerator.Models
{
    /// <summary>
    /// 分類済みのインターフェース定義一覧
    /// </summary>
   public class ClassifiedInterface
    {

        private ClassifiedInterface() { }

        public static ClassifiedInterface Generate(List<InterfaceDefinition> RawInterfaceDefinition)
        {

            #region ローカル関数

            // 対象をフィルタする
            List<InterfaceDefinition> LF_DoFilter(List<InterfaceDefinition> src, string prefix)
            {
                return src.Where(e => Regex.IsMatch(e.ColumnName, $"^{prefix}[a-zA-Z]"))
                    .OrderBy(e => e.ColumnName).ToList();
            }

            #endregion

            var r = new ClassifiedInterface();

            // 個別要素でフィルタする
            r.ClassHash = LF_DoFilter(RawInterfaceDefinition, "Class");
            r.NumHash = LF_DoFilter(RawInterfaceDefinition, "Num");
            r.DateHash = LF_DoFilter(RawInterfaceDefinition, "Date");
            r.DescriptionHash = LF_DoFilter(RawInterfaceDefinition, "Description");
            r.CheckHash = LF_DoFilter(RawInterfaceDefinition, "Check");
            r.AttachmentsHash = LF_DoFilter(RawInterfaceDefinition, "Attachments");

            return r;

        }


        /// <summary>
        /// 分類A～Z
        /// </summary>
        public List<InterfaceDefinition> ClassHash;

        /// <summary>
        /// 数値A～Z
        /// </summary>
        public List<InterfaceDefinition> NumHash { get; set; }
        /// <summary>
        /// 日付A～Z
        /// </summary>
        public List<InterfaceDefinition> DateHash { get; set; }

        /// <summary>
        /// 説明A～Z
        /// </summary>
        public List<InterfaceDefinition> DescriptionHash { get; set; }

        /// <summary>
        /// チェックA～Z
        /// </summary>
        public List<InterfaceDefinition> CheckHash { get; set; }

        /// <summary>
        /// 添付ファイルA～Z
        /// </summary>
        public List<InterfaceDefinition> AttachmentsHash { get; set; }

    }


    public class SiteInfos
    {

        /// <summary>
        /// 対象となるサイトの情報
        /// </summary>
        public SiteDefinition SiteDefinition;

        /// <summary>
        /// 対象とするカラムリスト
        /// </summary>
        public List<InterfaceDefinition> RawInterfaceDefinition;

        /// <summary>
        /// 分類済みのインターフェース定義
        /// </summary>
        public ClassifiedInterface ClassifiedInterface;


    }
}
