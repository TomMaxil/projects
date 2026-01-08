namespace Eproject.Helpers
{
    public static class Passwordhash
    {
        public static string Hash(string password)
        {
            return Convert.ToBase64String(
                System.Security.Cryptography.SHA256.Create()
                .ComputeHash(System.Text.Encoding.UTF8.GetBytes(password))
            );
        }
    }
}
