using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class ScratchThoughts: CardThoughts
    {
        public ScratchThoughts()
        {
            Add("Looks like a claw gauntlet weapon", new List<int>() { 0, 1 });
            Add("Perfect fit for my claws", new List<int>() { 1, 2 });
        }
    }
}
