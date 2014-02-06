
namespace Example.Tests
open System
open NUnit.Framework


// Nunit use PlatformAttribute to skip test based on where are executed
// Usefull to replace #IF MONO and document why test is disabled
// see http://www.nunit.org/index.php?p=platform&r=2.6.3 for more info
// 
// include/exclude by tag like:
// Win Unix MacOsX Windows8 Net-3.5 Mono-4.0 Net Mono
//
// case insensitive, comma separed
//
// PROTIP: add a Reason like:
//   mono bug https://bugzilla.xamarin.com/show_bug.cgi?id=1234
//   fsharp issue https://github.com/fsharp/fsharp/issues/188
//   platform specific library
//
// NOTE: Mono run also on Windows, dont use tag mono to filter Unix/MacOsx
//
[<TestFixture>]
type FilterByPlatform() = 

    let isMono = typeof<System.Type>.GetType().Name = "MonoType"

    [<Test>]
    member x.AllPlatforms  () =
        Assert.Pass()

    [<Test>]
    [<Platform(Include = "mono, Macosx", Reason = "check path resolution of '/Library/Application Support'")>]
    member x.OnlyMono () =
        Assert.IsTrue isMono

    [<Test>]
    [<Platform("Net")>]
    member x.OnlyDotNet () =
        Assert.IsFalse isMono

    [<Test>]
    [<Platform(Exclude = "Mono", Reason = "mono bug https://bugzilla.xamarin.com/show_bug.cgi?id=5678")>]
    member x.NotMono () =
        Assert.IsFalse isMono

    [<Test>]
    [<Platform(Include = "Net-3.5", Exclude = "Net-2.0")>]
    member x.OnlyDotNet35Not20 () =
        Assert.IsFalse isMono

