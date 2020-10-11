using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackPleasanterApi.Generator.Library.Models.CSV.Map
{
    /// <summary>
    /// サイト情報に関するマッピング定義
    /// </summary>
    public class SiteDefinitionMap : ClassMap<SiteDefinition>
    {
        public SiteDefinitionMap()
        {
            Map(c => c.SiteId).Index(0);
            Map(c => c.Title).Index(1);
            Map(c => c.IsTarget).Index(2);
            Map(c => c.SiteVariableName).Index(3);
            Map(c => c.Memo).Index(4);

        }
    }
}
