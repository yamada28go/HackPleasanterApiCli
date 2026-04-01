using System.Text;
using HackPleasanterApi.Generator.Library.Models.CSV;
using HackPleasanterApi.Generator.Library.Models.DB;
using HackPleasanterApi.Generator.Library.Service;
using Xunit;

namespace HackPleasanterApi.Generator.Library.Tests.Service;

public class CSVExportTests
{
    [Fact]
    public void WriteSiteDefinition_WritesCsvWithExpectedColumnOrder()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.csv");
        var sut = new CSVExport();
        var definitions = new[]
        {
            new SiteDefinition
            {
                SiteId = 100,
                Title = "Projects",
                IsTarget = true,
                SiteVariableName = "ProjectsSite",
                Memo = "memo"
            }
        };

        try
        {
            sut.WriteSiteDefinition(definitions, tempFile, "utf-8");

            var csv = File.ReadAllText(tempFile, Encoding.UTF8);
            var lines = csv.Replace("\r\n", "\n").TrimEnd().Split('\n');

            Assert.Equal("SiteId,Title,IsTarget,SiteVariableName,Memo", lines[0]);
            Assert.Equal("100,Projects,True,ProjectsSite,memo", lines[1]);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    [Fact]
    public void DoExportSiteInformation_DeduplicatesBySiteId_AndSortsAscending()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.csv");
        var sut = new CSVExport();
        var models = new[]
        {
            CreateSiteModel(30, "Zeta", "{\"Columns\":[{\"ColumnName\":\"NumA\",\"LabelText\":\"A\"}]}"),
            CreateSiteModel(10, "Alpha", "{\"Columns\":[{\"ColumnName\":\"NumB\",\"LabelText\":\"B\"}]}"),
            CreateSiteModel(10, "Alpha duplicate", "{\"Columns\":[{\"ColumnName\":\"NumC\",\"LabelText\":\"C\"}]}"),
            CreateSiteModel(20, "No columns", "{\"Columns\":null}"),
            CreateSiteModel(40, "No settings", null)
        };

        try
        {
            sut.doExportSiteInformation(models, tempFile, "utf-8");

            var csv = File.ReadAllText(tempFile, Encoding.UTF8);
            var lines = csv.Replace("\r\n", "\n").TrimEnd().Split('\n');

            Assert.Equal("SiteId,Title,IsTarget,SiteVariableName,Memo", lines[0]);
            Assert.Equal("10,Alpha,False,,", lines[1]);
            Assert.Equal("30,Zeta,False,,", lines[2]);
            Assert.Equal(3, lines.Length);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    private static SiteModel CreateSiteModel(long siteId, string title, string? siteSettings)
    {
        var model = new SiteModel
        {
            SiteId = siteId,
            Title = title
        };

        model.SiteSettings = siteSettings!;

        return model;
    }
}
