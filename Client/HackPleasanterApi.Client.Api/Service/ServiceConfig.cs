using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Service
{
    public class ServiceConfig
    {
        /// <summary>
        /// 対象となるAPI
        /// </summary>
        public Uri uri;

        /// <summary>
        /// APIバージョン
        /// </summary>
        public string ApiVersion;
        /// <summary>
        /// アクセス用APIキー
        /// </summary>
        public string ApiKey;
    }
}
