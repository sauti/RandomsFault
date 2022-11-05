using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    private Button btn;
    // [SerializeField] private Button gameMenuBtn; 
    private Animator animator; 

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate{ToggleMenu(!menuPanel.active);});
        animator = menuPanel.GetComponent<Animator>();
    }

    public void ToggleMenu(bool isShown){
        menuPanel.SetActive(isShown);
        animator.SetBool("SetYou", isShown);
    }
}
