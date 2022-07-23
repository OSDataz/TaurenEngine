/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:40:39
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEditor
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

        public void Draw(string label, params GUILayoutOption[] options)
        {
            Value = EditorGUILayout.IntField(label, property.intValue, options);
        }

        public void Draw(Rect rect)
        {
            Value = EditorGUI.IntField(rect, property.intValue);
        }
    }
}