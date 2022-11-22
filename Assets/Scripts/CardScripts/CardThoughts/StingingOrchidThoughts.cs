using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class StingingOrchidThoughts : CardThoughts
    {
        public StingingOrchidThoughts() : base()
        {
            Add("Ouch! My paw!", new List<int>() { 0, 1 });
            Add("What are you, some kind of a stinging nettle?", new List<int>() { 1, 2 });            
        }
    }
}
