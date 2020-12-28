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

using AutoMapper;
using HackPleasanterApi.Client.Api.Interface;
using HackPleasanterApi.Client.Api.Mapper;
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
using System.Threading;
using System.Threading.Tasks;

namespace HackPleasanterApi.Client.Api.Service
{
    /// <summary>
    /// 各種サービス処理への基底クラス
    /// </summary>
    public class ServiceBase
    {
        /// <summary>
        /// 動作デバッグ用のロギングハンドラ
        /// </summary>
        private class Helper
        {
            public class LoggingHandler : DelegatingHandler
            {
                public LoggingHandler(HttpMessageHandler innerHandler)
                    : base(innerHandler)
                {
                }

                protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
                {
                    Console.WriteLine("Request:");
                    Console.WriteLine(request.ToString());
                    if (request.Content != null)
                    {
                        Console.WriteLine(await request.Content.ReadAsStringAsync());
                    }
                    Console.WriteLine();

                    HttpResponseMessage response = await base.SendAsync(request, cancellationToken);

                    Console.WriteLine("Response:");
                    Console.WriteLine(response.ToString());
                    if (response.Content != null)
                    {
                        Console.WriteLine(await response.Content.ReadAsStringAsync());
                    }
                    Console.WriteLine();

                    return response;
                }
            }


        }


        /// <summary>
        /// サービス設定
        /// </summary>
        protected ServiceConfig serviceConfig;

        /// <summary>
        /// HTTPアクセス用クライアント
        /// </summary>
        //  protected HttpClient client = new HttpClient(new Helper.LoggingHandler(new HttpClientHandler()));
        protected HttpClient client = new HttpClient();
        /// <summary>
        /// 値変換用のマッパーオブジェクト
        /// </summary>
        protected IMapper Mapper { get; private set; } = MapperUtility.GetMapper();


        /// <summary>
        /// 初期化
        /// </summary>
        protected void init()
        {
            // Update port # in the following line.
            client.BaseAddress = serviceConfig.uri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public ServiceBase(ServiceConfig config)
        {
            this.serviceConfig = config;
            this.init();
        }

        /// <summary>
        /// リクエスト用オブジェクトを生成する
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T GenerateRequestBase<T>() where T : RequestBase, new()
        {
            var p = new T
            {
                ApiVersion = this.serviceConfig.ApiVersion,
                ApiKey = this.serviceConfig.ApiKey
            };

            return p;
        }

    }
}


