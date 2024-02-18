namespace HackPleasanterApi.Generator.GenerationCommand.Helper;

public static class TempResource
{
    // https://takap-tech.com/entry/2019/02/24/114210
    // 使用後に消える一時フォルダを使用するためのコンテキストを提供します。
    public static void FolderContext(Action<string> f)
    {
        string path = string.Empty;
        try
        {
            // 一時フォルダに作業用のサブフォルダを作成
            path = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(path);

            f(path);
        }
        finally
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }
    }
}
