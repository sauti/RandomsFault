using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
    public class CardGameController : MonoBehaviour
    {
        public Vector2Int _gridSize = new Vector2Int(4, 3);
        public Vector2Int _handSize = new Vector2Int(4, 2);
        public GameObject _tableGo;
        public GameObject _handGo;

        private TableController _tableCtrl;
        private HandController _handCtrl;

        void Start()
        {
            _tableCtrl = _tableGo.GetComponent<TableController>();
            _handCtrl = _handGo.GetComponent<HandController>();
            _tableCtrl.initCards(_gridSize);
            _handCtrl.initCards(_handSize);
        }

        void Update()
        {
            OnClickListener();
        }
        
        public void OnClickListener() {
            if (Input.GetMouseButtonDown(0)) {  
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (!Physics.Raycast(ray, out hit)) {
                    return;
                }  

                _handCtrl.OnClickListener(hit);
                _tableCtrl.OnClickListener(hit);
            } 
        }
    }
}
