using System.Collections.Generic;

using HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Reader;
using HackPleasanterApi.Generator.Library.Models.CSV;
using HackPleasanterApi.Generator.Library.Service;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor
{
    /// <summary>
    /// 主要処理部分
    /// 
    /// </summary>
    class Exporter
    {

        private DefinitionExtractorConfig LoadConfig(string ConfgiPath)
        {
            var r = HackPleasanterApi.Generator.Library.Utility.XMLSerialize.Deserialize<DefinitionExtractorConfig>(ConfgiPath);
            return r;
        }
        private class JsonDefinition
        {
            public IEnumerable<SiteDefinition> SiteDefinitionConverter;
            public IEnumerable<InterfaceDefinition> InterfaceDefinitionConverter;
        }

        /// <summary>
        /// JSON定義を生成する
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private JsonDefinition ReadJsonDefinition(string path)
        {
            // 対象となるファイル群を読み取る
            var r = new JsonReader();
            var def = r.ReadAll<ExportJsonDefinition.Rootobject>(path);

            // データを変換して出力する
            var si = (new SiteDefinitionConverter()).Convert(def);

            var ifd = (new InterfaceDefinitionConverter()).Convert(def);

            return new JsonDefinition
            {
                InterfaceDefinitionConverter = ifd,
                SiteDefinitionConverter = si
            };
        }

        public void DoExporter(string configPath)
        {
            //設定ファイルを読み取る
            var config = LoadConfig(configPath);

            // サイト定義JSONを読み込む
            var r = ReadJsonDefinition(config.Input.SiteExportDefinitionFile);

            // 読み込み結果をCSVで出力する
            (new CSVExport()).WriteSiteDefinition(r.SiteDefinitionConverter, config.Output.SiteDefinitionFile);
            (new CSVExport()).WriteInterfaceDefinition(r.InterfaceDefinitionConverter, config.Output.InterfaceDefinitionFile);
        }
    }
}
