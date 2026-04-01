using HackPleasanterApi.Generator.DebugCommand.CallableCommand;
using HackPleasanterApi.Generator.GenerationCommand.CallableCommand;
using System.CommandLine;
using Xunit;

namespace EntranceCommand.Tests;

public class ProgramTests
{
    [Fact]
    public void BuildRootCommand_BuildsExpectedCommandTree()
    {
        var command = Program.BuildRootCommand();

        Assert.Equal("Pleasanter インターフェースコード生成", command.Description);

        var workingDirectory = Assert.Single(command.Arguments);
        Assert.Equal("WorkingDirectory", workingDirectory.Name);
        Assert.Equal("コマンドの作業ディレクトリ", workingDirectory.Description);

        var subcommands = command.Children.OfType<Command>().ToList();
        Assert.Equal(
            new[]
            {
                GenerationCommandDef.GetCommandName(),
                DefaultConfigurationFileGenerationCommandDef.GetCommandName(),
                DebugCommandDef.GetCommandName()
            },
            subcommands.Select(x => x.Name));
    }

    [Fact]
    public void BuildRootCommand_ContainsNestedDebugCommandStructure()
    {
        var command = Program.BuildRootCommand();

        var debugCommand = Assert.Single(command.Children.OfType<Command>(), x => x.Name == DebugCommandDef.GetCommandName());
        var debugSubcommands = debugCommand.Children.OfType<Command>().Select(x => x.Name).ToList();

        Assert.Equal(new[] { "JsonDefinitionExtractor", "CodeGenerator" }, debugSubcommands);
    }
}
