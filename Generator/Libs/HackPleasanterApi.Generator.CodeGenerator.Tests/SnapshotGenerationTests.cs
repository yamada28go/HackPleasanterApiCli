using HackPleasanterApi.Generator.CodeGenerator;
using HackPleasanterApi.Generator.CodeGenerator.Configs;
using HackPleasanterApi.Generator.CodeGenerator.Loder;
using Xunit;

namespace HackPleasanterApi.Generator.CodeGenerator.Tests;

public class SnapshotGenerationTests
{
    [Fact]
    public void DoGenerae_WithSampleTemplates_ProducesExpectedSnapshots()
    {
        var projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", ".."));
        var testDataRoot = Path.Combine(projectRoot, "TestData", "SnapshotGeneration");
        var outputRoot = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputRoot);

        try
        {
            var config = CreateConfig(testDataRoot, outputRoot);
            var sites = new CSVLoader().DoLoad(config).ToList();

            foreach (var site in sites)
            {
                site.GeneratedDate = "2026-04-01";
            }

            var context = new GenerationContext
            {
                Sites = sites
            };

            new HackPleasanterApi.Generator.CodeGenerator.Generators.Generator().DoGenerae(config, context);

            var actualFiles = Directory.GetFiles(outputRoot, "*", SearchOption.AllDirectories)
                .Select(path => new
                {
                    RelativePath = Path.GetRelativePath(outputRoot, path).Replace('\\', '/'),
                    Content = Normalize(File.ReadAllText(path))
                })
                .OrderBy(x => x.RelativePath)
                .ToList();

            var expectedRoot = Path.Combine(testDataRoot, "Expected");
            var expectedFiles = Directory.GetFiles(expectedRoot, "*", SearchOption.AllDirectories)
                .Select(path => new
                {
                    RelativePath = Path.GetRelativePath(expectedRoot, path).Replace('\\', '/'),
                    Content = Normalize(File.ReadAllText(path))
                })
                .OrderBy(x => x.RelativePath)
                .ToList();

            Assert.Equal(expectedFiles.Select(x => x.RelativePath), actualFiles.Select(x => x.RelativePath));

            foreach (var expected in expectedFiles)
            {
                var actual = Assert.Single(actualFiles, x => x.RelativePath == expected.RelativePath);
                Assert.Equal(expected.Content, actual.Content);
            }
        }
        finally
        {
            if (Directory.Exists(outputRoot))
            {
                Directory.Delete(outputRoot, true);
            }
        }
    }

    private static GeneratorConfig CreateConfig(string testDataRoot, string outputRoot)
    {
        return new GeneratorConfig
        {
            InputFiles = new GeneratorConfig.Definition.InputFiles
            {
                SiteDefinitionFile = Path.Combine(testDataRoot, "Input", "Sites.csv"),
                InterfaceDefinitionFile = Path.Combine(testDataRoot, "Input", "Interface.csv"),
                Encoding = "utf-8"
            },
            OutputConfig = new GeneratorConfig.Definition.OutputConfig
            {
                OutputDirectory = outputRoot
            },
            TemplateFiles = new List<GeneratorConfig.Definition.TemplateFiles>
            {
                new GeneratorConfig.Definition.TemplateFiles
                {
                    TemplateFileName = Path.Combine(testDataRoot, "Templates", "ServiceTemplate.txt"),
                    OutputSubdirectoryName = "Services",
                    HeadPrefix = "",
                    EndPrefix = "Service",
                    OutputExtension = "cs",
                    Encoding = "utf-8"
                },
                new GeneratorConfig.Definition.TemplateFiles
                {
                    TemplateFileName = Path.Combine(testDataRoot, "Templates", "ModelTemplate.txt"),
                    OutputSubdirectoryName = "Models",
                    HeadPrefix = "",
                    EndPrefix = "Model",
                    OutputExtension = "cs",
                    Encoding = "utf-8"
                }
            },
            CodeConfig = new GeneratorConfig.Definition.CodeConfig
            {
                NameSpace = "Snapshot.Generated"
            }
        };
    }

    private static string Normalize(string value)
    {
        return value.Replace("\r\n", "\n").TrimEnd();
    }
}
