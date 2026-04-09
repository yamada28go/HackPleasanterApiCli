using HackPleasanterApi.Generator.JsonDefinitionExtractor.Models;
using HackPleasanterApi.Generator.JsonDefinitionExtractor.Reader;
using Xunit;

namespace HackPleasanterApi.Generator.JsonDefinitionExtractor.Tests.Reader;

public class JsonReaderTests
{
    [Fact]
    public void ReadAll_DeserializesJsonFile()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.json");
        var json = "{\n" +
                   "  \"Sites\": [\n" +
                   "    {\n" +
                   "      \"SiteId\": 100,\n" +
                   "      \"Title\": \"Projects\",\n" +
                   "      \"SiteSettings\": {\n" +
                   "        \"Columns\": [\n" +
                   "          {\n" +
                   "            \"ColumnName\": \"NumA\",\n" +
                   "            \"LabelText\": \"Label A\"\n" +
                   "          }\n" +
                   "        ]\n" +
                   "      }\n" +
                   "    }\n" +
                   "  ]\n" +
                   "}";

        try
        {
            File.WriteAllText(tempFile, json);
            var sut = new JsonReader();

            var actual = sut.ReadAll<ExportJsonDefinition.Rootobject>(tempFile);

            Assert.Single(actual.Sites);
            Assert.Equal(100, actual.Sites[0].SiteId);
            Assert.Equal("Projects", actual.Sites[0].Title);
            Assert.Single(actual.Sites[0].SiteSettings.Columns);
            Assert.Equal("NumA", actual.Sites[0].SiteSettings.Columns[0].ColumnName);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }
}
