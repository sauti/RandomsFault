using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
public class GemsBag : OnClickListener
{
    protected override void OnClick(RaycastHit hit)
    {
        if (hit.transform.name == "GemsBag") {
            UI.OpenGemsBag();
        }
    }
}
}
