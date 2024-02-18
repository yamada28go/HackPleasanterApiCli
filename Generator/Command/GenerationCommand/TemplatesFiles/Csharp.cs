/**
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 * 
 *   http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 * */

using System.Text.RegularExpressions;
using HackPleasanterApi.Generator.SimpleCommand.TemplatesFiles.Settings;

using Zx;

namespace HackPleasanterApi.Generator.GenerationCommand.TemplatesFiles
{


    public class Csharp : DownloaderBase<CsharpSettings>
    {
        private static readonly string DefaultNamespace = "CsharpSamples.Generated";

        public Csharp(string refLocalPath, CsharpSettings ver) : base("https://github.com/yamada28go/HackPleasanterApi.Csharp.git", refLocalPath, ver)
        {
        }


        protected override async Task PostProcessingImp(string workPath)
        {

            // 名前空間を調整する
            if (null != settingsBase_.Namespace)
            {
                ReplaceTextInFiles(workPath, "*.cs", Csharp.DefaultNamespace, settingsBase_.Namespace);
            }

            if (settingsBase_.ProjectName is null)
            {
                // プロジェクト名が指定されていなければフォーマットしない
                return;
            }

            // 作業パスを移動する
            await $"cd {workPath}";

            if (true == settingsBase_.ForcedOverwrite)
            {
                // 強制上書きモードの場合
                logger.Info($"強制上書きモードであるため既存プロジェクトを消去します。 : {settingsBase_.ProjectName}");

                var rm = await $"rm -rf {settingsBase_.ProjectName}";
                logger.Debug($"cmd out : ${rm}");
            }

            //ライブラリを作る
            var r = await $"dotnet new classlib -n {settingsBase_.ProjectName} -f net6.0";
            logger.Debug($"cmd out : ${r}");

            // "Class1.cs"が作成されるけど不要なので消す
            _ = DeleteFile(Path.Combine(workPath, settingsBase_.ProjectName, "Class1.cs"));


            // コードをコピーする
            var cr = await $"cp -r  Generated/* {settingsBase_.ProjectName}";
            logger.Debug($"cmd out : ${cr}");

            //コードをフォーマットする
            var fr = await $"dotnet format {settingsBase_.ProjectName}/{settingsBase_.ProjectName}.csproj";
            logger.Debug($"cmd out : ${fr}");

            // パッケージを追加
            await $"cd {settingsBase_.ProjectName}";
            var pr = await $"dotnet add package HackPleasanterApi.Csharp";
            logger.Debug($"cmd out : ${pr}");

            // 生成結果を削除
            if (settingsBase_.ProjectName is not null)
            {
                DeleteDirectory(Path.Combine(workPath, "Generated"));
            }

        }

        #region 補助関数


        private static void ReplaceTextInFiles(string folderPath, string filePattern, string searchText, string replaceText)
        {
            // 指定したフォルダ内のすべてのファイルを取得
            foreach (string file in Directory.EnumerateFiles(folderPath, filePattern, SearchOption.AllDirectories))
            {
                // ファイルの内容を読み取る
                string content = File.ReadAllText(file);

                // 文字列を置換する
                string newContent = Regex.Replace(content, Regex.Escape(searchText), replaceText);

                // 新しい内容でファイルを上書きする
                File.WriteAllText(file, newContent);
            }
        }

        private static void DeleteDirectory(string directoryPath)
        {
            try
            {
                // ディレクトリが存在するか確認
                if (Directory.Exists(directoryPath))
                {
                    // ディレクトリを再帰的に削除
                    Directory.Delete(directoryPath, true);
                    Console.WriteLine($"{directoryPath} has been deleted successfully.");
                }
                else
                {
                    Console.WriteLine($"{directoryPath} does not exist.");
                }
            }
            catch (Exception e)
            {
                // エラー処理
                Console.WriteLine($"An error occurred: {e.Message}");
            }
        }


        // ファイルを削除する関数
        private static string DeleteFile(string filePath)
        {
            // ファイルが存在するか確認します。
            if (File.Exists(filePath))
            {
                try
                {
                    // ファイルを削除します。
                    File.Delete(filePath);
                    return $"{filePath} has been deleted.";
                }
                catch (IOException e)
                {
                    // 削除中にエラーが発生した場合は、エラーメッセージを返します。
                    return "An error occurred while trying to delete the file: " + e.Message;
                }
            }
            else
            {
                // ファイルが存在しない場合は、その旨を返します。
                return "The file does not exist.";
            }
        }

        #endregion
    }
}

