using static BookPlatform.Common.ApplicationConstants;

namespace BookPlatform.Common
{
    public static class ModelValidationErrorMessages
    {
        public static class DateTimeFormats
        {
            public const string WrongDateViewFormat = $"Date must be in format {DateViewFormat}";
            public const string DateInFuture = "Date cannot be in the future.";
        }
    }
}
