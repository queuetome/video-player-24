using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor.MethodButton
{
    internal sealed class Button
    {
        private readonly string _displayName;
        private readonly MethodInfo _method;

        public Button(MethodInfo method)
        {
            _displayName = ObjectNames.NicifyVariableName(method.Name);
            _method = method;
        }

        public void Draw(IEnumerable<object> targets)
        {
            if (GUILayout.Button(_displayName))
            {
                if (Application.isPlaying)
                    foreach (object target in targets)
                        _method.Invoke(target, null);
                else
                    Debug.LogWarning("Must be in Play mode");
            }
        }
    }
}