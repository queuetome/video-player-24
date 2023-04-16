using System.IO;
using System.Linq;
using Kernel.Server;
using UnityEngine;
using static System.IO.Path;
using static Kernel.Constants.ApplicationPersistent;

namespace Kernel.Configs
{
    [ExecuteAlways]
    [CreateAssetMenu]
    public sealed class ServerConfig : ScriptableObject
    {
        private static readonly string FilePath = Combine(DataFolder, nameof(ServerConfig) + ".txt");
        
        [Space(10), Header("Server Authorization")]
        [SerializeField] private string _OAuthToken;
        [SerializeField] private string _rootDirectory;
        
        public ServerAuthorization Authorization => GetDataFromFile();

        private void OnValidate()
        {
            if (Directory.Exists(DataFolder))
                WriteDataToFile();
        }

        private void WriteDataToFile()
        {
            File.WriteAllText(FilePath,
                $"token = {_OAuthToken}\n" +
                $"directory = {_rootDirectory}");
        }

        private ServerAuthorization GetDataFromFile()
        {
            if (File.Exists(FilePath) == false)
                WriteDataToFile();

            string token = "";
            string directory = "";
            
            foreach (string line in File.ReadAllLines(FilePath))
            {
                if (line.Contains("=") == false)
                    continue;
                
                switch (line.Split('=').First().Replace(" ", "").ToLower())
                {
                    case "token":
                        token = line.Split('=').Last().Replace(" ", "");
                        break;
                    case "directory":
                        directory = line.Split('=').Last().Replace(" ", "");
                        break;
                }
            }
            
            return new ServerAuthorization(token, directory);
        }
    }
}