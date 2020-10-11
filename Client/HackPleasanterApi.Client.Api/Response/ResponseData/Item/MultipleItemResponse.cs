using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Response.ResponseData.Item
{
    public class MultipleItemResponse : ItemResponseBase

    {
        public long Offset { get; set; }
        public long PageSize { get; set; }
        public long TotalCount { get; set; }
    }
}
