using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Default {
    public class Thought
    {
        public string text;
        public int[] levels;

        public Thought(string thoughtText, int[] levelsList) {
            text = thoughtText;
            levels = levelsList;
        }
    }

    public class CardThoughts
    {
        public List<Thought> list = new List<Thought>();

        public void Add(string text, int[] levels)
        {
            list.Add(new Thought(text, levels));
        }

        public List<Thought> GetList()
        {
            return list;
        }
    }
}
