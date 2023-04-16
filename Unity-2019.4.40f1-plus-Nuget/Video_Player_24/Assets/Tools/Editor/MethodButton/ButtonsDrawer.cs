using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Tools.Editor.MethodButton
{
    internal sealed class ButtonsDrawer
    {
        private const BindingFlags Flags =
            BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        
        private readonly IEnumerable<Button> _buttons;

        public ButtonsDrawer(object target)
        {
            _buttons = GetButtons(target);
        }

        public void DrawButtons(IEnumerable<object> targets)
        {
            foreach (var button in _buttons)
                using (new EditorGUILayout.HorizontalScope())
                    button.Draw(targets);
        }

        private IEnumerable<Button> GetButtons(object target)
        {
            return target.GetType().GetMethods(Flags)
                .Where(method => method.GetCustomAttribute<ButtonAttribute>() != null)
                .Select(method => new Button(method));
        }
    }
}