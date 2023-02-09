using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class WayOutThoughts: CardThoughts
    {
        public WayOutThoughts() : base()
        {
            Add("My feelers are telling me it might be an exit", new List<int>() { 0, 1 });
            Add("There's sweet smell on the air this way", new List<int>() { 0, 1 });

        }
    }
}
