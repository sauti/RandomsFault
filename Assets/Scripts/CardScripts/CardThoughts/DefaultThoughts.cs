using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class DefaultThoughts: CardThoughts
    {
        public DefaultThoughts()
        {
            Add("No idea what is this card", new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 });
        }
    }
}
