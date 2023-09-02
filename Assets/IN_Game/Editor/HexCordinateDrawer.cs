using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(HexCordinates))]
public class HexCordinateDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        HexCordinates cordinate = new HexCordinates(
              property.FindPropertyRelative("x").intValue,
              property.FindPropertyRelative("z").intValue
            );

        position = EditorGUI.PrefixLabel(position, label);
        GUI.Label(position, cordinate.ToString());
    }
}
