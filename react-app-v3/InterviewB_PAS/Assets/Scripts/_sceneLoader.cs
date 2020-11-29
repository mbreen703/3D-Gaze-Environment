using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

public class _sceneLoader : MonoBehaviour
{
    //[DllImport("__Internal")]
    //private static extern void SceneLoaderEvent();

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadSceneAsync("Interview");
        /*
        #if !UNITY_EDITOR && UNITY_WEBGL
        void SceneLoaderEvent()
        {
            SceneManager.LoadScene(1);
        }
        #endif
        */
    }


}
