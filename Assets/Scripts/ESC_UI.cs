using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ESC_UI : MonoBehaviour
{
    public GameObject ParentUI;

    private bool isShowingCanvas = true;

    private void Start()
    {
        ToggleUI();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleUI();
        }
    }

    public void Continue()
    {
        ToggleUI();
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Farm");
        Time.timeScale = 1;
    }

    public void QuitScene()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1;
    }

    private void ToggleUI()
    {
        ParentUI.SetActive(!isShowingCanvas);
        isShowingCanvas = !isShowingCanvas;
        Time.timeScale = isShowingCanvas ? 0 : 1;
    }
}
