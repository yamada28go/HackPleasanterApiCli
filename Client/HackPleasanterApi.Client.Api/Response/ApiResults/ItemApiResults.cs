using HackPleasanterApi.Client.Api.Response.ResponseData.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Response.ApiResults
{


    /// <summary>
    /// Item系統のAPI戻り値
    /// </summary>
    public class ItemApiResults<ItemType> : ApiResultsBase where ItemType : ItemResponseBase
    {

        /// <summary>
        /// 戻り値データ
        /// </summary>
        public ItemType Response { get; set; }
    }


}
