using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Default {
public class MenuButton : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject swipe;

    private Button btn;
    private Animator animator; 
    private SwipeTest swipeController;

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(delegate{ToggleMenu(!menuPanel.active);});
        animator = menuPanel.GetComponent<Animator>();
        swipeController = swipe.GetComponent<SwipeTest>();
    }

    public void ToggleMenu(bool isShown) {
        if (isShown) {
            swipeController.PauseMove();
        } else {
            swipeController.ResumeMove();
        }
        menu.SetActive(isShown);
        animator.SetBool("SetYou", isShown);
    }
}
}