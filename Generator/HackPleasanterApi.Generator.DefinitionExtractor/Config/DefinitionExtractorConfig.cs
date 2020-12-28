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

namespace HackPleasanterApi.Generator.DefinitionExtractor.Config
{

    /// <summary>
    /// 定義入出力設定
    /// </summary>
    public class DefinitionExtractorConfig
    {
        public class Definition
        {
            /// <summary>
            /// PostgreSQL関係の設定
            /// </summary>
            public class PostgreSQL
            {
                public string Host;
                public int Port;
                public string Database;
                public string Username;
                public string Password;
            }

            /// <summary>
            /// 出力関係
            /// </summary>
            public class Output
            {
                /// <summary>
                /// サイト定義ファイル名
                /// </summary>
                public string SiteDefinitionFile { get; set; }


                /// <summary>
                /// インターフェース定義ファイル名
                /// </summary>
                public string InterfaceDefinitionFile { get; set; }

                /// <summary>
                /// 入力ファイルのエンコード設定
                /// </summary>
                public string Encoding { get; set; }

            }

        }

        public Definition.PostgreSQL PostgreSQL { get; set; }

        public Definition.Output Output { get; set; }

    }
}
