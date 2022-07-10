/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v3.0
 *│　Time    ：2021/9/27 14:41:25
 *└────────────────────────┘*/

using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor
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

        public void Draw(string label)
        {
            Value = EditorGUILayout.Vector3Field(label, property.vector3Value);
        }

        public void Draw(Rect rect, string label)
        {
            Value = EditorGUI.Vector3Field(rect, label, property.vector3Value);
        }
    }
}