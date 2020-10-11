using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Models.ItemModel.Hash
{
    public class Attachments
    {

        /// <summary>
        /// string 添付ファイルのGUID
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// 添付ファイル名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// string Content Type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        ///  string ファイルデータをBase64エンコーディングしたもの
        /// </summary>
        public string Base64 { get; set; }

    }
}
