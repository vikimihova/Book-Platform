using BookPlatform.Data.Models;
using BookPlatform.Services.Data.DataProcessor.ImportDtos;

namespace BookPlatform.Services.Data.DataProcessor
{
    public class Seeder
    {
        public static IEnumerable<Author> GenerateAuthors()
        {         
            BookImportDto[] bookImportDtos = Deserializer.GenerateBookImportDtos();

            List<Author> authorsToAdd = new List<Author>();

            foreach (var bookDto in bookImportDtos)
            {
                if (!authorsToAdd.Any(a => a.FullName == bookDto.Author))
                {
                    Author author = new Author()
                    {
                        FullName = bookDto.Author,
                    };

                    authorsToAdd.Add(author);
                }
            }

            return authorsToAdd;
        }
    }
}
