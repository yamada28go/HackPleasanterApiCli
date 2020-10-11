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
