using System.Text;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Config;
using Xunit;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Tests;

public class ExporterTests
{
    [Fact]
    public void DoExporter_AppliesVariableNameRules_AndWritesCsvFiles()
    {
        var workDirPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDirPath);
        var workDir = new DirectoryInfo(workDirPath);
        var jsonPath = Path.Combine(workDirPath, "site-export.json");

        var json = "{\n" +
                   "  \"Sites\": [\n" +
                   "    {\n" +
                   "      \"SiteId\": 10,\n" +
                   "      \"Title\": \"案件管理\",\n" +
                   "      \"SiteSettings\": {\n" +
                   "        \"Columns\": [\n" +
                   "          {\n" +
                   "            \"ColumnName\": \"NumA\",\n" +
                   "            \"LabelText\": \"状態\",\n" +
                   "            \"Description\": \"状態説明\",\n" +
                   "            \"ChoicesText\": \"Open,1\\nClose,0\"\n" +
                   "          },\n" +
                   "          {\n" +
                   "            \"ColumnName\": \"NumB\",\n" +
                   "            \"LabelText\": \"状態\",\n" +
                   "            \"Description\": \"別説明\"\n" +
                   "          },\n" +
                   "          {\n" +
                   "            \"ColumnName\": \"NumC\",\n" +
                   "            \"LabelText\": \"\",\n" +
                   "            \"Description\": \"説明用\"\n" +
                   "          }\n" +
                   "        ]\n" +
                   "      }\n" +
                   "    }\n" +
                   "  ]\n" +
                   "}";

        File.WriteAllText(jsonPath, json, Encoding.UTF8);

        var config = new DefinitionExtractorConfig
        {
            Input = new DefinitionExtractorConfig.Definition.Input
            {
                SiteExportDefinitionFile = "site-export.json"
            },
            Output = new DefinitionExtractorConfig.Definition.Output
            {
                SiteDefinitionFile = "sites.csv",
                InterfaceDefinitionFile = "interfaces.csv",
                Encoding = "utf-8",
                UseDescriptionAsVariableName = true,
                UseSiteTitleAsVariableName = true,
                ExportAllSites = true
            }
        };

        try
        {
            var sut = new HackPleasanterApi.Generator.JsonDefinitionExtractor.Exporter();

            sut.DoExporter(workDir, config);

            var siteCsv = File.ReadAllText(Path.Combine(workDirPath, "sites.csv"), Encoding.UTF8)
                .Replace("\r\n", "\n")
                .TrimEnd()
                .Split('\n');
            var interfaceCsv = File.ReadAllText(Path.Combine(workDirPath, "interfaces.csv"), Encoding.UTF8)
                .Replace("\r\n", "\n")
                .TrimEnd()
                .Split('\n');

            Assert.Equal("SiteId,Title,IsTarget,SiteVariableName,Memo", siteCsv[0]);
            Assert.Equal("10,案件管理,True,案件管理,", siteCsv[1]);

            Assert.Equal("Title,SiteId,ParentId,InheritPermission,Description,ValidateRequired,ColumnName,LabelText,VariableName,IsTarget,ChoicesText", interfaceCsv[0]);
            Assert.Contains("案件管理,10,0,0,状態説明,,NumA,状態,状態_NumA,True,Open|C|1|L|Close|C|0", interfaceCsv);
            Assert.Contains("案件管理,10,0,0,別説明,,NumB,状態,状態_NumB,True,", interfaceCsv);
            Assert.Contains("案件管理,10,0,0,説明用,,NumC,,説明用,True,", interfaceCsv);
        }
        finally
        {
            if (Directory.Exists(workDirPath))
            {
                Directory.Delete(workDirPath, true);
            }
        }
    }
}
