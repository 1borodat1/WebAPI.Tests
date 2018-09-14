namespace WebAPI.Tests
{
	using NUnit.Framework;
	using WebAPI.TextProcessing;

	[TestFixture]
	class ClearNewLines_Tests
	{
		[Test, Category("PreCommit")]
		[TestCase("Test\r\ninput\r\nparametr", "Test input parametr")]
		[TestCase("Test,\r\n\r\n\r\ninput,\r\n\r\n\r\n text.", "Test, input, text.")]
		public void Process_clear_all_punctuation_marks(string inputText, string expetedText) {
			var clearTextPunctuator = new ClearNewLines();
			var actualText = clearTextPunctuator.Process(inputText);
			Assert.AreEqual(expetedText, actualText);
		}
	}
}

