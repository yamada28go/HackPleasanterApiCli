using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.Library.Models.CSV

{
    /// <summary>
    /// インターフェース定義用構造体
    /// </summary>
    public class InterfaceDefinition
    {
        #region Siteテーブル関係

        // みんな同じ値を持つ
        public string Title { get; set; }
        public long SiteId { get; set; }
        public long ParentId { get; set; }
        public long InheritPermission { get; set; }

        #endregion

        #region カラム毎の値

        /// <summary>
        /// カラム名
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 画面上の表示名
        /// </summary>
        public string LabelText { get; set; }



        #endregion

        #region ユーザーがTSV上で指定する項目

        /// <summary>
        /// API変換した後のオブジェクト名
        /// </summary>
        public string ObjectName { get; set; }

        /// <summary>
        /// 新たに指定する変数名
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        /// 変換対処テーブルか判定する
        /// </summary>
        public bool IsTarget { get; set; }


        #endregion
    }
}
