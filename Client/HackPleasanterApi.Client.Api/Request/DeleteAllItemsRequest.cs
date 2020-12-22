using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Request
{
    /// <summary>
    /// 全データ削除リクエスト
    /// </summary>
    public class DeleteAllItemsRequest : RequestBase
    {
        /// <summary>
        /// 物理消去する
        /// </summary>
        public bool PhysicalDelete = false;


        public View View;
    }
}
