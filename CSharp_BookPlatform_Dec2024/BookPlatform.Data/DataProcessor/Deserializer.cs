using Newtonsoft.Json;
using BookPlatform.Services.Data.DataProcessor.ImportDtos;
using static BookPlatform.Common.ApplicationConstants;
using BookPlatform.Data.Models;
using BookPlatform.Data;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace BookPlatform.Services.Data.DataProcessor
{
    public static class Deserializer
    {    
        private static string GenerateFilePath()
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

        //public async Task<IEnumerable<Book>> GenerateBooks()
        //{
        //    BookImportDto[] bookImportDtos = GenerateBookImportDtos();

        //    List<Book> generatedBooks = new List<Book>();

        //    foreach (var bookDto in bookImportDtos)
        //    {
        //        if (!generatedBooks.Any(b => b.Title == bookDto.Title && b.Author.FullName == bookDto.Author))
        //        {
        //            Author? author = await context.Authors
        //                .FirstOrDefaultAsync(a => a.FullName == bookDto.Author);

        //            if (author == null)
        //            {
        //                continue;
        //            }

        //            Genre? genre = await this.context.Genres
        //                .FirstOrDefaultAsync(g => g.Name == bookDto.Genre);

        //            if (genre == null)
        //            {
        //                continue;
        //            }

        //            Book book = new Book()
        //            {
        //                Title = bookDto.Title,
        //                PublicationYear = bookDto.Year,
        //                Author = author,
        //                Genre = genre,
        //                Description = bookDto.Description,
        //                ImageUrl = bookDto.ImageLink,
        //            };

        //            generatedBooks.Add(book);
        //        }
        //    }

        //    return generatedBooks;
        //}

        public static IEnumerable<Character> GenerateCharacters()
        {
            throw new NotImplementedException();
        }

        public static IEnumerable<Genre> GenerateGenres()
        {
            BookImportDto[] bookImportDtos = GenerateBookImportDtos();

            List<Genre> generatedGenres = new List<Genre>();

            foreach (var bookDto in bookImportDtos)
            {
                if (!generatedGenres.Any(g => g.Name == bookDto.Genre))
                {
                    Genre genre = new Genre()
                    {
                        Name = bookDto.Genre,
                    };

                    generatedGenres.Add(genre);
                }
            }

            return generatedGenres;
        }
    }
}
