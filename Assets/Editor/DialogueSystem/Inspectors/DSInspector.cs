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

            DSDialogueContainerSO oldDialogueContainer = (DSDialogueContainerSO) dialogueContainerProperty.objectReferenceValue;

            DrawDialogueContainerArea();

            DSDialogueContainerSO currentDialogueContainer = (DSDialogueContainerSO) dialogueContainerProperty.objectReferenceValue;

            if (currentDialogueContainer == null)
            {
                StopDrawing("Select a Dialogue Container to see the rest of the Inspector.");

                return;
            }

            ResetIndexesOnDialogueContainerUpdate(oldDialogueContainer, currentDialogueContainer);

            DrawFiltersArea();

            List<string> dialogueNames;

            string dialogueFolderPath = $"Assets/DialogueSystem/Dialogues/{currentDialogueContainer.FileName}";

            string dialogueInfoMessage;

            if (groupedDialoguesProperty.boolValue)
            {
                List<string> dialogueGroupNames = currentDialogueContainer.GetDialogueGroupNames();

                if (dialogueGroupNames.Count == 0)
                {
                    StopDrawing("There are no Dialogue Groups in this Dialogue Container.");

                    return;
                }

                DrawDialogueGroupArea(currentDialogueContainer, dialogueGroupNames);

                DSDialogueGroupSO dialogueGroup = (DSDialogueGroupSO) dialogueGroupProperty.objectReferenceValue;

                dialogueNames = currentDialogueContainer.GetGroupedDialogueNames(dialogueGroup);

                dialogueFolderPath += $"/Groups/{dialogueGroup.GroupName}/Dialogues";

                dialogueInfoMessage = "There are no Dialogues in this Dialogue Group.";
            }
            else
            {
                dialogueNames = currentDialogueContainer.GetUngroupedDialogueNames();

                dialogueFolderPath += "/Global/Dialogues";

                dialogueInfoMessage = "There are no Ungrouped Dialogues in this Dialogue Container.";
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

            DSDialogueGroupSO oldDialogueGroup = (DSDialogueGroupSO) dialogueGroupProperty.objectReferenceValue;
            DSDialogueGroupSO selectedDialogueGroup = DSIOUtility.LoadAsset<DSDialogueGroupSO>($"Assets/DialogueSystem/Dialogues/{dialogueContainer.FileName}/Groups/{selectedDialogueGroupName}", selectedDialogueGroupName);
            
            ResetIndexOnDialogueGroupUpdate(oldDialogueGroup, selectedDialogueGroup);

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

        private void ResetIndexesOnDialogueContainerUpdate(DSDialogueContainerSO oldDialogueContainer, DSDialogueContainerSO currentDialogueContainer)
        {
            if (oldDialogueContainer != currentDialogueContainer)
            {
                selectedDialogueGroupIndexProperty.intValue = 0;
                selectedDialogueIndexProperty.intValue = 0;
            }
        }

        private void ResetIndexOnDialogueGroupUpdate(DSDialogueGroupSO oldDialogueGroup, DSDialogueGroupSO selectedDialogueGroup)
        {
            if (oldDialogueGroup != selectedDialogueGroup)
            {
                selectedDialogueIndexProperty.intValue = 0;
            }
        }
    }
}