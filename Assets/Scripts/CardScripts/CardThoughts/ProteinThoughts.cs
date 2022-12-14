using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class ProteinThoughts: CardThoughts
    {
        public ProteinThoughts() : base()
        {
            Add("Easy-to-digest high quality protein", new List<int>() { 0, 1 });
            Add("Protein jelly, yammy", new List<int>() { 1, 2 });
        }
    }
}
