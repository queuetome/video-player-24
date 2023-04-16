using Kernel;
using Kernel.Constants;
using UnityEditor;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayModeStateChange;
using static UnityEditor.SceneManagement.EditorSceneManager;

namespace Tools.Editor
{
    [InitializeOnLoad]
    internal static class PlayModeBootstrap
    {
        public static string BufferScene
        {
            get => EditorPrefs.GetString(nameof(BufferScene));
            set => EditorPrefs.SetString(nameof(BufferScene), value);
        }
        
        static PlayModeBootstrap()
        {
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }
        
        private static void OnPlayModeChanged(PlayModeStateChange playModeStateChange)
        {
            switch (playModeStateChange)
            {
                case ExitingEditMode when SaveCurrentModifiedScenesIfUserWantsTo():
                    BufferScene = SceneManager.GetActiveScene().path;
                    OpenScene(SceneName.Bootstrap);
                    break;
                case EnteredEditMode when BufferScene != string.Empty:
                    OpenScene(BufferScene);
                    BufferScene = "";
                    break;
            }
        }
    }
}