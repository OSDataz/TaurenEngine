/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:40:58
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
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

        public void Draw(string label)
        {
            Value = EditorGUILayout.DoubleField(label, property.doubleValue);
        }

        public void Draw(Rect rect)
        {
            Value = EditorGUI.DoubleField(rect, property.doubleValue);
        }
    }
}