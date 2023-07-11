namespace Poq.Application.Helpers
{
    public static class WordsHighlighter
    {
        public static string HighlightDescription(string description, string highlight, string highlighter)
        {
            var words = highlight.Split(",");

            foreach (var word in words)
            {
                description = description.Replace(word, $"<{highlighter}>{word}</{highlighter}>");
            }

            return description;
        }
    }
}
