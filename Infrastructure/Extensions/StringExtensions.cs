namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsJsonStringEmpty(this string jsonString)
        {
            return jsonString is null or "[]";
        }
    }
}