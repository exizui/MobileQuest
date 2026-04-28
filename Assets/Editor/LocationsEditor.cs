using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Locations))]
public class LocationsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Locations loc = (Locations)target;

        // Показываем ID
        loc.id = (LocationID)EditorGUILayout.EnumPopup("ID", loc.id);

        // Показываем тип состояния
        loc.stateType = (StateLocation)EditorGUILayout.EnumPopup("State Type", loc.stateType);

        EditorGUILayout.Space();

        // Если НЕ аудитория — показываем навигацию
        if (loc.stateType != StateLocation.Audience)
        {
            EditorGUILayout.LabelField("Navigation", EditorStyles.boldLabel);

            loc.next = (LocationID)EditorGUILayout.EnumPopup("Next", loc.next);
            loc.prev = (LocationID)EditorGUILayout.EnumPopup("Prev", loc.prev);
        }
        else
        {
            EditorGUILayout.HelpBox("Navigation disabled for Audience", MessageType.Info);
        }

        // применяем изменения
        if (GUI.changed)
        {
            EditorUtility.SetDirty(loc);
        }
    }
}
