using Newtonsoft.Json;
using BookPlatform.Services.Data.DataProcessor.ImportDtos;
using static BookPlatform.Common.ApplicationConstants;
using BookPlatform.Data.Models;

namespace BookPlatform.Services.Data.DataProcessor
{
    public static class Deserializer
    {     
        public static string GenerateFilePath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryName = Path.GetFileName(currentDirectory);
            string filePath = directoryName + DataSetsPath + DataSetsFile;


            return filePath;
        }

        public static BookImportDto[] GenerateBookImportDtos()
        {
            string filePath = GenerateFilePath();
            return JsonConvert.DeserializeObject<BookImportDto[]>(File.ReadAllText(filePath));
        }

        public static IEnumerable<Author> GenerateAuthors()
        {
            BookImportDto[] bookImportDtos = GenerateBookImportDtos();

            List<Author> generatedAuthors = new List<Author>();

            foreach (var bookDto in bookImportDtos)
            {
                if (!generatedAuthors.Any(a => a.FullName == bookDto.Author))
                {
                    Author author = new Author()
                    {
                        FullName = bookDto.Author,
                    };

                    generatedAuthors.Add(author);
                }
            }

            return generatedAuthors;
        }

        public static IEnumerable<Book> GenerateBooks()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Character> GenerateCharacters()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Genre> GenerateGenres()
        {
            throw new NotImplementedException();
        }
    }
}
