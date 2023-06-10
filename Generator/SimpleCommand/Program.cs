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

using System.Text;
using System.Text.Json;
using CsvHelper;
using HackPleasanterApi.Generator.CodeGenerator.Configs;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;
using HackPleasanterApi.Generator.SimpleCommand.Helper;
using HackPleasanterApi.Generator.SimpleCommand.Logic;
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles;
using LibGit2Sharp;
using NLog;

namespace HackPleasanterApi.Generator.SimpleCommand;

class Program
{
    static void Main(string[] args)
    {

        // tempディレクトリで処理する
        TempResource.FolderContext(tempPath =>
        {

            // 出力対象のファイルパス
            var workDir = "/Volumes/WorkSSD/work/testTTT/";

            // 設定情報を抽出する
            // 抽出するファイル名はworkDirから相対パスで取得する
            var ex = new Logic.DefinitionExtractor(workDir, "export.json", tempPath);
            var exportCfg =  ex.DoExport();

            // テンプレート種別ごとに生成を行う


            // TypeScrypt
            var t1 = Task.Run(() =>
            {
                {
                    var pg = new ScriptTs(tempPath, "0.2");
                    var g = new HackPleasanterApi.Generator.SimpleCommand.Logic.CodeGanarator();
                    g.d(workDir, exportCfg, pg);
                }
            });


            // C#
            var t2 = Task.Run(() =>
            {
            
                var pg = new Csharp(tempPath, "0.2");
                var g = new HackPleasanterApi.Generator.SimpleCommand.Logic.CodeGanarator();
                g.d(workDir, exportCfg, pg);
            });

            // PostgreSQL
            var t3 = Task.Run(() =>
            {
                var pg = new PostgreSQL(tempPath, "0.2");
                var g = new HackPleasanterApi.Generator.SimpleCommand.Logic.CodeGanarator();
                g.d(workDir, exportCfg, pg);
            });

            t1.Wait();
            t2.Wait();
            t3.Wait();
        });

    }


    /// <summary>
    /// ロガー
    /// </summary>
    private static Logger logger = LogManager.GetCurrentClassLogger();


}

