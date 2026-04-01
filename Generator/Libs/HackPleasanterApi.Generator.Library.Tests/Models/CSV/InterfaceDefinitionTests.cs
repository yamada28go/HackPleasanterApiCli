using HackPleasanterApi.Generator.Library.Models.CSV;
using Xunit;

namespace HackPleasanterApi.Generator.Library.Tests.Models.CSV;

public class InterfaceDefinitionTests
{
    [Fact]
    public void HasChoicesTextInfos_ReturnsFalse_WhenListIsEmpty()
    {
        var sut = new InterfaceDefinition();

        Assert.False(sut.HasChoicesTextInfos);
    }

    [Fact]
    public void HasChoicesTextInfos_ReturnsTrue_WhenListContainsItems()
    {
        var sut = new InterfaceDefinition
        {
            ChoicesTextInfos = new List<InterfaceDefinition.Definition.ChoicesTextInfo>
            {
                new InterfaceDefinition.Definition.ChoicesTextInfo
                {
                    VariableName = "Open",
                    Description = "Open status",
                    Value = "1"
                }
            }
        };

        Assert.True(sut.HasChoicesTextInfos);
    }
}
