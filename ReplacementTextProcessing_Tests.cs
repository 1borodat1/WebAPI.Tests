using NSubstitute;
using NUnit.Framework;
using System;
using WebAPI.TextProcessing;

namespace WebAPI.Tests
{
	[TestFixture]
	class ReplacementTextProcessing_Tests
    {

		[Test, Category("PreCommit")]
		public void Process_when_not_transfer_input_parametrs() {
			var inputText = "Test input text.";
			var baseTextProcessing = new ReplacementTextProcessing();
			var exp = Assert.Throws<ArgumentNullException>(() => {
				baseTextProcessing.Process(inputText);
			});
			Assert.IsTrue(exp.Message.Contains("Pattern:, ReplacingPattern:"));
		}

		[Test, Category("PreCommit")]
		public void Process_when_add_null_processing() {
			var inputText = "Test input text.";
			var baseTextProcessing = new ReplacementTextProcessing("T","t");
			baseTextProcessing.AddTextProcessing(null);
			var actualText = baseTextProcessing.Process(inputText);
			Assert.AreEqual(inputText.ToLower(), actualText);
		}

		[Test, Category("PreCommit")]
		public void Process_when_add_one_processing() {
			var inputText = "Test input text.";
			var expectedText = "testinputtext.";
			var baseTextProcessing = new ReplacementTextProcessing("T", "t");
			var textProcessing = Substitute.For<ITextProcessing>();
			textProcessing.Process(inputText).Returns("Testinputtext.");
			baseTextProcessing.AddTextProcessing(textProcessing);
			var actualText = baseTextProcessing.Process(inputText);
			Assert.AreEqual(expectedText, actualText);
		}

		[Test, Category("PreCommit")]
		public void Process_when_add_more_then_one_processing() {
			var inputText = "Test, input, text.";
			var expectedText = "testinputtext";
			var baseTextProcessing = new ReplacementTextProcessing("T", "t");
			var clearSpaceTextProcessing = Substitute.For<ITextProcessing>();
			var clearPunctuationTextProcessing = Substitute.For<ITextProcessing>();
			clearSpaceTextProcessing.Process(inputText).Returns("Testinputtext.");
			clearPunctuationTextProcessing.Process("Testinputtext.").Returns("Testinputtext");
			baseTextProcessing.AddTextProcessing(clearSpaceTextProcessing);
			baseTextProcessing.AddTextProcessing(clearPunctuationTextProcessing);
			var actualText = baseTextProcessing.Process(inputText);
			Assert.AreEqual(expectedText, actualText);
		}
	}
}
