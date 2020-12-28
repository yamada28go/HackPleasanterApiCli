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
using HackPleasanterApi.Client.Api.Models.ItemModel;
using HackPleasanterApi.Client.Api.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Mapper
{
    class MapperUtility
    {

        /// <summary>
        /// データ変換用のマッパーを生成する
        /// </summary>
        /// <returns></returns>
        public static IMapper GetMapper()
        {
            // Mapするモデルの設定
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ItemRawData, CreateItemRequest>();
            });
            // Mapperを作成
            var mapper = config.CreateMapper();
            return mapper;
        }

    }
}
