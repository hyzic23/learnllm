namespace learnllm.Helper;

public class TextChunker
{
     /// <summary>
     /// Takes a long piece of text and splits it into smaller pieces (chunks)
     /// like cutting a long article into paragraphs of ~1000 characters each
     /// with a little bit of overlap between them so nothing gets lost at the boundaries
     /// </summary>
     /// <param name="text"></param> the input text you want to split.
     /// <param name="chunkSize"></param> each chunk should be 1000 characters long by default.
     /// <param name="overlap"></param> each new chunk overlaps the previous one by 200 characters.
     /// <returns></returns>
     public List<string> ChunkText(string text, int chunkSize = 1000, int overlap = 200)
     {
          var chunks = new List<string>();
          
          if(string.IsNullOrWhiteSpace(text))
               return chunks;
          
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