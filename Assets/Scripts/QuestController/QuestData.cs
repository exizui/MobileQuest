using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(menuName = "Quest/QuestData")]
public class QuestData : ScriptableObject 
{
    public string questID;
    public List<LocationID> allowedRooms;
    [TextArea] public string description;

    public QuestStepData[] steps;

    public List <BaseReward> rewards;
}
[System.Serializable]
public class QuestProgressData
{
    public string questID;
    public int currentStep;
}

[System.Serializable]
public class QuestStepData
{
    public QuestStepType stepType;

    public ItemData item;
    public int amount;

    public LocationID locationID;

    public string customStepID;

    [ResizableTextArea]
    public string description;

    public string triggerID;
}
[System.Serializable]
public class QuestSaveData
{
    public List<QuestProgressData> activeQuests = new List<QuestProgressData>();
    public List<string> completedQuests = new List<string>();
}

#region HideInspectorManager
[CustomPropertyDrawer(typeof(QuestStepData))]
public class QuestStepDataDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Починаємо процес малювання властивості
        EditorGUI.BeginProperty(position, label, property);

        // Знаходимо посилання на конкретні поля всередині класу QuestStepData
        SerializedProperty stepType = property.FindPropertyRelative("stepType");
        SerializedProperty description = property.FindPropertyRelative("description");

        // Створюємо прямокутник (Rect) для першого поля (вибір типу кроку)
        // Встановлюємо висоту в один стандартний рядок
        Rect rect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

        // Малюємо поле вибору типу (воно відображається завжди)
        EditorGUI.PropertyField(rect, stepType);

        // Зміщуємо позицію малювання вниз для наступного поля (+2 пікселі відступу)
        rect.y += EditorGUIUtility.singleLineHeight + 2;

        // Отримуємо поточне вибране значення Enum, щоб знати, які поля показувати
        QuestStepType type = (QuestStepType)stepType.enumValueIndex;

        // Логіка відображення полів залежно від обраного типу кроку
        switch (type)
        {
            case QuestStepType.CollectItem:
                DrawField(ref rect, property.FindPropertyRelative("item"));
                DrawField(ref rect, property.FindPropertyRelative("amount"));
                break;

            case QuestStepType.GoToLocation:
                DrawField(ref rect, property.FindPropertyRelative("locationID"));
                break;

            case QuestStepType.Trigger:
                DrawField(ref rect, property.FindPropertyRelative("triggerID"));
                break;

            case QuestStepType.Custom:
                DrawField(ref rect, property.FindPropertyRelative("customStepID"));
                break;
        }

        // В кінці малюємо поле опису (воно є у кожного типу квесту)
        // Оскільки це TextArea, воно автоматично займе більше місця, якщо Rect дозволяє
        EditorGUI.PropertyField(rect, description);

        // Завершуємо процес малювання
        EditorGUI.EndProperty();
    }

    /// <summary>
    /// Допоміжний метод: малює поле і автоматично зсуває координату 'y' вниз
    /// </summary>
    private void DrawField(ref Rect rect, SerializedProperty prop)
    {
        EditorGUI.PropertyField(rect, prop);
        rect.y += EditorGUIUtility.singleLineHeight + 2;
    }

    /// <summary>
    /// Важливо: Unity має заздалегідь знати повну висоту елемента, 
    /// щоб вони не накладалися один на одного в списку (List/Array)
    /// </summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Початкова кількість рядків: 1 (для stepType) + приблизно 3 (для TextArea опису)
        float totalHeight = EditorGUIUtility.singleLineHeight + 2;

        SerializedProperty stepType = property.FindPropertyRelative("stepType");
        QuestStepType type = (QuestStepType)stepType.enumValueIndex;

        // Додаємо висоту додаткових полів залежно від типу
        switch (type)
        {
            case QuestStepType.CollectItem:
                totalHeight += (EditorGUIUtility.singleLineHeight + 2) * 2; // item + amount
                break;
            case QuestStepType.GoToLocation:
            case QuestStepType.Trigger:
            case QuestStepType.Custom:
                totalHeight += (EditorGUIUtility.singleLineHeight + 2); // одне додаткове поле
                break;
        }

        // Додаємо висоту для TextArea (опис). Стандартно TextArea займає біля 3-х рядків
        totalHeight += EditorGUI.GetPropertyHeight(property.FindPropertyRelative("description"));

        return totalHeight;
    }
}
#endregion