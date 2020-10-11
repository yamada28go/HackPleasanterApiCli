using HackPleasanterApi.Client.Api.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Response.ResponseData.Item
{
    /// <summary>
    /// 要素取得結果ベース
    /// </summary>
    public class ItemResponseBase

    {
        public List<ItemRawData> Data { get; set; }
    }
}
