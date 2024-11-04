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

        public static IEnumerable<Character> GenerateCharacters()
        {
            BookImportDto[] bookImportDtos = GenerateBookImportDtos();

            List<Character> generatedCharacters = new List<Character>();

            foreach (var bookDto in bookImportDtos)
            {
                if (bookDto.Characters.Count == 0)
                {
                    continue;
                }

                foreach (var characterName in bookDto.Characters)
                {
                    if (!generatedCharacters.Any(c => c.Name == characterName))
                    {
                        Character character = new Character()
                        {
                            Name = characterName,
                        };

                        generatedCharacters.Add(character);
                    }
                }                
            }

            return generatedCharacters;
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
