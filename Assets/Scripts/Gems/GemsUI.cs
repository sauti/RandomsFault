using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Default {
public class GemsUI : MonoBehaviour
{
    public GameObject overlay;
    public GameObject bag;

    public void OpenBag(bool hasOverlay) {
        overlay.SetActive(hasOverlay);
        bag.SetActive(true);
    }

    public void CloseBag() {
        overlay.SetActive(false);
        bag.SetActive(false);
    }
}
}
