using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.Library.Models.CSV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter
{
    /// <summary>
    /// サイト定義を生成する
    /// </summary>
    class SiteDefinitionConverter
    {
        /// <summary>
        /// サイト定義形式に変換
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public IEnumerable<SiteDefinition> Convert(ExportJsonDefinition.Rootobject src)
        {
            return src.Sites.Select(x =>
            {
                return new SiteDefinition
                {
                    SiteId = x.SiteId,
                    Title = x.Title
                };
            })
            .GroupBy(e => e.SiteId)
            .Select(e => e.FirstOrDefault())
            .Where(e => e != null)
            .OrderBy(e => e.SiteId);
        }
    }
}
