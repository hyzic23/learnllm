namespace learnllm.Helper;

public class TextChunker
{
     public List<string> ChunkText(string text, int chunkSize = 1000, int overlap = 200)
     {
          var chunks = new List<string>();
          var start = 0;

          while (start < text.Length)
          {
               var length = Math.Min(chunkSize, text.Length - start);
               var chunk = text.Substring(start, length);
               chunks.Add(chunk);
               start += chunkSize - overlap;
          }
          return chunks;
     }
}