using System;
using UnityEngine;

namespace Default
{
    public class DebugBoot : MonoBehaviour
    {
        [SerializeField] 
        private GridController _controller;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                _controller.GenerateField();

            var input = Vector2Int.zero;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                input.x = -1;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                input.x = 1;
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
                input.y = 1;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                input.y = -1;
            
            _controller.MovePlayerDelta(input);
        }
    }
}