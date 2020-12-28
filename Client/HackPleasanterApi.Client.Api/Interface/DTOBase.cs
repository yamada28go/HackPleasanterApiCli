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
