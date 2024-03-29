﻿/**
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
using System.Diagnostics;
using Cysharp.Diagnostics;
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles.Settings;
using Zx;

namespace HackPleasanterApi.Generator.GenerationCommand.TemplatesFiles
{
    public class ScriptTs : DownloaderBase<ScriptTsSettings>
    {
        public ScriptTs(string refLocalPath, ScriptTsSettings ver) : base("https://github.com/yamada28go/HackPleasanterApi.pleasanter-web-script.git", refLocalPath, ver)
        {

        }

        protected override async Task PostProcessingImp(string path)
        {
            var workPath = path;

            // 作業パスを移動する
            await $"cd {workPath}";

            //フォーマットをかける
            var r = await "prettier --write \"**/*.ts\"";
            logger.Debug($"cmd out : ${r}");

        }


    }
}

