using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public string levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    public void Replay()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Play()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void Level()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
