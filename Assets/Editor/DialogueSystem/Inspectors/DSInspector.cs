using UnityEditor;

namespace DS.Inspectors
{
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

        private void OnEnable()
        {
            dialogueContainerProperty = serializedObject.FindProperty("dialogueContainer");
            dialogueGroupProperty = serializedObject.FindProperty("dialogueGroup");
            dialogueProperty = serializedObject.FindProperty("dialogue");

            groupedDialoguesProperty = serializedObject.FindProperty("groupedDialogues");
            startingDialoguesOnlyProperty = serializedObject.FindProperty("startingDialoguesOnly");
        }

        public override void OnInspectorGUI()
        {
            DrawDialogueContainerArea();
        }

        private void DrawDialogueContainerArea()
        {
            EditorGUILayout.LabelField("Dialogue Container", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(dialogueContainerProperty);
        }
    }
}