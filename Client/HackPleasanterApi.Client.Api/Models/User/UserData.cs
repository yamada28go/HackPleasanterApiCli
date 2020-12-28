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

namespace HackPleasanterApi.Client.Api.Models.User
{
    /// <summary>
    /// User 系統APIの戻り値データ形式
    /// </summary>
    public class UserData
    {
        public int TenantId { get; set; }
        public int UserId { get; set; }
        public int Ver { get; set; }
        public string LoginId { get; set; }
        public string GlobalId { get; set; }
        public string Name { get; set; }
        public string UserCode { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Birthday { get; set; }
        public string Gender { get; set; }
        public string Language { get; set; }
        public string TimeZone { get; set; }
        public string DeptCode { get; set; }
        public int DeptId { get; set; }
        public int FirstAndLastNameOrder { get; set; }
        public string Body { get; set; }
        public string LastLoginTime { get; set; }
        public DateTime PasswordExpirationTime { get; set; }
        public string PasswordChangeTime { get; set; }
        public string NumberOfLogins { get; set; }
        public string NumberOfDenial { get; set; }
        public object TenantManager { get; set; }
        public object ServiceManager { get; set; }
        public bool Disabled { get; set; }
        public bool Lockout { get; set; }
        public int LockoutCounter { get; set; }
        public bool Developer { get; set; }
        public string UserSettings { get; set; }
        public string LdapSearchRoot { get; set; }
        public DateTime SynchronizedTime { get; set; }
        public string Comments { get; set; }
        public int Creator { get; set; }
        public int Updator { get; set; }
        public string CreatedTime { get; set; }
        public string UpdatedTime { get; set; }
        public float ApiVersion { get; set; }
        public Classhash ClassHash { get; set; }
        public Numhash NumHash { get; set; }
        public Datehash DateHash { get; set; }
        public Descriptionhash DescriptionHash { get; set; }
        public Checkhash CheckHash { get; set; }
        public Attachmentshash AttachmentsHash { get; set; }

    }



    public class Rootobject
    {
        public int StatusCode { get; set; }
        public Response Response { get; set; }
    }

    public class Response
    {
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public Datum[] Data { get; set; }
    }

    public class Datum
    {

    }

    public class Classhash
    {
    }

    public class Numhash
    {
    }

    public class Datehash
    {
    }

    public class Descriptionhash
    {
    }

    public class Checkhash
    {
    }

    public class Attachmentshash
    {
    }



}
