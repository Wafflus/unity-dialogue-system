using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace DS.Elements
{
    using Enumerations;

    public class DSNode : Node
    {
        public string DialogueName { get; set; }
        public List<string> Choices { get; set; }
        public string Text { get; set; }
        public DSDialogueType DialogueType { get; set; }

        public void Initialize()
        {
            DialogueName = "DialogueName";
            Choices = new List<string>();
            Text = "Dialogue text.";
        }
    }
}