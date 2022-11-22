using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class MurenaThoughts: CardThoughts
    {
        public MurenaThoughts() : base()
        {
            Add("What a nasty surprise!", new List<int>() { 0, 1 });
            Add("Deadly beautiful", new List<int>() { 1, 2 });
        }
    }
}
