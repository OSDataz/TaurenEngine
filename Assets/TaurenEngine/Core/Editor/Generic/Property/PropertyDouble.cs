/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:40:58
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEditor.Core
{
    public class PropertyDouble : EditorProperty
    {
        public override void Clear() => Value = 0.0;

        public double Value
        {
            get => property.doubleValue;
            set
            {
                if (property.doubleValue == value)
                    return;

                property.doubleValue = value;
                UpdateModified();
            }
        }

        public void Draw(string label, params GUILayoutOption[] options)
        {
            Value = EditorGUILayout.DoubleField(label, property.doubleValue, options);
        }

        public void Draw(Rect rect)
        {
            Value = EditorGUI.DoubleField(rect, property.doubleValue);
        }
    }
}