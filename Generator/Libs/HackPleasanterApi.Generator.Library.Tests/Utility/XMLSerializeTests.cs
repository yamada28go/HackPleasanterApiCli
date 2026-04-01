using System.Xml.Serialization;
using HackPleasanterApi.Generator.Library.Utility;
using Xunit;

namespace HackPleasanterApi.Generator.Library.Tests.Utility;

public class XMLSerializeTests
{
    [Fact]
    public void SerializeAndDeserialize_RoundTripsObject()
    {
        var tempFile = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid():N}.xml");
        var expected = new TestXmlModel
        {
            Id = 42,
            Name = "sample"
        };

        try
        {
            XMLSerialize.Serialize(expected, tempFile);

            var actual = XMLSerialize.Deserialize<TestXmlModel>(tempFile);

            Assert.Equal(expected.Id, actual.Id);
            Assert.Equal(expected.Name, actual.Name);
        }
        finally
        {
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);
            }
        }
    }

    public class TestXmlModel
    {
        [XmlElement]
        public int Id { get; set; }

        [XmlElement]
        public string Name { get; set; } = string.Empty;
    }
}
