using HackPleasanterApi.Client.Api.Models.ItemModel;
using HackPleasanterApi.Client.Api.Models.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Response.ResponseData.Item
{
    /// <summary>
    /// ユーザー戻り値
    /// </summary>
    public class UserResponse

    {
        public long Offset { get; set; }
        public long PageSize { get; set; }
        public long TotalCount { get; set; }

        public List<UserData> Data { get; set; }
    }
}
