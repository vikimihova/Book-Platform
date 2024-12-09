using Newtonsoft.Json;
using BookPlatform.Data.DataProcessor.ImportDtos;

using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Data.DataProcessor
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

        public static AuthorImportDto[] GenerateAuthorImportDtos()
        {
            string filePath = GenerateFilePath();
            return JsonConvert.DeserializeObject<AuthorImportDto[]>(File.ReadAllText(filePath))!;
        }

        public static GenreImportDto[] GenerateGenreImportDtos()
        {
            string filePath = GenerateFilePath();
            return JsonConvert.DeserializeObject<GenreImportDto[]>(File.ReadAllText(filePath))!;
        }

        public static BookImportDto[] GenerateBookImportDtos()
        {
            string filePath = GenerateFilePath();
            return JsonConvert.DeserializeObject<BookImportDto[]>(File.ReadAllText(filePath))!;
        }

        public static CharactersImportDto[] GenerateCharactersImportDtos()
        {
            string filePath = GenerateFilePath();
            return JsonConvert.DeserializeObject<CharactersImportDto[]>(File.ReadAllText(filePath))!;
        }    
        
        public static QuotesImportDto[] GenerateQuotesImportDtos()
        {
            string filePath = GenerateFilePath();
            return JsonConvert.DeserializeObject<QuotesImportDto[]>(File.ReadAllText(filePath))!;
        }
    }
}
