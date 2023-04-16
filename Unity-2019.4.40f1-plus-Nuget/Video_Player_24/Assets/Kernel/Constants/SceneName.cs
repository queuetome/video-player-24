namespace Kernel.Constants
{
    public static class SceneName
    {
        public static readonly string Bootstrap = Format(nameof(Bootstrap));
        public static readonly string Kernel = Format(nameof(Kernel));
        
        private static string Format(string sceneName) => 
            $"Assets/{sceneName}/Scene/{sceneName}.unity";
    }
}