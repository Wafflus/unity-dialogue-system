using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace DS.Data.Error
{
    public class DSGroupErrorData
    {
        public DSErrorData ErrorData { get; set; }
        public List<Group> Groups { get; set; }

        public DSGroupErrorData()
        {
            ErrorData = new DSErrorData();
            Groups = new List<Group>();
        }
    }
}