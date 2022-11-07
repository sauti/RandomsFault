using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject swipe;

    private Button btn;
    private Animator animator; 

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate{ToggleMenu(!menuPanel.active);});
        animator = menuPanel.GetComponent<Animator>();
    }

    public void ToggleMenu(bool isShown){
        swipe.gameObject.SetActive(!isShown);
        menu.SetActive(isShown);
        animator.SetBool("SetYou", isShown);
    }
}
