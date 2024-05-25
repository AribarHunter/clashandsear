// GdUnit generated TestSuite
using Godot;
using GdUnit4;

namespace GdUnitDefaultTestNamespace
{
	using static Assertions;
	using static Utils;

	[TestSuite]
	public class mainTest
	{
		// TestSuite generated from
		private const string sourceClazzPath = "C:/Code/clashandsear/scripts/main.cs";
		[TestCase]
		public void gonnaTestThis()
		{
			AssertNotYetImplemented();
		}

		[TestCase]
		public void Example()
		{
			AssertString("This is an example message")
			.HasLength(26)
			.StartsWith("This is an ex");
		}

		[TestCase]
		public void ExampleButWillFail()
		{
			AssertString("Diss is an example message")
			.HasLength(26)
			.StartsWith("This is an ex");
		}
	}
}