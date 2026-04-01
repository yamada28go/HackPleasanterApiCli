using System.CommandLine;
using HackPleasanterApi.Generator.GenerationCommand.CallableCommand;
using Xunit;

namespace HackPleasanterApi.Generator.GenerationCommand.Tests.CallableCommand;

public class GenerationCommandDefTests
{
    [Fact]
    public void GetCommandName_ReturnsExpectedName()
    {
        Assert.Equal("Generation", GenerationCommandDef.GetCommandName());
    }

    [Fact]
    public void MakeCommand_BuildsExpectedMetadata()
    {
        var command = GenerationCommandDef.MakeCommand();

        Assert.Equal("Generation", command.Name);
        Assert.Equal("Pleasanterから生成されたサイトファイルからグルーコードを生成します。", command.Description);

        var argument = Assert.Single(command.Arguments);
        Assert.Equal("ExportFileName", argument.Name);
        Assert.Equal("Pleasanterから生成されたサイトファイル名", argument.Description);

        var option = Assert.Single(command.Options);
        Assert.Equal("config", option.Name);
        Assert.Equal("生成動作設定用の動作設定ファイル", option.Description);
    }
}
