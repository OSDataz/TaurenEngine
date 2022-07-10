/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:41:04
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEditor.Core
{
    public class PropertyLong : EditorProperty
    {
        public override void Clear() => Value = 0;

        public long Value
        {
            get => property.longValue;
            set
            {
                if (property.longValue == value)
                    return;

                property.longValue = value;
                UpdateModified();
            }
        }

        public void Draw(string label, params GUILayoutOption[] options)
        {
            Value = EditorGUILayout.LongField(label, property.longValue, options);
        }

        public void Draw(Rect rect)
        {
            Value = EditorGUI.LongField(rect, property.longValue);
        }
    }
}