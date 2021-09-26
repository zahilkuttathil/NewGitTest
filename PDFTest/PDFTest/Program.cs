using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;

namespace PDFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            // Create a reader from the file bytes.
            var reader = new PdfReader(File.ReadAllBytes(@"C:\Code\NewGitTest\PDFTest\NTC Song Book To 2016.pdf"));

            for (var pageNum = 1; pageNum <= reader.NumberOfPages; pageNum++)
            {
                // Get the page content and tokenize it.
                var contentBytes = reader.GetPageContent(pageNum);
                var tokenizer = new PrTokeniser(new RandomAccessFileOrArray(contentBytes));

                var stringsList = new List<string>();
                while (tokenizer.NextToken())
                {
                    if (tokenizer.TokenType == PrTokeniser.TK_STRING)
                    {
                        // Extract string tokens.
                        stringsList.Add(tokenizer.StringValue);
                    }
                }

                // Print the set of string tokens, one on each line.
                Console.WriteLine(string.Join("\r\n", stringsList));
                var oFile = File.CreateText($@"C:\Code\NewGitTest\PDFTest\{pageNum}.txt");
                oFile.Write(string.Join("", stringsList));
                oFile.Close();
                oFile.Dispose();
            }

            reader.Close();
        }
    }
}
