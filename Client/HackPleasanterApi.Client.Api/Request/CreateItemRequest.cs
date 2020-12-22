using HackPleasanterApi.Client.Api.Interface;
using HackPleasanterApi.Client.Api.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Request
{
    public class CreateItemRequest : ItemRawData
    {

        /// <summary>
        /// 対象とするAPIバージョン
        /// </summary>
       // public string ApiVersion;

        /// <summary>
        /// アクセス用のAPIキー
        /// </summary>
        public string ApiKey;

    }
}
