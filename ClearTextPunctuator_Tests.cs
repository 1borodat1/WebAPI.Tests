namespace WebAPI.Tests
{
	using NUnit.Framework;
	using WebAPI.TextProcessing;

	[TestFixture]
	class ClearTextPunctuator_Tests
    {
		[Test, Category("PreCommit")]
		[TestCase(",<.>/?;:'[{]}|!@#$%^&*()!№;%:?*+", "")]
		[TestCase("Test, input,  text.", "Test input  text")]
		public void Process_clear_all_punctuation_marks(string inputText, string expetedText) {
			var clearTextPunctuator = new ClearTextPunctuator();
			var actualText = clearTextPunctuator.Process(inputText);
			Assert.AreEqual(expetedText, actualText);
		}
	}
}
