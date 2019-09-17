# TwemojiNet

Twemoji.NET is a .NET string parser that can recognize emojis and return their codepoints.

## Ehm, why?

Sometimes, you want to replace the regular emojis with equivalent images.

Most emoji standards (for example Twitter's Twemoji) store these emojis in the `[codepoint].png` format.

Where composite emojis are connected with a dash. For example the 👩‍💻 emoji will be saved as `1f469-200d-1f4bb.png`.

That is: `woman` codepoint, followed by a `Zero Width Joiner` and a `Laptop Computer`.

This can get pretty complex. Especially when you just want to replace some emojis.

## Alright, how do I use this?


```cs
var data = EmojiParser.GetCodepoints("No way! 👩🏿‍🚀 On Mars?! 💢");

// Result:
// data.Codepoints: [ "1f468-1f3ff-200d-1f680", "1f4a2" ]
// data.SourceSplit: ["No way! ", "On Mars?! "]
```

## This looks too complex, have you tried...

This almost drove me to madness, please feel absolutely free to improve this.

I am not happy with this implementation, but it does what I need for now.

## Get on NuGet

Available through [NuGet](https://www.nuget.org/packages/TwemojiNet/).

```
dotnet add package TwemojiNet
```
