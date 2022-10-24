using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public GameObject loadingScreen;
    public Image img;
    
//     public void StartGame(){
//         loadingScreen.gameObject.SetActive(true);
//         //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
//         //loadingScreen.SetActive(false);
//         //StartSceen();
//                 SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().buildIndex +1);

//     }

//     public void StartSceen(){
//         SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().buildIndex +1);
//     }

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
