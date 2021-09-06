using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace DS.Windows
{
    using Utilities;

    public class DSEditorWindow : EditorWindow
    {
        private readonly string defaultFileName = "DialoguesFileName";

        private TextField fileNameTextField;
        private Button saveButton;

        [MenuItem("Window/DS/Dialogue Graph")]
        public static void Open()
        {
            GetWindow<DSEditorWindow>("Dialogue Graph");
        }

        private void OnEnable()
        {
            AddGraphView();
            AddToolbar();

            AddStyles();
        }

        private void AddGraphView()
        {
            DSGraphView graphView = new DSGraphView(this);

            graphView.StretchToParentSize();

            rootVisualElement.Add(graphView);
        }

        private void AddToolbar()
        {
            Toolbar toolbar = new Toolbar();

            fileNameTextField = DSElementUtility.CreateTextField(defaultFileName, "File Name:", callback =>
            {
                fileNameTextField.value = callback.newValue.RemoveWhitespaces().RemoveSpecialCharacters();
            });

            saveButton = DSElementUtility.CreateButton("Save");

            toolbar.Add(fileNameTextField);
            toolbar.Add(saveButton);

            toolbar.AddStyleSheets("DialogueSystem/DSToolbarStyles.uss");

            rootVisualElement.Add(toolbar);
        }

        private void AddStyles()
        {
            rootVisualElement.AddStyleSheets("DialogueSystem/DSVariables.uss");
        }

        public void EnableSaving()
        {
            saveButton.SetEnabled(true);
        }

        public void DisableSaving()
        {
            saveButton.SetEnabled(false);
        }
    }
}