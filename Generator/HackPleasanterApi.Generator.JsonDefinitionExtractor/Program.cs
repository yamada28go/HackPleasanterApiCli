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

using HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Reader;
using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using HackPleasanterApi.Generator.Library.Models.CSV;
using HackPleasanterApi.Generator.Library.Service;
using Microsoft.Extensions.CommandLineUtils;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // Memo
            //
            // 統合コマンドから呼び出すので、この関数は廃止とする。
        }
    }
}
