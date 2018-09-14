using NSubstitute;
using NUnit.Framework;
using WebAPI.TextProcessing;

namespace WebAPI.Tests
{
	[TestFixture]
	public class BaseTextProcessing_Tests
	{
		[Test, Category("PreCommit")]
		public void AddTextProcessing_when_empty_processings() {
			var inputText = "Test input text.";
			var baseTextProcessing = new BaseTextProcessing();
			var actualText = baseTextProcessing.Process(inputText);
			Assert.AreEqual(inputText, actualText);
		}

		[Test, Category("PreCommit")]
		[TestCase(null)]
		public void AddTextProcessing_when_add_null_processing(ITextProcessing textProcessing) {
			var inputText = "Test input text.";
			var baseTextProcessing = new BaseTextProcessing();
			baseTextProcessing.AddTextProcessing(textProcessing);
			var actualText = baseTextProcessing.Process(inputText);
			Assert.AreEqual(inputText, actualText);
		}

		[Test, Category("PreCommit")]
		public void AddTextProcessing_when_add_one_processing() {
			var inputText = "Test input text.";
			var expectedText = "Testinputtext.";
			var baseTextProcessing = new BaseTextProcessing();
			var textProcessing = Substitute.For<ITextProcessing>();
			textProcessing.Process(inputText).Returns(expectedText);
			baseTextProcessing.AddTextProcessing(textProcessing);
			var actualText = baseTextProcessing.Process(inputText);
			Assert.AreEqual(expectedText, actualText);
		}

		[Test, Category("PreCommit")]
		public void AddTextProcessing_when_add_more_then_one_processing() {
			var inputText = "Test, input, text.";
			var expectedText = "Testinputtext";
			var baseTextProcessing = new BaseTextProcessing();
			var clearSpaceTextProcessing = Substitute.For<ITextProcessing>();
			var clearPunctuationTextProcessing = Substitute.For<ITextProcessing>();
			clearSpaceTextProcessing.Process(inputText).Returns("Testinputtext.");
			clearPunctuationTextProcessing.Process("Testinputtext.").Returns(expectedText);
			baseTextProcessing.AddTextProcessing(clearSpaceTextProcessing);
			baseTextProcessing.AddTextProcessing(clearPunctuationTextProcessing);
			var actualText = baseTextProcessing.Process(inputText);
			Assert.AreEqual(expectedText, actualText);
		}

		[Test, Category("PreCommit")]
		[TestCase(true, "Test, input, text.", "Testinputtext")]
		[TestCase(false, "Test, input, text.", "Test, input, text.")]
		public void AddTextProcessing_when_UseProcessing(bool useProcessing,
				string inputText, string expectedText) {
			var baseTextProcessing = new BaseTextProcessing {
				UseProcessing = useProcessing
			};
			var clearSpaceTextProcessing = Substitute.For<ITextProcessing>();
			var clearPunctuationTextProcessing = Substitute.For<ITextProcessing>();
			clearSpaceTextProcessing.Process(inputText).Returns("Testinputtext.");
			clearPunctuationTextProcessing.Process("Testinputtext.").Returns(expectedText);
			baseTextProcessing.AddTextProcessing(clearSpaceTextProcessing);
			baseTextProcessing.AddTextProcessing(clearPunctuationTextProcessing);
			var actualText = baseTextProcessing.Process(inputText);
			Assert.AreEqual(expectedText, actualText);
		}
	}
}
