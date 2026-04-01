using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles.Settings;
using Xunit;

namespace HackPleasanterApi.Generator.GenerationCommand.Tests.TemplatesFiles.Settings;

public class GenerationSettingsTests
{
    [Fact]
    public void MakeDefault_CreatesAllDefaultSettings()
    {
        var actual = GenerationSettings.MakeDefault();

        Assert.NotNull(actual.CsharpSettings);
        Assert.NotNull(actual.ScriptTsSettings);
        Assert.NotNull(actual.PostgreSQLSettings);
        Assert.Equal("PleasanterApiLib", actual.CsharpSettings!.Namespace);
        Assert.Equal("PleasanterApiLib", actual.CsharpSettings.ProjectName);
        Assert.True(actual.CsharpSettings.ForcedOverwrite);
        Assert.Null(actual.CsharpSettings.DebugSettings);
        Assert.Null(actual.ScriptTsSettings!.DebugSettings);
        Assert.Null(actual.PostgreSQLSettings!.DebugSettings);
    }

    [Fact]
    public void MakeFullSetDefault_SetsDebugInformationForAllTemplates()
    {
        var actual = GenerationSettings.MakeFullSetDefault();

        Assert.NotNull(actual.CsharpSettings);
        Assert.NotNull(actual.ScriptTsSettings);
        Assert.NotNull(actual.PostgreSQLSettings);

        Assert.Equal("test.namespace", actual.CsharpSettings!.Namespace);
        Assert.Equal("Path/to/template/file", actual.CsharpSettings.DebugSettings!.TemplatePath);
        Assert.Equal("Path/to/template/file", actual.ScriptTsSettings!.DebugSettings!.TemplatePath);
        Assert.Equal("Path/to/template/file", actual.PostgreSQLSettings!.DebugSettings!.TemplatePath);
    }
}
