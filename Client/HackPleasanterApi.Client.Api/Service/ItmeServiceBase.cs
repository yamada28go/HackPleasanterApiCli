/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 * */

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

                return null;

            }

            return null;


        }

        /// <summary>
        /// アイテムを一括削除する
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns></returns>
        public async Task<DeleteAllItemsResponse> DeleteALL(DeleteAllItemsRequest req = null)
        {
            var r = new DeleteAllItemsRequest();
            r.ApiKey = serviceConfig.ApiKey;
            r.ApiVersion = serviceConfig.ApiVersion;

            r.View = new View();

            HttpResponseMessage response = await client.PostAsJsonAsync($"/api/items/{SiteId}/bulkdelete", r);
            if (response.IsSuccessStatusCode)
            {
                // API呼び出しを実行
                var targetData = await response.Content.ReadAsAsync<DeleteAllItemsResponse>();
                return targetData;
            }

            return null;


        }

        public async Task<CreateItemResponse> CreateItem(DataType data)
        {
            var r = this.Mapper.Map<CreateItemRequest>(data.rawData);
            r.ApiKey = this.serviceConfig.ApiKey;

            HttpResponseMessage response = await client.PostAsJsonAsync($"pleasanter/api/items/{SiteId}/create", r);
            if (response.IsSuccessStatusCode)
            {
                // API呼び出しを実行
                var targetData = await response.Content.ReadAsAsync<CreateItemResponse>();

                return targetData;
            }

            return null;

        }

        /// <summary>
        /// アイテム情報更新
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<CreateItemResponse> UpdateItem(long itemID, DataType data)
        {
            var r = this.Mapper.Map<CreateItemRequest>(data.rawData);
            r.ApiKey = this.serviceConfig.ApiKey;

            HttpResponseMessage response = await client.PostAsJsonAsync($"pleasanter/api/items/{itemID}/update", r);
            if (response.IsSuccessStatusCode)
            {
                // API呼び出しを実行
                var targetData = await response.Content.ReadAsAsync<CreateItemResponse>();

                return targetData;
            }

            return null;

        }



    }
}


