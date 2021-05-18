namespace ByPetravetsKittens.Services
{
    static class Utils
    {
        public static int ParseInt(string intString)
        {
            int parsedResult = 0;
            int.TryParse(intString, out parsedResult);
            return parsedResult;
        }
    }
}
