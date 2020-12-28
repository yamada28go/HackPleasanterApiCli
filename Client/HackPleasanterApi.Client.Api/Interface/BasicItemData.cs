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
    /// 基本Itemデータ構造
    /// </summary>
    public class BasicItemData
    {
        /// プリザンター側と連携する生データ
        /// </summary>
        public WeakReference<ItemRawData> rawData { get; set; }


        #region アクセスインターフェース定義

        /// <summary>
        /// サイトID
        /// </summary>
        public int SiteId
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.SiteId;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.SiteId = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// 更新日時
        /// </summary>
        public DateTime UpdatedTime
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.UpdatedTime;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.UpdatedTime = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// レコード ID 記録テーブルのみ
        /// </summary>
        public long ResultId
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.ResultId;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.ResultId = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }

        /// <summary>
        /// レコード ID 期限付きテーブルのみ
        /// </summary>
        public long IssueId
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.IssueId;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.IssueId = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }


        /// <summary>
        /// バージョン
        /// </summary>
        public int Ver
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Ver;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Ver = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }

        /// <summary>
        /// タイトル
        /// </summary>
        public string Title
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Title;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Title = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }

        /// <summary>
        /// 内容
        /// </summary>
        public string Body
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Body;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Body = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }

        /// <summary>
        /// 状況
        /// </summary>
        public int Status
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Status;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Status = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }

        /// <summary>
        /// 管理者
        /// </summary>
        public int Manager
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Manager;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Manager = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// 担当者
        /// </summary>
        public int Owner
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Owner;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Owner = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// ロックされているか
        /// </summary>
        public bool Locked
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Locked;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Locked = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// コメント
        /// </summary>
        public string Comments
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Comments;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Comments = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// 作成者
        /// </summary>
        public int Creator
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Creator;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Creator = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// 更新者
        /// </summary>
        public int Updator
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.Updator;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.Updator = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// 作成日時
        /// </summary>
        public DateTime CreatedTime
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.CreatedTime;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.CreatedTime = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }

        /// <summary>
        /// タイトル(表示用)
        /// </summary>
        public string ItemTitle
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.ItemTitle;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.ItemTitle = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }
        /// <summary>
        /// APIバージョン
        /// </summary>
        public double ApiVersion
        {
            get
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    return obj.ApiVersion;
                }
                throw new ApplicationException("参照エラー");
            }
            set
            {
                if (rawData.TryGetTarget(out var obj))
                {
                    obj.ApiVersion = value;
                }
                throw new ApplicationException("参照エラー");
            }
        }

        #endregion
    }
}
