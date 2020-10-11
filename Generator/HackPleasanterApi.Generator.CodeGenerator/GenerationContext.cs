using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using HackPleasanterApi.Generator.CodeGenerator.Models;

namespace HackPleasanterApi.Generator.CodeGenerator
{

    /// <summary>
    /// コード生成に必要な情報セットを格納
    /// </summary>
    class GenerationContext
    {
        public IEnumerable<SiteInfos> Sites;
    }
}
