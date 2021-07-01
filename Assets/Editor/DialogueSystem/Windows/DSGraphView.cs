using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace DS.Windows
{
    public class DSGraphView : GraphView
    {
        public DSGraphView()
        {
            AddGridBackground();

            AddStyles();
        }

        private void AddGridBackground()
        {
            GridBackground gridBackground = new GridBackground();

            gridBackground.StretchToParentSize();

            Insert(0, gridBackground);
        }

        private void AddStyles()
        {
            StyleSheet styleSheet = (StyleSheet) EditorGUIUtility.Load("DialogueSystem/DSGraphViewStyles.uss");

            styleSheets.Add(styleSheet);
        }
    }
}