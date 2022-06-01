/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:40:45
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
    public class PropertyFloat : EditorProperty
    {
        public override void Clear() => Value = 0.0f;

        public float Value
        {
            get => property.floatValue;
            set
            {
                if (property.floatValue == value)
                    return;

                property.floatValue = value;
                UpdateModified();
            }
        }

        public void Draw(string label)
        {
            Value = EditorGUILayout.FloatField(label, property.floatValue);
        }

        public void Draw(Rect rect)
        {
            Value = EditorGUI.FloatField(rect, property.floatValue);
        }
    }
}