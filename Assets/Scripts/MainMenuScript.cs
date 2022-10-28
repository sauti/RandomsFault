using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public bool revClick;

    public void LoadingPanel(){

        LoadScreen();
    }
    public void LoadLevel(int sceneIndex){
        StartCoroutine(LoadAsync(sceneIndex + 1));
    }

    IEnumerator LoadAsync (int sceneIndex){
        SceneManager.LoadSceneAsync(sceneIndex);
        yield return null;
    }
    IEnumerator LoadScreen(){
        loadingScreen.SetActive(true);
        return null;
    }

    public void QuitGame(){
        Application.Quit();
        Debug.Log("QUIT");
    }  

    public void reversClick(){
        revClick = !revClick;
    } 
}
