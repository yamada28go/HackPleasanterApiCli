using HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using Xunit;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Tests.Converter;

public class SiteDefinitionConverterTests
{
    [Fact]
    public void Convert_DeduplicatesBySiteId_AndSortsAscending()
    {
        var sut = new SiteDefinitionConverter();
        var source = new ExportJsonDefinition.Rootobject
        {
            Sites = new[]
            {
                new ExportJsonDefinition.Site { SiteId = 30, Title = "Zeta" },
                new ExportJsonDefinition.Site { SiteId = 10, Title = "Alpha" },
                new ExportJsonDefinition.Site { SiteId = 10, Title = "Alpha duplicate" }
            }
        };

        var actual = sut.Convert(source).ToList();

        Assert.Collection(
            actual,
            first =>
            {
                Assert.Equal(10, first.SiteId);
                Assert.Equal("Alpha", first.Title);
            },
            second =>
            {
                Assert.Equal(30, second.SiteId);
                Assert.Equal("Zeta", second.Title);
            });
    }
}
