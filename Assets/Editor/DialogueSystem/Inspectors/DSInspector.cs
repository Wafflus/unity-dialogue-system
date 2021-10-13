using System.Collections.Generic;
using UnityEditor;

namespace DS.Inspectors
{
    using Utilities;
    using ScriptableObjects;

    [CustomEditor(typeof(DSDialogue))]
    public class DSInspector : Editor
    {
        /* Dialogue Scriptable Objects */
        private SerializedProperty dialogueContainerProperty;
        private SerializedProperty dialogueGroupProperty;
        private SerializedProperty dialogueProperty;

        /* Filters */
        private SerializedProperty groupedDialoguesProperty;
        private SerializedProperty startingDialoguesOnlyProperty;

        /* Indexes */
        private SerializedProperty selectedDialogueGroupIndexProperty;
        private SerializedProperty selectedDialogueIndexProperty;

        private void OnEnable()
        {
            dialogueContainerProperty = serializedObject.FindProperty("dialogueContainer");
            dialogueGroupProperty = serializedObject.FindProperty("dialogueGroup");
            dialogueProperty = serializedObject.FindProperty("dialogue");

            groupedDialoguesProperty = serializedObject.FindProperty("groupedDialogues");
            startingDialoguesOnlyProperty = serializedObject.FindProperty("startingDialoguesOnly");

            selectedDialogueGroupIndexProperty = serializedObject.FindProperty("selectedDialogueGroupIndex");
            selectedDialogueIndexProperty = serializedObject.FindProperty("selectedDialogueIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawDialogueContainerArea();

            DSDialogueContainerSO dialogueContainer = (DSDialogueContainerSO) dialogueContainerProperty.objectReferenceValue;

            if (dialogueContainer == null)
            {
                StopDrawing("Select a Dialogue Container to see the rest of the Inspector.");

                return;
            }

            DrawFiltersArea();

            if (groupedDialoguesProperty.boolValue)
            {
                List<string> dialogueGroupNames = dialogueContainer.GetDialogueGroupNames();

                if (dialogueGroupNames.Count == 0)
                {
                    StopDrawing("There are no Dialogue Groups in this Dialogue Container.");

                    return;
                }

                DrawDialogueGroupArea(dialogueContainer, dialogueGroupNames);
            }

            DrawDialogueArea();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawDialogueContainerArea()
        {
            DSInspectorUtility.DrawHeader("Dialogue Container");

            dialogueContainerProperty.DrawPropertyField();

            DSInspectorUtility.DrawSpace();
        }

        private void DrawFiltersArea()
        {
            DSInspectorUtility.DrawHeader("Filters");

            groupedDialoguesProperty.DrawPropertyField();
            startingDialoguesOnlyProperty.DrawPropertyField();

            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueGroupArea(DSDialogueContainerSO dialogueContainer, List<string> dialogueGroupNames)
        {
            DSInspectorUtility.DrawHeader("Dialogue Group");

            selectedDialogueGroupIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue Group", selectedDialogueGroupIndexProperty, dialogueGroupNames.ToArray());

            string selectedDialogueGroupName = dialogueGroupNames[selectedDialogueGroupIndexProperty.intValue];

            DSDialogueGroupSO selectedDialogueGroup = DSIOUtility.LoadAsset<DSDialogueGroupSO>($"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}/Groups/{selectedDialogueGroupName}", selectedDialogueGroupName);

            dialogueGroupProperty.objectReferenceValue = selectedDialogueGroup;

            dialogueGroupProperty.DrawPropertyField();

            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueArea()
        {
            DSInspectorUtility.DrawHeader("Dialogue");

            selectedDialogueIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue", selectedDialogueIndexProperty, new string[] { });

            dialogueProperty.DrawPropertyField();
        }

        private void StopDrawing(string reason)
        {
            DSInspectorUtility.DrawHelpBox(reason);

            serializedObject.ApplyModifiedProperties();
        }
    }
}