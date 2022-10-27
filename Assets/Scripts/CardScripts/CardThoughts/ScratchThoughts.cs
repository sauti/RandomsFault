using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class ScratchThoughts: CardThoughts
    {
        public ScratchThoughts()
        {
            Add("Strong attack!", new List<int>() { 0, 1 });
            Add("Awesome attack!", new List<int>() { 0, 1 });
        }
    }
}
