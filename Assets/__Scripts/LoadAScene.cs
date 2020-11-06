using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadAScene : MonoBehaviour
{
    public void LoadNextOrPrevScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }// end LoadNextOrPrevScene()

}// end class LoadAScene

