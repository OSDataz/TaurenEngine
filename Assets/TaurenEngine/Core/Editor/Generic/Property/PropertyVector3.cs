/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.3.0
 *│　Time    ：2021/9/27 14:41:25
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEditor.Core
{
    public class PropertyVector3 : EditorProperty
    {
        public override void Clear() => Value = Vector3.zero;

        public Vector3 Value 
        {
            get => property.vector3Value;
            set
            {
                if (property.vector3Value == value)
                    return;

                property.vector3Value = value;
                UpdateModified();
            }
        }

        public void Draw(string label, params GUILayoutOption[] options)
        {
            Value = EditorGUILayout.Vector3Field(label, property.vector3Value, options);
        }

        public void Draw(Rect rect, string label)
        {
            Value = EditorGUI.Vector3Field(rect, label, property.vector3Value);
        }
    }
}