using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.Library.Models.CSV

{
    /// <summary>
    /// サイト定義用　構造体
    /// </summary>
    public class SiteDefinition
    {
        #region Siteテーブル関係

        // みんな同じ値を持つ
        public string Title { get; set; }
        /// <summary>
        /// 対象となるサイトID
        /// </summary>
        public long SiteId { get; set; }

        #endregion

        #region ユーザーがTSV上で指定する項目

        /// <summary>
        /// ユーザーが指定すサイト名称
        /// </summary>
        public string SiteVariableName { get; set; }

        /// <summary>
        /// メモ
        /// </summary>
        public string Memo { get; set; }

        /// <summary>
        /// 変換対処テーブルか判定する
        /// </summary>
        public bool IsTarget { get; set; }

        #endregion
    }
}
