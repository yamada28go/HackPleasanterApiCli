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
using HackPleasanterApi.Generator.Libraryrary.Constant;

namespace HackPleasanterApi.Generator.CodeGenerator.Loder
{
    /// <summary>
    /// CSV読み取り機
    /// </summary>
    public class CSVLoader
    {
        private IEnumerable<DataType> LoadCsv<DataType, MapType>(GeneratorConfig refConfig, string path)
        where MapType : ClassMap
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var sjisEnc = Encoding.GetEncoding(refConfig.InputFiles.Encoding);
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
            var sites = LoadCsv<SiteDefinition, SiteDefinitionMap>(config, config.InputFiles.SiteDefinitionFile)
                .Where(e => e.IsTarget == true)
                .ToList();

            // 項目名別一覧定義を読み込む
            var interfaces = LoadCsv<InterfaceDefinition, InterfaceDefinitionMap>(config, config.InputFiles.InterfaceDefinitionFile)
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

                    // ChoicesTextを独自形式に変換する
                    ChoicesTextAdjustment(r.ClassifiedInterface);

                    return r;
                })
                .ToList();
            return rl;
        }

        /// <summary>
        /// ChoicesTextを調整する
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private void ChoicesTextAdjustment(ClassifiedInterface target)
        {
            var l =
            target.ClassHash.Where(e => false == String.IsNullOrWhiteSpace(e.ChoicesText));

            foreach (var ele in l)
            {
                if (ele.ChoicesText.Contains(CSVConstant.ChoicesText_ColumnSeparator)
                    || ele.ChoicesText.Contains(CSVConstant.ChoicesText_NewLine)
                    )
                {
                    // enumに変換可能な形式
                    var lines = ele.ChoicesText.Split(CSVConstant.ChoicesText_NewLine);
                    ele.ChoicesTextInfos = lines.Select(e =>
                    {
                        var l = e.Split(CSVConstant.ChoicesText_ColumnSeparator);

                        if (1 == l.Length)
                        {
                            // 選択肢だけ入力された場合
                            return new InterfaceDefinition.Definition.ChoicesTextInfo
                            {
                                Value = l[0],
                                VariableName = HackPleasanterApi.Generator.Libraryrary.Utility.CharacterType.ReplaceInvalidChars(l[0])
                            };
                        }
                        else if (2 == l.Length)
                        {
                            // 保存する値,テキストで指定された場合
                            // 以下のような形式で値が指定される。
                            // [指定例]
                            // AC7459,製品A
                            return new InterfaceDefinition.Definition.ChoicesTextInfo
                            {
                                Value = l[0],
                                VariableName = HackPleasanterApi.Generator.Libraryrary.Utility.CharacterType.ReplaceInvalidChars(l[0]),
                                Description = l[1]
                            };
                        }

                        else if (0 != l.Length)
                        {
                            // 短い名前などを指定した場合
                            //
                            // [指定例]
                            //100,未着手,未,status - new

                            // enumに変形可能な場合、
                            // 4分割する事が出来る                        
                            return new InterfaceDefinition.Definition.ChoicesTextInfo
                            {
                                Value = l[0],
                                Description = l[1],
                                VariableName = HackPleasanterApi.Generator.Libraryrary.Utility.CharacterType.ReplaceInvalidChars(l[3])
                            };
                        }
                        return null;
                    })
                    .Where(e => e != null)
                    // 変数名が無いデータは無視する
                    .Where(x => false == String.IsNullOrEmpty(x.VariableName))
                    .Where(x => false == String.IsNullOrWhiteSpace(x.VariableName))
                    .ToList();
                }
            }
        }
    }
}
