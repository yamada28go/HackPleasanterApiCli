using HackPleasanterApi.Generator.GenerationCommand.CallableCommand;
using Xunit;

namespace HackPleasanterApi.Generator.GenerationCommand.Tests.CallableCommand;

public class DefaultConfigurationFileGenerationCommandDefTests
{
    [Fact]
    public void GetCommandName_ReturnsExpectedName()
    {
        Assert.Equal("DefaultConfigurationFileGeneration", DefaultConfigurationFileGenerationCommandDef.GetCommandName());
    }

    [Fact]
    public void MakeCommand_BuildsExpectedMetadata()
    {
        var command = DefaultConfigurationFileGenerationCommandDef.MakeCommand();

        Assert.Equal("DefaultConfigurationFileGeneration", command.Name);
        Assert.Equal("コマンドの動作設定用のコンフィグファイルのひな形を生成します。", command.Description);

        var argument = Assert.Single(command.Arguments);
        Assert.Equal("ConfigurationFileName", argument.Name);
        Assert.Equal("出力されるコンフィグファイル名", argument.Description);

        var option = Assert.Single(command.Options);
        Assert.Equal("full", option.Name);
        Assert.Equal("デバッグ用設定を含む全内容で生成", option.Description);
    }
}
