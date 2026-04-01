using HackPleasanterApi.Generator.GenerationCommand.Helper;
using Xunit;

namespace HackPleasanterApi.Generator.GenerationCommand.Tests.Helper;

public class FileHelperTests
{
    [Fact]
    public void FindFile_ReturnsMatchingFileFromNestedDirectories()
    {
        var root = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        var nested = Path.Combine(root, "a", "b");
        Directory.CreateDirectory(nested);
        var targetFile = Path.Combine(nested, "CodeGeneratorConfig.xml");
        File.WriteAllText(targetFile, "<root />");

        try
        {
            var actual = FileHelper.FindFile(root, "CodeGeneratorConfig.xml");

            Assert.Equal(targetFile, actual);
        }
        finally
        {
            if (Directory.Exists(root))
            {
                Directory.Delete(root, true);
            }
        }
    }
}
