namespace Kernel.Server
{
    public class ServerAuthorization
    {
        public readonly string OAuthToken;
        public readonly string Directory;

        public ServerAuthorization(string oAuthToken, string directory)
        {
            OAuthToken = oAuthToken;
            Directory = directory;
        }
    }
}