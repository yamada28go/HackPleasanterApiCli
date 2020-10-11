using HackPleasanterApi.Client.Api.Models.User;
using HackPleasanterApi.Client.Api.Response.ResponseData.Item;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Response.ApiResults
{


    /// <summary>
    /// Item系統のAPI戻り値
    /// </summary>
    public class UserApiResults : ApiResultsBase 
    {

        /// <summary>
        /// 戻り値データ
        /// </summary>
        public UserResponse Response { get; set; }
    }


}
