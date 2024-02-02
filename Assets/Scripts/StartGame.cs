using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public Button playButton;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void BeginGame()
    {
        SceneManager.LoadScene(1);
    }
}
