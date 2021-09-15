using UnityEditor;

namespace DS.Utilities
{
    public static class DSIOUtility
    {
        public static void Save()
        {
        }

        private static void CreateFolder(string parentFolderPath, string newFolderName)
        {
            if (AssetDatabase.IsValidFolder($"{parentFolderPath}/{newFolderName}"))
            {
                return;
            }

            AssetDatabase.CreateFolder(parentFolderPath, newFolderName);
        }
    }
}