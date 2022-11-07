using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
public class GemsBag : OnClickListener
{
    private GameUI UI;

    void Start()
    {
        UI = GameObject.Find("UI").GetComponent<GameUI>();
    }

    protected override void OnClick(RaycastHit hit)
    {
        if (hit.transform.name == "GemsBag") {
            UI.ToggleGemsBag();
        }
    }
}
}
