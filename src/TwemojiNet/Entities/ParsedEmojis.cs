using System.Collections.Generic;

namespace TwemojiNet.Entities
{
    public sealed class ParsedEmojis
    {
        public IEnumerable<string> Codepoints { get; private set; }
        public IEnumerable<string> SourceSplit { get; private set; }

        internal ParsedEmojis(IEnumerable<string> codepoints, IEnumerable<string> sourceSplit)
        {
            Codepoints = codepoints;
            SourceSplit = sourceSplit;
        }
    }
}
