using HackPleasanterApi.Generator.Libraryrary.Utility;
using Xunit;

namespace HackPleasanterApi.Generator.Library.Tests.Utility;

public class CharacterTypeTests
{
    [Theory]
    [InlineData("Valid_Name123", "Valid_Name123")]
    [InlineData("abc-def", "abc_def")]
    [InlineData("日本語ー項目", "日本語ー項目")]
    [InlineData("1column", "_1column")]
    [InlineData("1 col-umn", "_1_col_umn")]
    public void ReplaceInvalidChars_ReplacesUnsupportedCharacters_AndPrefixesLeadingDigits(
        string input,
        string expected)
    {
        var actual = CharacterType.ReplaceInvalidChars(input);

        Assert.Equal(expected, actual);
    }
}
