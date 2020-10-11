using HackPleasanterApi.Client.Api.Models.ItemModel.Hash;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Models.ItemModel
{
    /// <summary>
    /// Item形式基本データ構造
    /// </summary>
    public class ItemRawData
    {
        /// <summary>
        /// サイトID
        /// </summary>
        public int SiteId { get; set; }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdatedTime { get; set; }
        /// <summary>
        /// レコード ID 記録テーブルのみ
        /// </summary>
        public long ResultId { get; set; }

        /// <summary>
        /// レコード ID 期限付きテーブルのみ
        /// </summary>
        public long IssueId { get; set; }

        /// <summary>
        /// バージョン
        /// </summary>
        public int Ver { get; set; }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 状況
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        public int Manager { get; set; }
        /// <summary>
        /// 担当者
        /// </summary>
        public int Owner { get; set; }
        /// <summary>
        /// ロックされているか
        /// </summary>
        public bool Locked { get; set; }
        /// <summary>
        /// コメント
        /// </summary>
        public string Comments { get; set; }
        /// <summary>
        /// 作成者
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 更新者
        /// </summary>
        public int Updator { get; set; }
        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreatedTime { get; set; }
        /// <summary>
        /// タイトル(表示用)
        /// </summary>
        public string ItemTitle { get; set; }
        /// <summary>
        /// APIバージョン
        /// </summary>
        public double ApiVersion { get; set; }
        /// <summary>
        /// 分類A～Z
        /// </summary>
        public ClassHash ClassHash { get; set; }
        /// <summary>
        /// 数値A～Z
        /// </summary>
        public NumHash NumHash { get; set; }
        /// <summary>
        /// 日付A～Z
        /// </summary>
        public DateHash DateHash { get; set; }
        /// <summary>
        /// 説明A～Z
        /// </summary>
        public DescriptionHash DescriptionHash { get; set; }

        /// <summary>
        /// チェックA～Z
        /// </summary>
        public CheckHash CheckHash { get; set; }

        /// <summary>
        /// 添付ファイルA～Z
        /// </summary>
        public AttachmentsHash AttachmentsHash { get; set; }
    }
}
