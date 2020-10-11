using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Response.ApiResults
{
    /// <summary>
    /// API戻り値の基底クラス
    /// </summary>
    public class ApiResultsBase
    {
        /// <summary>
        /// 戻り値ステータスコード
        /// </summary>
        public int StatusCode { get; set; }

    }
}
