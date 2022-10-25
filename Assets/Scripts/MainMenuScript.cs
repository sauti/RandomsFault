using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image img;    

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
}
