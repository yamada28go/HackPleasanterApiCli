using HackPleasanterApi.Client.Api.Interface;
using HackPleasanterApi.Client.Api.Models.User;
using HackPleasanterApi.Client.Api.Request;
using HackPleasanterApi.Client.Api.Response;
using HackPleasanterApi.Client.Api.Response.ApiResults;
using HackPleasanterApi.Client.Api.Response.ResponseData.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HackPleasanterApi.Client.Api.Service
{
    /// <summary>
    /// ユーザー情報にアクセスするための基底クラス
    /// </summary>
    public class UserService : ServiceBase
    {

        public UserService(ServiceConfig config) : base(config)
        {
        }

        /// <summary>
        /// 全ユーザーを取得する
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task<UserResponse> GetAllUser()
        {
            // 検索条件を設定
            var r = GenerateRequestBase<RequestBase>();

            HttpResponseMessage response = await client.PostAsJsonAsync($"pleasanter/api/users/get", r);
            if (response.IsSuccessStatusCode)
            {
                // API呼び出しを実行
                var targetData = await response.Content.ReadAsAsync<UserApiResults>();

                return targetData.Response;
            }

            return null;

        }


        /// <summary>
        /// アイテムを消去する
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task<DeleteApiResults> DeleteItem(long itemID)
        {
            var r = GenerateRequestBase<RequestBase>();

            HttpResponseMessage response = await client.PostAsJsonAsync($"fs/api/users/{itemID}/delete", r);
            if (response.IsSuccessStatusCode)
            {
                // API呼び出しを実行
                var targetData = await response.Content.ReadAsAsync<DeleteApiResults>();

                if (targetData.StatusCode == 200)
                {

                    return targetData;
                }
            }

            return null;

        }

    }
}


