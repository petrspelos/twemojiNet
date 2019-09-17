using TwemojiNet.Entities;
using TwemojiNet.Utilities;

namespace TwemojiNet
{
    public static class EmojiParser
    {
        public static ParsedEmojis GetCodepoints(string source)
        {
            var result = UnicodeStringParser.Parse(source);
            return new ParsedEmojis(result.Codepoints, result.SourceSplits);
        }
    }
}
