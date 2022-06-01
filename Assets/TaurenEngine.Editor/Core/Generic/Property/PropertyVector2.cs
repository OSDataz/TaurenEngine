/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:41:16
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
    public class PropertyVector2 : EditorProperty
    {
        public override void Clear() => Value = Vector2.zero;

        public Vector2 Value
        {
            get => property.vector2Value;
            set
            {
                if (property.vector2Value == value)
                    return;

                property.vector2Value = value;
                UpdateModified();
            }
        }

        public void Draw(string label)
        {
            Value = EditorGUILayout.Vector2Field(label, property.vector2Value);
        }

        public void Draw(Rect rect, string label)
        {
            Value = EditorGUI.Vector2Field(rect, label, property.vector2Value);
        }
    }
}