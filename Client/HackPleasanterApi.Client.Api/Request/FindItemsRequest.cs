using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Request
{
    /// <summary>
    /// ユーザー検索用リクエスト
    /// </summary>
    public class FindItemsRequest : RequestBase
    {
        public long Offset; 
    }
}
