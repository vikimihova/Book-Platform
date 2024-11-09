namespace BookPlatform.Common
{
    public static class EntityValidationConstants
    {
        public static class BookValidationConstants
        {
            public const int TitleMinLength = 2;
            public const int TitleMaxLength = 200;
            public const int MaxImageUrlLength = 2083;
            public const int DescriptionMinLength = 20;
        }

        public static class AuthorValidationConstants
        {
            public const int AuthorFirstNameMinLength = 2;
            public const int AuthorFirstNameMaxLength = 50;
            public const int AuthorLastNameMinLength = 2;
            public const int AuthorLastNameMaxLength = 50;
        }

        public static class GenreValidationConstants
        {
            public const int GenreNameMinLength = 5;
            public const int GenreNameMaxLength = 20;
        }

        public static class CharacterValidationConstants
        {
            public const int CharacterNameMinLength = 2;
            public const int CharacterNameMaxLength = 50;
        }

        public static class QuoteValidationConstants
        {
            public const int QuoteContentMinLength = 10;
            public const int QuoteContentMaxLength = 2500;            
        }

        public static class ReviewValidationConstants
        {
            public const int ReviewContentMinLength = 3;
            public const int ReviewContentMaxLength = 5000;
        }
    }
}
