using UnityEditor;
using Object = UnityEngine.Object;

namespace Tools.Editor.MethodButton
{
    [CustomEditor(typeof(Object), true), CanEditMultipleObjects]
    internal sealed class ButtonsInspector : UnityEditor.Editor
    {
        private ButtonsDrawer _buttonsDrawer;

        private void OnEnable()
        {
            _buttonsDrawer = new ButtonsDrawer(target);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            _buttonsDrawer.DrawButtons(targets);
        }
    }
}
