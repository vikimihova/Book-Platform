using Newtonsoft.Json;
using BookPlatform.Services.Data.DataProcessor.ImportDtos;
using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Services.Data.DataProcessor
{
    public class Deserializer
    {
        public static BookImportDto[] GenerateBookImportDtos()
        {
            string filePath = GenerateFolderPath() + @"/100books.json";
            return JsonConvert.DeserializeObject<BookImportDto[]>(File.ReadAllText(filePath));
        }

        public static string GenerateFolderPath()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var directoryName = Path.GetFileName(currentDirectory);
            string folderPath = directoryName + DataSetsPath;

            return folderPath;
        }
    }
}
