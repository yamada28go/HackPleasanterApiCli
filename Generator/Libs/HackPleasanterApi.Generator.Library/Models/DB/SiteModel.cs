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

using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.Library.Models.DB
{
    /// <summary>
    /// DB上のSiteテーブルのマッピング用構造体
    /// </summary>
    [Serializable]
    public class SiteModel //: BaseItemModel
    {
        public string Title = string.Empty;

        public long SiteId = 0;

        //public SiteSettings SiteSettings;
        public int TenantId = 0;
        public string GridGuide = string.Empty;
        public string EditorGuide = string.Empty;
        public string ReferenceType = "Sites";
        public long ParentId = 0;
        public long InheritPermission = 0;
        public bool Publish = false;
        // public Time LockedTime = new Time();
        // public User LockedUser = new User();
        // public SiteCollection Ancestors = null;
        public int SiteMenu = 0;
        public List<string> MonitorChangesColumns = null;
        public List<string> TitleColumns = null;
        //   public Export Export = null;
        //public DateTime ApiCountDate = 0.ToDateTime();
        public int ApiCount = 0;

        public string SiteSettings = string.Empty;
    }
}
