/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/27 14:40:25
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
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

        public void Draw(string label)
        {
            Value = EditorGUILayout.Toggle(label, property.boolValue);
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