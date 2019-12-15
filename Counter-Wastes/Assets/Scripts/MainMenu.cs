using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenu;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void NewGame()
    {
        PlayGame();
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OptionsBack()
    {
        optionsMenu.SetActive(false);
    }

    public void Options()
    {
        optionsMenu.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionsBack();
        }
    }
}
