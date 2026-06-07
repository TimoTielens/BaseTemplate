namespace AppointMe.Shared.Utilities;

public static class SearchExtensions
{
    extension(string? search)
    {
        public string[] Tokenize()
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return [];
            }

            return
            [
                .. search
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Select(token => token.ToLowerInvariant())
                    .Distinct()
                    .Take(30)
            ];
        }
    }
}
