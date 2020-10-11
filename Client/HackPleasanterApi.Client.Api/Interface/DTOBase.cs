using HackPleasanterApi.Client.Api.Models.ItemModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Client.Api.Interface
{
    /// <summary>
    /// DTOオブジェクト基底
    /// </summary>
    public abstract class DTOBase<T> where T : ExtensionElementsBase
    {

        private ItemRawData _rawData;

        /// <summary>
        /// プリザンター側と連携する生データ
        /// </summary>
        public ItemRawData rawData
        {
            get
            {
                return _rawData;
            }
            set
            {
                this._rawData = value;

                if (null != ExtensionElements)
                {
                    _ExtensionElements.rawData = new WeakReference<ItemRawData>(value);
                }

                if (null != BasicItemData)
                {
                    _BasicItemData.rawData = new WeakReference<ItemRawData>(value);
                }


            }
        }

        private T _ExtensionElements;

        /// <summary>
        /// ユーザーが独自に定義した拡張要素一覧
        /// </summary>
        public T ExtensionElements
        {
            get
            {
                return _ExtensionElements;
            }
            set
            {
                this._ExtensionElements = value;
                this._ExtensionElements.rawData = new WeakReference<ItemRawData>(this.rawData);
            }
        }


        private BasicItemData _BasicItemData;

        public BasicItemData BasicItemData {

            get
            {
                return _BasicItemData;
            }
            set
            {
                this._BasicItemData = value;
                this._BasicItemData.rawData = new WeakReference<ItemRawData>(this.rawData);
            }


        }



    }
}
