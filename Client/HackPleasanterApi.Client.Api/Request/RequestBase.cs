using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Request
{
    public class RequestBase
    {
        /// <summary>
        /// 対象とするAPIバージョン
        /// </summary>
        public string ApiVersion;
        /// <summary>
        /// アクセス用のAPIキー
        /// </summary>
        public string ApiKey;
    }
}
