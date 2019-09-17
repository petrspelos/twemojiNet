using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TwemojiNet.Entities;

namespace TwemojiNet.Utilities
{
    internal static class UnicodeStringParser
    {
        internal static EmojiParseResult Parse(string source)
        {
            var result = new EmojiParseResult
            {
                Codepoints = new List<string>(),
                SourceSplits = Constants.UnicodeRegex.Split(source).Where(s => !string.IsNullOrEmpty(s)).ToList()
            };

            var fullInputCodePoints = ToCodePoints(source);

            var sb = new List<string>();
            foreach(var c in fullInputCodePoints)
            {
                if(c.StartsWith("0"))
                {
                    if(sb.Count == 0) { continue; }

                    if(IsValidEmoji(sb))
                    {
                        result.Codepoints.Add(string.Join("-", sb));
                        sb.Clear();
                    }
                    else
                    {
                        var leftovers = new List<string>(sb);
                        leftovers.Reverse();

                        while(leftovers.Count != 0)
                        {
                            sb = new List<string>(leftovers);
                            sb.Reverse();
                            leftovers.Clear();
                            while(!IsValidEmoji(sb) && sb.Count > 0)
                            {
                                leftovers.Add(sb.Last());
                                sb.RemoveAt(sb.Count - 1);
                            }

                            if(sb.Count == 0)
                            {
                                leftovers.Clear();
                            }
                            else
                            {
                                result.Codepoints.Add(string.Join("-", sb));
                                sb.Clear();
                            }
                        }
                    }

                    continue;
                }

                sb.Add(c);
            }

            if(sb.Count != 0)
            {
                result.Codepoints.Add(string.Join("-", sb));
                sb.Clear();
            }

            return result;
        }

        public static IEnumerable<string> ToCodePoints(string str)
        {
            if (str == null)
                throw new ArgumentNullException("str");

            var codePoints = new List<int>(str.Length);
            for (int i = 0; i < str.Length; i++)
            {
                codePoints.Add(Char.ConvertToUtf32(str, i));
                if (Char.IsHighSurrogate(str[i]))
                    i += 1;
            }

            return codePoints.Select(i => string.Format("{0:X4}", i).ToLower());
        }

        private static bool IsValidEmoji(IEnumerable<string> composite)
        {
            var codepoint = string.Join("-", composite);
            return Constants.StandardCodepoints.Contains(codepoint);
        }
    }
}
