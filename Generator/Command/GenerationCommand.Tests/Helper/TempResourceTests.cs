using HackPleasanterApi.Generator.GenerationCommand.Helper;
using Xunit;

namespace HackPleasanterApi.Generator.GenerationCommand.Tests.Helper;

public class TempResourceTests
{
    [Fact]
    public void FolderContext_CreatesAndDeletesTemporaryDirectory()
    {
        string? capturedPath = null;

        TempResource.FolderContext(path =>
        {
            capturedPath = path;
            Assert.True(Directory.Exists(path));
            File.WriteAllText(Path.Combine(path, "test.txt"), "ok");
        });

        Assert.NotNull(capturedPath);
        Assert.False(Directory.Exists(capturedPath));
    }
}
