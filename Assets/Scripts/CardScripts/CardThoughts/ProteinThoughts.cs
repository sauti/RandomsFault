using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class ProteinThoughts: CardThoughts
    {
        public ProteinThoughts() : base()
        {
            Add("Nice protein!", new List<int>() { 0, 2 });
            Add("Awesome protein!", new List<int>() { 0, 1 });
        }
    }
}