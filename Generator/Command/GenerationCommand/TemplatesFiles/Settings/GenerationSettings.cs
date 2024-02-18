using System;
namespace HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles.Settings
{

    public class DebugSettings
    {
        public string? TemplatePath { get; set; }

    }

    /// <summary>
    /// コードの生成設定
    /// </summary>
    public class GenerationSettings
    {
        public CsharpSettings? CsharpSettings { get; init; }
        public ScriptTsSettings? ScriptTsSettings { get; init; }
        public PostgreSQLSettings? PostgreSQLSettings { get; init; }


        public GenerationSettings(CsharpSettings? csharpSettings, ScriptTsSettings? scriptTsSettings, PostgreSQLSettings? postgreSQLSettings)
        {
            this.CsharpSettings = csharpSettings;
            this.ScriptTsSettings = scriptTsSettings;
            this.PostgreSQLSettings = postgreSQLSettings;
        }

        public GenerationSettings() { }


        public static GenerationSettings MakeDefault()
        {
            var tc = new GenerationSettings(
                new CsharpSettings(),
                new ScriptTsSettings(),
                new PostgreSQLSettings()
            );

            return tc;
        }

        public static GenerationSettings MakeFullSetDefault()
        {
            var tc = new GenerationSettings(
                new CsharpSettings(),
                new ScriptTsSettings(),
                new PostgreSQLSettings()
            );

            var debug = new DebugSettings { TemplatePath = "Path/to/template/file" };

            tc.CsharpSettings.DebugSettings = debug;
            tc.CsharpSettings.Namespace = "test.namespace";

            tc.ScriptTsSettings.DebugSettings = debug;
            tc.PostgreSQLSettings.DebugSettings = debug;

            return tc;
        }


    }

    public class SettingsBase
    {
        public string TemplateVersion { get; init; }

        public DebugSettings? DebugSettings { get; set; } = null;


        public SettingsBase(string rev)
        {
            this.TemplateVersion = rev;
        }
    }

    /// <summary>
    /// C#の設定
    /// </summary>
    public class CsharpSettings : SettingsBase
    {
        public static string DefaultVer = "0.3";

        public string? Namespace { get; set; } = "PleasanterApiLib";
        public string? ProjectName { get; set; } = "PleasanterApiLib";

        public bool ForcedOverwrite { get; set; } = true;

        public CsharpSettings() : base(CsharpSettings.DefaultVer)
        {
        }


        public CsharpSettings(string rev, string? namesapce = null)
            : base(rev)
        {
            this.Namespace = namesapce;
        }
    }

    public class ScriptTsSettings : SettingsBase
    {
        public static string DefaultVer = "0.4";
        public ScriptTsSettings()
    : base(ScriptTsSettings.DefaultVer)
        {
        }


        public ScriptTsSettings(string rev)
            : base(rev)
        {
        }
    }

    public class PostgreSQLSettings : SettingsBase
    {
        public static string DefaultVer = "0.2";
        public PostgreSQLSettings()
    : base(PostgreSQLSettings.DefaultVer)
        {
        }


        public PostgreSQLSettings(string rev)
            : base(rev)
        {
        }
    }

}

