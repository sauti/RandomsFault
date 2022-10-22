using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Default {
    public class Thought
    {
        public string text;
        public List<int> levels;

        public Thought(string thoughtText, List<int> levelsList) {
            text = thoughtText;
            levels = levelsList;
        }
    }

    public class CardThoughts
    {
        public List<Thought> list = new List<Thought>();

        public void Add(string text, List<int> levels)
        {
            list.Add(new Thought(text, levels));
        }

        public List<Thought> GetList()
        {
            return list;
        }
    }
}
