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

            bool oldGroupedDialoguesFilter = groupedDialoguesProperty.boolValue;
            bool oldStartingDialoguesOnlyFilter = startingDialoguesOnlyProperty.boolValue;

            DrawFiltersArea();

            bool currentGroupedDialoguesFilter = groupedDialoguesProperty.boolValue;
            bool currentStartingDialoguesOnlyFilter = startingDialoguesOnlyProperty.boolValue;
            
            ResetIndexOnFiltersUpdate(oldGroupedDialoguesFilter, oldStartingDialoguesOnlyFilter, currentGroupedDialoguesFilter, currentStartingDialoguesOnlyFilter);

            List<string> dialogueNames;

            string dialogueFolderPath = $"Assets/DialogueSystem/Dialogues/{currentDialogueContainer.FileName}";

            string dialogueInfoMessage;

            if (currentGroupedDialoguesFilter)
            {
                List<string> dialogueGroupNames = currentDialogueContainer.GetDialogueGroupNames();

                if (dialogueGroupNames.Count == 0)
                {
                    StopDrawing("There are no Dialogue Groups in this Dialogue Container.");

                    return;
                }

                DrawDialogueGroupArea(currentDialogueContainer, dialogueGroupNames);

                DSDialogueGroupSO dialogueGroup = (DSDialogueGroupSO) dialogueGroupProperty.objectReferenceValue;

                dialogueNames = currentDialogueContainer.GetGroupedDialogueNames(dialogueGroup, currentStartingDialoguesOnlyFilter);

                dialogueFolderPath += $"/Groups/{dialogueGroup.GroupName}/Dialogues";

                dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + " Dialogues in this Dialogue Group.";
            }
            else
            {
                dialogueNames = currentDialogueContainer.GetUngroupedDialogueNames(currentStartingDialoguesOnlyFilter);

                dialogueFolderPath += "/Global/Dialogues";

                dialogueInfoMessage = "There are no" + (currentStartingDialoguesOnlyFilter ? " Starting" : "") + "  Ungrouped Dialogues in this Dialogue Container.";
            }

            if (dialogueNames.Count == 0)
            {
                StopDrawing(dialogueInfoMessage);

                return;
            }

            DrawDialogueArea(dialogueNames, dialogueFolderPath);

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

            DSInspectorUtility.DrawDisabledFields(() => dialogueGroupProperty.DrawPropertyField());

            DSInspectorUtility.DrawSpace();
        }

        private void DrawDialogueArea(List<string> dialogueNames, string dialogueFolderPath)
        {
            DSInspectorUtility.DrawHeader("Dialogue");

            selectedDialogueIndexProperty.intValue = DSInspectorUtility.DrawPopup("Dialogue", selectedDialogueIndexProperty, dialogueNames.ToArray());

            string selectedDialogueName = dialogueNames[selectedDialogueIndexProperty.intValue];

            DSDialogueSO selectedDialogue = DSIOUtility.LoadAsset<DSDialogueSO>(dialogueFolderPath, selectedDialogueName);

            dialogueProperty.objectReferenceValue = selectedDialogue;

            DSInspectorUtility.DrawDisabledFields(() => dialogueProperty.DrawPropertyField());
        }

        private void StopDrawing(string reason, MessageType messageType = MessageType.Info)
        {
            DSInspectorUtility.DrawHelpBox(reason, messageType);

            DSInspectorUtility.DrawSpace();

            DSInspectorUtility.DrawHelpBox("You need to select a Dialogue for this component to work properly at Runtime!", MessageType.Warning);

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

        private void ResetIndexOnFiltersUpdate(bool oldGroupedDialogueFilter, bool oldStartingDialoguesOnlyFilter, bool currentGroupedDialogueFilter, bool currentStartingDialoguesOnlyFilter)
        {
            if (oldGroupedDialogueFilter != currentGroupedDialogueFilter)
            {
                selectedDialogueIndexProperty.intValue = 0;
            }

            if (oldStartingDialoguesOnlyFilter != currentStartingDialoguesOnlyFilter)
            {
                selectedDialogueIndexProperty.intValue = 0;
            }
        }
    }
}