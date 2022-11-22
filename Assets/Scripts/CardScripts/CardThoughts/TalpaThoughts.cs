using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class TalpaThoughts: CardThoughts
    {
        public TalpaThoughts() : base()
        {
            Add("Yuck, you smell like my auntie", new List<int>() { 0, 1 });
            Add("Protein jelly, yammy", new List<int>() { 1, 2 });
        }
    }
}
