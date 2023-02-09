using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class HuntingOrchidThoughts : CardThoughts
    {
        public HuntingOrchidThoughts() : base()
        {
            Add("Why it smells like my grandma meatballs?..", new List<int>() { 0, 1 });
            Add("The most interesting plants grow in the shade", new List<int>() { 0, 1 });
        }
    }
}
