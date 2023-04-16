using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Tools.Editor
{
    [InitializeOnLoad]
    public class TaggedHierarchy : MonoBehaviour
    {
        static TaggedHierarchy()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
            
            if (gameObject == null || gameObject.CompareTag("Untagged"))
                return;
            
            Color fontColor = Color.gray;
            
            if (Selection.instanceIDs.Contains(instanceID))
                fontColor = Color.white;
            
            Rect offsetRect = new Rect(selectionRect.position, selectionRect.size);
            EditorGUI.LabelField(offsetRect, gameObject.tag, new GUIStyle()
            {
                normal = new GUIStyleState() {textColor = fontColor},
                fontStyle = FontStyle.Italic,
                alignment = TextAnchor.MiddleRight,
                stretchWidth = true,
                
            });
        
        }
    }
}