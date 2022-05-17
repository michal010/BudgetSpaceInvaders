using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int _sceneIndex;

    public void LoadScene()
    {
        //Try load scene by given index.
        try
        {
            SceneManager.LoadScene(_sceneIndex);
        }
        catch(Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

}
