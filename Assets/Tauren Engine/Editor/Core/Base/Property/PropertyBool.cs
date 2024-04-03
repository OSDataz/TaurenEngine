/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:40:25
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace Tauren.Core.Editor
{
    public class PropertyBool : EditorProperty
    {
        public override void Clear() => Value = false;

        public bool Value
        {
            get => property.boolValue;
            set
            {
                if (property.boolValue == value)
                    return;

                property.boolValue = value;
                UpdateModified();
            }
        }

        public void Draw(params GUILayoutOption[] options)
		{
            Value = EditorGUILayout.Toggle(property.boolValue, options);
        }

        public void Draw(string label, params GUILayoutOption[] options)
        {
            Value = EditorGUILayout.Toggle(label, property.boolValue, options);
        }

        public void Draw(Rect rect)
        {
            Value = EditorGUI.Toggle(rect, property.boolValue);
        }

        public void DrawFoldout(string label)
        {
            Value = EditorGUILayout.Foldout(property.boolValue, label);
        }

        public void DrawFoldout(Rect rect, string label)
        {
            Value = EditorGUI.Foldout(rect, property.boolValue, label);
        }
    }
}