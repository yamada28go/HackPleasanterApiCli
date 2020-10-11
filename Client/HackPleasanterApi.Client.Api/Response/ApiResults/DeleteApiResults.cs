using HackPleasanterApi.Client.Api.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Response.ApiResults
{
    /// <summary>
    /// 削除系統のAPI戻り値
    /// </summary>
    public class DeleteApiResults
    {
        public long Id { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }

}
