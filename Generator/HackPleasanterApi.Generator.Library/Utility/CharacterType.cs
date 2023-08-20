using System;
using System.Text.RegularExpressions;

namespace HackPleasanterApi.Generator.Libraryrary.Utility
{
	public class CharacterType
	{
        /// <summary>
        /// 使用できない文字列を置換する
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ReplaceInvalidChars(string input)
        {

            // C#で使用できない文字を表す正規表現
            // 以下だと「ー」が使えないので、
            //            string pattern = @"[^\p{Ll}\p{Lu}\p{Lt}\p{Lo}\p{Nd}\p{Mn}\p{Pc}]";
            // 以下に置換する
            string pattern = @"[^\p{L}\p{M}\p{N}\p{Pc}]";

            // 不正な文字を_に置換する
            string result = Regex.Replace(input, pattern, "_");

            //行頭が数字で始まっている場合、使えない文字列となっているので_をつけて数値を外す
            {
                string num_pattern = @"^(?=\d)";
                string replacement = "_";

                result = Regex.Replace(input, num_pattern, replacement);
            }

            return result;

#if false

            // C#で使用できない文字を定義します。
            // これには、空白、制御文字、ASCIIコード0～31、およびUnicodeカテゴリー"Other_Not_Assigned"の文字が含まれます。
            var invalidChars = Enumerable.Range(0, 32).Concat(new int[] { 127 }).Concat(
                Enumerable.Range(0xD800, 0xE000 - 0xD800)).Concat(
                Enumerable.Range(0xFDD0, 0xFDF0 - 0xFDD0)).Concat(
                Enumerable.Range(0xFFFE, 0x10000 - 0xFFFE))
                .Select(c => (char)c).ToArray();

            // 渡された文字列の中から使用できない文字を検索し、_に置換します。
            var result = new StringBuilder(input.Length);
            foreach (var c in input)
            {
                result.Append(invalidChars.Contains(c) ? '_' : c);
            }


            // その他、使えない文字を置換する

            return result.ToString();

#endif
        }
    }
}

