/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/27 14:40:39
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
{
    public class PropertyInt : EditorProperty
    {
        public override void Clear() => Value = 0;

        public int Value
        {
            get => property.intValue;
            set
            {
                if (property.intValue == value)
                    return;

                property.intValue = value;
                UpdateModified();
            }
        }

        public void Draw(string label)
        {
            Value = EditorGUILayout.IntField(label, property.intValue);
        }

        public void Draw(Rect rect)
        {
            Value = EditorGUI.IntField(rect, property.intValue);
        }
    }
}