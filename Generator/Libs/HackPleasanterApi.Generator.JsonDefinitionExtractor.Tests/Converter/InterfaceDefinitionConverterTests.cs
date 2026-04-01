using HackPleasanterApi.Generator.JsonDefinitionExtractor.Converter;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.Libraryrary.Constant;
using Xunit;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Tests.Converter;

public class InterfaceDefinitionConverterTests
{
    [Fact]
    public void Convert_FlattensColumns_AdjustsChoicesText_AndAddsSeparatorRows()
    {
        var sut = new InterfaceDefinitionConverter();
        var source = new ExportJsonDefinition.Rootobject
        {
            Sites = new[]
            {
                new ExportJsonDefinition.Site
                {
                    SiteId = 20,
                    Title = "B site",
                    SiteSettings = new ExportJsonDefinition.Sitesettings
                    {
                        Columns = new[]
                        {
                            new ExportJsonDefinition.Column
                            {
                                ColumnName = "NumB",
                                LabelText = "Label B",
                                Description = "Desc B",
                                ValidateRequired = true,
                                ChoicesText = "Open,1\nClose,0"
                            },
                            new ExportJsonDefinition.Column
                            {
                                ColumnName = "NumA",
                                LabelText = "Label A"
                            }
                        }
                    }
                },
                new ExportJsonDefinition.Site
                {
                    SiteId = 10,
                    Title = "A site",
                    SiteSettings = new ExportJsonDefinition.Sitesettings
                    {
                        Columns = new[]
                        {
                            new ExportJsonDefinition.Column
                            {
                                ColumnName = "ClassA",
                                LabelText = "Class A"
                            }
                        }
                    }
                },
                new ExportJsonDefinition.Site
                {
                    SiteId = 30,
                    Title = "No settings",
                    SiteSettings = null
                }
            }
        };

        var actual = sut.Convert(source).ToList();

        Assert.Equal(5, actual.Count);

        Assert.Equal(10, actual[0].SiteId);
        Assert.Equal("ClassA", actual[0].ColumnName);

        Assert.Equal(0, actual[1].SiteId);
        Assert.Null(actual[1].ColumnName);

        Assert.Equal(20, actual[2].SiteId);
        Assert.Equal("NumA", actual[2].ColumnName);

        Assert.Equal(20, actual[3].SiteId);
        Assert.Equal("NumB", actual[3].ColumnName);
        Assert.Equal($"Open{CSVConstant.ChoicesText_ColumnSeparator}1{CSVConstant.ChoicesText_NewLine}Close{CSVConstant.ChoicesText_ColumnSeparator}0", actual[3].ChoicesText);
        Assert.True(actual[3].ValidateRequired);

        Assert.Equal(0, actual[4].SiteId);
        Assert.Null(actual[4].ColumnName);
    }
}
