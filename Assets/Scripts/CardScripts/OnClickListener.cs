using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
public class OnClickListener : MonoBehaviour
{
    private bool clicking = false;
    private float totalDownTime = 0;
    public float longClickDuration = 1;

    protected GameUI UI;

    void Awake()
    {
        UI = GameObject.Find("UI").GetComponent<GameUI>();
    }

    protected void Update()
    {
        if (UI.isUIOverlay) {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            totalDownTime = 0;
            clicking = true;
        }

        if (clicking && Input.GetMouseButton(0))
        {
            totalDownTime += Time.deltaTime;
            if (totalDownTime >= longClickDuration)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (!Physics.Raycast(ray, out hit)) {
                    return;
                }  

                clicking = false;
                totalDownTime = 0;
                OnLongClick(hit);
            }
        }

        if (clicking && Input.GetMouseButtonUp(0))
        {
            if (totalDownTime < longClickDuration)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);  
                RaycastHit hit;  
                if (!Physics.Raycast(ray, out hit)) {
                    return;
                }  

                clicking = false;
                totalDownTime = 0;
                OnClick(hit);
            }
        }
    }

    protected virtual void OnClick(RaycastHit hit) {}

    protected virtual void OnLongClick(RaycastHit hit) {}
}
}
