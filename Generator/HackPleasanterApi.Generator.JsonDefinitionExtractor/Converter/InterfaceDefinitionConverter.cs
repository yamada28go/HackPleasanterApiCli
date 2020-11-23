using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.Library.Models.CSV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter
{
    /// <summary>
    /// インターフェース定義形式に変換する
    /// </summary>
    class InterfaceDefinitionConverter
    {
        /// <summary>
        /// サイト定義形式に変換
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public IEnumerable<InterfaceDefinition> Convert(ExportJsonDefinition.Rootobject src)
        {
            return src.Sites.Select(x =>
            {
                if (null == x.SiteSettings)
                {
                    return null;
                }
                if (null == x.SiteSettings.Columns)
                {
                    return null;
                }

                return x.SiteSettings.Columns.Select(e =>
                {
                    return new InterfaceDefinition
                    {
                        SiteId = x.SiteId,
                        Title = x.Title,
                        ColumnName = e.ColumnName,
                        LabelText = e.LabelText
                    };
                });
            })
            .Where(e => null != e)
            .SelectMany(e => e)
            .Where(e => null != e)
            .OrderBy(e=>e.SiteId)
            .ThenBy(e=>e.ColumnName);
        }
    }
}
