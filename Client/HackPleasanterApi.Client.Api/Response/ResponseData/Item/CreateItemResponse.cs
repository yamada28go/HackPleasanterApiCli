using HackPleasanterApi.Client.Api.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Response.ResponseData.Item
{
    /// <summary>
    /// 生成結果オブジェクト
    /// </summary>

    public class CreateItemResponse
    {
        public long Id { get; set; }
        public int StatusCode { get; set; }
        public int LimitPerDate { get; set; }
        public int LimitRemaining { get; set; }
        public string Message { get; set; }
    }

}
