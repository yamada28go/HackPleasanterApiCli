using HackPleasanterApi.Client.Api.Interface;
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
    /// アイテム情報にアクセスするための基底クラス
    /// </summary>
    public abstract class ItmeServiceBase<DataType, ExtensionElementsType> : 
        ServiceBase where DataType : DTOBase<ExtensionElementsType>, new()
        where ExtensionElementsType : ExtensionElementsBase

    {

        /// <summary>
        /// 対象としているサイトID
        /// </summary>
        protected long SiteId;

        public ItmeServiceBase(ServiceConfig config, long SiteId) : base(config)
        {
            this.SiteId = SiteId;
        }

        /// <summary>
        /// アイテムを1個だけ取得する
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task<DataType> GetItem(long itemID)
        {
            var r = GenerateRequestBase<RequestBase>();

            HttpResponseMessage response = await client.PostAsJsonAsync($"pleasanter/api/items/{itemID}/get", r);
            if (response.IsSuccessStatusCode)
            {
                // API呼び出しを実行
                var targetData = await response.Content.ReadAsAsync<ItemApiResults<SingleItemResponse>>();

                var ps = targetData.Response.Data.Select(e =>
                {
                    var t = new DataType();
                    t.rawData = e;
                    return t;
                });

                // 取れてくるのは1個だけなので
                return ps.FirstOrDefault();

            }

            return null;

        }


        /// <summary>
        /// アイテムを複数個取得する(検索を含む)
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DataType>> FindItems<T>(T request) where T : FindItemsRequest, new()
        {
            // 検索条件を設定
            var r = GenerateRequestBase<T>();
            r.Offset = 0;

            HttpResponseMessage response = await client.PostAsJsonAsync($"pleasanter/api/items/{SiteId}/get", r);
            if (response.IsSuccessStatusCode)
            {
                // API呼び出しを実行
                var targetData = await response.Content.ReadAsAsync<ItemApiResults<MultipleItemResponse>>();

                var ps = targetData.Response.Data.Select(e =>
                {
                    var t = new DataType();
                    t.rawData = e;
                    return t;
                });

                // 存在するデータは全部とれる
                return ps;
            }

            return null;

        }



        /// <summary>
        /// アイテムを消去する
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task<DataType> DeleteItem(long itemID)
        {
            var r = GenerateRequestBase<RequestBase>();

            HttpResponseMessage response = await client.PostAsJsonAsync($"pleasanter/api/items/{itemID}/delete", r);
            if (response.IsSuccessStatusCode)
            {
                // API呼び出しを実行
                var targetData = await response.Content.ReadAsAsync<ItemApiResults<SingleItemResponse>>();

                var ps = targetData.Response.Data.Select(e =>
                {
                    var t = new DataType();
                    t.rawData = e;
                    return t;
                });

                // 取れてくるのは1個だけなので
                return ps.FirstOrDefault();

            }

            return null;

        }


    }
}


