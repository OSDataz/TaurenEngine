/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/27 14:41:37
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
    public class PropertyVector4 : EditorProperty
    {
        public override void Clear() => Value = Vector4.zero;

        public Vector4 Value
        {
            get => property.vector4Value;
            set
            {
                if (property.vector4Value == value)
                    return;

                property.vector4Value = value;
                UpdateModified();
            }
        }

        public void Draw(string label)
        {
            Value = EditorGUILayout.Vector4Field(label, property.vector4Value);
        }

        public void Draw(Rect rect, string label)
        {
            Value = EditorGUI.Vector4Field(rect, label, property.vector4Value);
        }
    }
}