namespace WebAPI.Tests
{
	using Newtonsoft.Json;
	using NUnit.Framework;
	using System.Collections.Generic;
	using WebAPI.Data;
	using WebAPI.TextProcessing;

	[TestFixture]
	class TextLemmatizer_Tests
	{
		[Test, Category("PreCommit")]
		[TestCase("Контрольный список управления процессом разработки")]
		public void Process_clear_all_punctuation_marks(string inputText) {
			var wordsLemmatizer = new TextLemmatizer();
			var actualText = wordsLemmatizer.Process(inputText);
			Assert.AreEqual("контрольный список управление процесс разработка ", actualText);
		}
	}
}
