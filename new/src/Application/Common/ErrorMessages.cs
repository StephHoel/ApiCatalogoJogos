namespace Application.Common;

public static class ErrorMessages
{
    public static class Game
    {
        public const string NotFound = "Game not found.";
        public const string InvalidPrice = "Price must be greater than zero.";
        public const string TitleRequired = "Title is required.";
    }

    public static class Developer
    {
        public const string NotFound = "Developer not found.";
        public const string NameRequired = "Developer name is required.";
    }

    public static class General
    {
        public const string UnexpectedError = "An unexpected error occurred.";
    }
}