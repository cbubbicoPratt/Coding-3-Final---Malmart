using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InGameEvent))]
public class InGameEventUI : Editor
{
    private InGameEvent inspectedEvent;

    private SerializedProperty groceryList;
    private SerializedProperty LegislatureList;

    private SerializedProperty npcSprite;
    private SerializedProperty conditionalEvents;
    private SerializedProperty additionalEvents;


    private void OnEnable()
    {
        groceryList = serializedObject.FindProperty("groceryList");
        LegislatureList = serializedObject.FindProperty("legislatureStack");
        npcSprite = serializedObject.FindProperty("NPCImage");
        conditionalEvents = serializedObject.FindProperty("ToComplete");
        additionalEvents = serializedObject.FindProperty("OnComplete");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        inspectedEvent = target as InGameEvent;
        GUILayout.Label("Event Type:");
        inspectedEvent.eventType = (EventType)EditorGUILayout.EnumPopup(inspectedEvent.eventType);
        GUILayout.Space(20);
        switch (inspectedEvent.eventType)
        {
            case EventType.Wait:
                WaitMenu();
                break;
            case EventType.NPC:
                NPCMenu();
                break;
            case EventType.Conditional:
                ConditionalMenu();
                break;
        }


        EditorGUILayout.PropertyField(additionalEvents, true);
        serializedObject.ApplyModifiedProperties();
    }

    private void WaitMenu()
    {
        inspectedEvent.waitTime = EditorGUILayout.FloatField("Time to Wait:", inspectedEvent.waitTime);
    }

    private void NPCMenu()
    {
        GUILayout.Label("NPC Info:", EditorStyles.boldLabel);
        inspectedEvent.NPCName = EditorGUILayout.TextField("Name:", inspectedEvent.NPCName);
        GUILayout.Space(10);
        inspectedEvent.npcType = (NPCType)EditorGUILayout.EnumPopup("Type:", inspectedEvent.npcType);

        if (inspectedEvent.npcType == NPCType.Grocery)
        {

            EditorGUILayout.PropertyField(groceryList, true);
            EditorGUILayout.PropertyField(npcSprite, true);
            // Push any changes back to the target object

            GUILayout.Space(10);
            GUILayout.Label("Dialogue Data:", EditorStyles.boldLabel);
            inspectedEvent.NPCFirstNodeName = EditorGUILayout.TextField("NPC First Node Name: ", inspectedEvent.NPCFirstNodeName);
        }
        else if (inspectedEvent.npcType == NPCType.Phone)
        {
            inspectedEvent.phoneTarget = (PhoneTarget)EditorGUILayout.EnumPopup("Target: ", inspectedEvent.phoneTarget);

            GUILayout.Space(10);
            GUILayout.Label("Dialogue Data:", EditorStyles.boldLabel);
            inspectedEvent.NPCFirstNodeName = EditorGUILayout.TextField("NPC First Node Name: ", inspectedEvent.NPCFirstNodeName);
        }
        else if (inspectedEvent.npcType == NPCType.Legislature)
        {
            EditorGUILayout.PropertyField(LegislatureList, true);
        }
        else if(inspectedEvent.npcType == NPCType.Bomb)
        {
            inspectedEvent.BombPressCount = EditorGUILayout.IntField(inspectedEvent.BombPressCount);
        }



        GUILayout.Space(10);
        GUILayout.Label("Anger Data:", EditorStyles.boldLabel);
        inspectedEvent.timeTillAngry = EditorGUILayout.FloatField("Time Till Angry: ", inspectedEvent.timeTillAngry);
        inspectedEvent.angryDamagePerSecond = EditorGUILayout.FloatField("Anger DPS: ", inspectedEvent.angryDamagePerSecond);
    }

    private void ConditionalMenu()
    {
        EditorGUILayout.PropertyField(conditionalEvents, true);
    }
}
