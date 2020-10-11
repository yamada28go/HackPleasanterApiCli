using HackPleasanterApi.Client.Api.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Interface
{
    /// <summary>
    /// ユーザーが拡張定義したデータ構造
    /// </summary>
    public class ExtensionElementsBase
    {

        /// プリザンター側と連携する生データ
        /// </summary>
        public WeakReference<ItemRawData> rawData { get; set; }

    }
}
