using GdUnit4;
using Godot;
using static GdUnit4.Assertions;

namespace ClashAndSear.scripts.tests;

[TestSuite]
public class Test
{
    [TestCase]
     [RequireGodotRuntime]
    public void IsEqual()
    {
         GD.Print("Aaaaah");
        AssertThat("This is a test message").IsEqual("This is a test message");
        // AssertThat("This is a test message").IsEqual("This is a test message");
    }
}