using HackPleasanterApi.Generator.DebugCommand.CallableCommand;
using System.CommandLine;
using Xunit;

namespace HackPleasanterApi.Generator.DebugCommand.Tests.CallableCommand;

public class DebugCommandDefTests
{
    [Fact]
    public void GetCommandName_ReturnsExpectedName()
    {
        Assert.Equal("DebugCommand", DebugCommandDef.GetCommandName());
    }

    [Fact]
    public void MakeCommand_BuildsExpectedCommandTree()
    {
        var command = DebugCommandDef.MakeCommand();

        Assert.Equal("DebugCommand", command.Name);
        Assert.Equal("[デバッグ用] Generationの内部動作で使用される個別コマンドを指定して実行します。", command.Description);

        var subcommands = command.Children.OfType<Command>().ToList();
        Assert.Equal(new[] { "JsonDefinitionExtractor", "CodeGenerator" }, subcommands.Select(x => x.Name));

        var jsonDefinitionExtractor = Assert.Single(subcommands, x => x.Name == "JsonDefinitionExtractor");
        Assert.Equal(new[] { "Export", "GetConfiguration" }, jsonDefinitionExtractor.Children.OfType<Command>().Select(x => x.Name));

        var codeGenerator = Assert.Single(subcommands, x => x.Name == "CodeGenerator");
        Assert.Equal(new[] { "Generate", "DefaultConfiguration" }, codeGenerator.Children.OfType<Command>().Select(x => x.Name));
    }
}
