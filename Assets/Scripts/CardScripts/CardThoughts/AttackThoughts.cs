using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class AttackThoughts: CardThoughts
    {
        public AttackThoughts()
        {
            Add("Strong attack!", new int[] { 0, 1 });
            Add("Awesome attack!", new int[] { 0, 1 });
        }
    }
}
