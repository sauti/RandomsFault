using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class ProteinThoughts: CardThoughts
    {
        public ProteinThoughts()
        {
            Add("Nice protein!", new int[] { 0, 2 });
            Add("Awesome protein!", new int[] { 0, 1 });
        }
    }
}
