namespace WebAPI.Tests
{
	using Newtonsoft.Json;
	using NUnit.Framework;
	using System.Collections.Generic;
	using WebAPI.Data;
	using WebAPI.TextProcessing;

	[TestFixture]
	class WordsLemmatizer_Tests
	{
		[Test, Category("PreCommit")]
		[TestCase("Контрольный список управления процессом разработки")]
		public void Process_clear_all_punctuation_marks(string inputText) {
			var wordsLemmatizer = new WordsLemmatizer();
			var actualText = wordsLemmatizer.Process(inputText);
			var expetedList = new List<WordBox>(){
				new WordBox(){ W = "контрольный", L = "контрольный" },
				new WordBox(){ W = "список", L = "список" },
				new WordBox(){ W = "управления", L = "управление" },
				new WordBox(){ W = "процессом", L = "процесс" },
				new WordBox(){ W = "разработки", L = "разработка" },
			};
			Assert.AreEqual(JsonConvert.SerializeObject(expetedList), actualText);
		}
	}
}
