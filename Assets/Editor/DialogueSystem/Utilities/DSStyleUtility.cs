using UnityEditor;
using UnityEngine.UIElements;

namespace DS.Utilities
{
    public static class DSStyleUtility
    {
        public static VisualElement AddClasses(this VisualElement element, params string[] classNames)
        {
            foreach (string className in classNames)
            {
                element.AddToClassList(className);
            }

            return element;
        }

        public static VisualElement AddStyleSheets(this VisualElement element, params string[] styleSheetNames)
        {
            foreach (string styleSheetName in styleSheetNames)
            {
                StyleSheet styleSheet = (StyleSheet) EditorGUIUtility.Load(styleSheetName);

                element.styleSheets.Add(styleSheet);
            }

            return element;
        }
    }
}