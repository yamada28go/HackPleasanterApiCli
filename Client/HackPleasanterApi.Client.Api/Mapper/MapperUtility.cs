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
