/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/27 14:41:04
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
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

        public void Draw(string label)
        {
            Value = EditorGUILayout.LongField(label, property.longValue);
        }

        public void Draw(Rect rect)
        {
            Value = EditorGUI.LongField(rect, property.longValue);
        }
    }
}