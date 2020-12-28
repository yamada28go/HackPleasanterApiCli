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

using Dapper;
using HackPleasanterApi.Generator.DefinitionExtractor.Config;
using HackPleasanterApi.Generator.Library.Models.DB;
using HackPleasanterApi.Generator.Library.Service;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.DefinitionExtractor
{
    /// <summary>
    /// 出力クラス
    /// </summary>
    class Exporter
    {

        private DefinitionExtractorConfig LoadConfig(string ConfgiPath)
        {
            var r = HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Deserialize<DefinitionExtractorConfig>(ConfgiPath);
            return r;
        }

        public void DoExporter(string ConfgiPath)
        {
            // 設定を読み取る
            DefinitionExtractorConfig cfg = LoadConfig(ConfgiPath);

            var builder = new NpgsqlConnectionStringBuilder()
            {
                Host = cfg.PostgreSQL.Host,
                Port = cfg.PostgreSQL.Port,
                Database = cfg.PostgreSQL.Database,
                Username = cfg.PostgreSQL.Username,
                Password = cfg.PostgreSQL.Password,
            };

            // SQLに接続して設定を読み取る
            using (var connection = new NpgsqlConnection(builder.ConnectionString))
            {
                var query = " select * from \"Sites\"; ";
                var sites = connection.Query<SiteModel>(query);

                (new CSVExport()).doExportSiteInformation(sites, cfg.Output.SiteDefinitionFile);
                (new CSVExport()).doExport(sites, cfg.Output.InterfaceDefinitionFile);

            }


        }

    }
}
