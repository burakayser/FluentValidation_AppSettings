namespace FluentValidation_AppSettings.Infrastructure.Options
{
    public class ExampleOptions
    {
        public static string SectionName = "Example";

        public string LogLevel { get; init; }

        public int Retries { get; init; }
    }
}
