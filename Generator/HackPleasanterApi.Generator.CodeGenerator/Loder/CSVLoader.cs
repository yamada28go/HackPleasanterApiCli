using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;
using HackPleasanterApi.Generator.CodeGenerator.Models;
using HackPleasanterApi.Generator.Library.Models.CSV;
using HackPleasanterApi.Generator.CodeGenerator.Configs;
using HackPleasanterApi.Generator.Library.Models.CSV.Map;
using CsvHelper.Configuration;
using CsvHelper;

namespace HackPleasanterApi.Generator.CodeGenerator.Loder
{
    /// <summary>
    /// CSV読み取り機
    /// </summary>
    class CSVLoader
    {
        private IEnumerable<DataType> LoadCsv<DataType, MapType>(string path)
        where MapType : ClassMap
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var sjisEnc = Encoding.GetEncoding("Shift_JIS");
            using (var reder = new StreamReader(path, sjisEnc))
            using (var csv = new CsvReader(reder, CultureInfo.InvariantCulture))
            {
                var config = csv.Configuration;
                config.HasHeaderRecord = true; // ヘッダーが存在する場合 true
                config.RegisterClassMap<MapType>();
                var list = csv.GetRecords<DataType>();
                return list.ToList();
            }

        }


        public IEnumerable<SiteInfos> DoLoad(GeneratorConfig config)
        {
            // サイト一覧定義を読み込む
            var sites = LoadCsv<SiteDefinition, SiteDefinitionMap>(config.InputFiles.SiteDefinitionFile)
                .Where(e=>e.IsTarget == true)
                .ToList();

            // 項目名別一覧定義を読み込む
            var interfaces = LoadCsv<InterfaceDefinition, InterfaceDefinitionMap>(config.InputFiles.InterfaceDefinitionFile)
                .Where(e => true == e.IsTarget)
                .ToList();

            // サイト単位でデータを整理する
            var rl = sites
                .Select(e =>
                {
                    var r = new SiteInfos
                    {
                        SiteDefinition = e,
                        RawInterfaceDefinition = interfaces.Where(i => i.SiteId == e.SiteId).ToList()
                    };

                    r.ClassifiedInterface = ClassifiedInterface.Generate(r.RawInterfaceDefinition);

                    return r;
                })
                .ToList();

           

            return rl;
        }

    }
}
