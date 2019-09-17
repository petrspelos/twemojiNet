using System.Collections.Generic;

namespace TwemojiNet.Entities
{
    internal struct EmojiParseResult
    {
        public ICollection<string> Codepoints { get; set; }
        public ICollection<string> SourceSplits { get; set; }
    }
}
