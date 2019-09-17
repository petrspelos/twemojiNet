using System.Linq;
using Xunit;

namespace TwemojiNet.UnitTests
{
    public class ParserTests
    {
        [Theory]
        [InlineData("😀", "1f600")]
        [InlineData("🤟", "1f91f")]
        [InlineData("👈", "1f448")]
        [InlineData("👏", "1f44f")]
        [InlineData("💅", "1f485")]
        [InlineData("🦶", "1f9b6")]
        [InlineData("🦷", "1f9b7")]
        [InlineData("😍", "1f60d")]
        [InlineData("😒", "1f612")]
        [InlineData("💩", "1f4a9")]
        [InlineData("💘", "1f498")]
        [InlineData("💣", "1f4a3")]
        public void SingleSimpleEmoji_ShouldParseCorrectly(string input, string expected)
        {
            var actual = EmojiParser.GetCodepoints(input);

            Assert.Single(actual.Codepoints);
            Assert.Empty(actual.SourceSplit);
            Assert.Equal(expected, actual.Codepoints.First());
        }

        [Theory]
        [InlineData("Hello, 😀",        new[] { "1f600" }, new[] { "Hello, " })]
        [InlineData("🤟...",            new[] { "1f91f" }, new[] { "..." })]
        [InlineData("   👈 text",       new[] { "1f448" }, new[] { "   ", " text" })]
        [InlineData("RANDOM |👏| TEXT", new[] { "1f44f" }, new[] { "RANDOM |", "| TEXT" })]
        [InlineData("💅<-- Emoji",      new[] { "1f485" }, new[] { "<-- Emoji" })]
        [InlineData("_🦶",              new[] { "1f9b6" }, new[] { "_" })]
        [InlineData("Hey there! 🦷",    new[] { "1f9b7" }, new[] { "Hey there! " })]
        [InlineData(" 😍 sup?",         new[] { "1f60d" }, new[] { " ", " sup?" })]
        [InlineData("\"😒\"",           new[] { "1f612" }, new[] { "\"", "\"" })]
        [InlineData(",.💩--",           new[] { "1f4a9" }, new[] { ",.", "--" })]
        [InlineData("'💘'",             new[] { "1f498" }, new[] { "'", "'" })]
        [InlineData("💣^.^",            new[] { "1f4a3" }, new[] { "^.^" })]
        [InlineData("Hello, 😀🤟...",               new[] { "1f600", "1f91f" }, new[] { "Hello, ", "..." })]
        [InlineData("   👈 textRANDOM |👏| TEXT",   new[] { "1f448", "1f44f" }, new[] { "   ", " textRANDOM |", "| TEXT" })]
        [InlineData("💅<-- Emoji_🦶",               new[] { "1f485", "1f9b6" }, new[] { "<-- Emoji_" })]
        [InlineData("Hey there! 🦷 😍 sup?",        new[] { "1f9b7", "1f60d" }, new[] { "Hey there! ", " ", " sup?" })]
        [InlineData("\"😒\",.💩--",                 new[] { "1f612", "1f4a9" }, new[] { "\"", "\",.", "--" })]
        [InlineData("'💘'💣^.^",                    new[] { "1f498", "1f4a3" }, new[] { "'", "'", "^.^" })]
        public void SingleSimpleEmojiMixedWithText_ShouldReturnCorrectCodepointsAndSplits(string input, string[] expectedCodepoints, string[] expectedSplits)
        {
            var actual = EmojiParser.GetCodepoints(input);

            Assert.Equal(expectedCodepoints, actual.Codepoints);
            Assert.Equal(expectedSplits, actual.SourceSplit);
        }

        [Theory]
        [InlineData("Hello, 😀🤟...",               new[] { "1f600", "1f91f" }, new[] { "Hello, ", "..." })]
        public void TwoEmojisOneAfterAnother(string input, string[] expectedCodepoints, string[] expectedSplits)
        {
            var actual = EmojiParser.GetCodepoints(input);

            Assert.Equal(expectedCodepoints, actual.Codepoints);
            Assert.Equal(expectedSplits, actual.SourceSplit);
        }

        [Theory]
        [InlineData("👨🏿‍🚀", new [] { "1f468-1f3ff-200d-1f680" })]
        [InlineData("🕵🏿", new [] { "1f575-1f3ff" })]
        [InlineData("👨‍👩‍👦‍👦", new [] { "1f468-200d-1f469-200d-1f466-200d-1f466" })]
        [InlineData("🏳️‍🌈", new [] { "1f3f3-fe0f-200d-1f308" })]
        [InlineData("👏🏽", new [] { "1f44f-1f3fd" })]
        public void CompositeEmoji_ShouldParseCorrectly(string input, string[] expectedCodepoints)
        {
            var actual = EmojiParser.GetCodepoints(input);

            Assert.Equal(expectedCodepoints, actual.Codepoints);
        }

        [Theory]
        [InlineData("žščřďťňŽŠČŘĎŤŇóúýů 🏳️‍🌈 cool flag?", new [] { "1f3f3-fe0f-200d-1f308" })]
        public void IntegrationTests(string input, string[] expectedCodepoints)
        {
            var actual = EmojiParser.GetCodepoints(input);

            Assert.Equal(expectedCodepoints, actual.Codepoints);
        }
    }
}
