using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelPaused;
    Scene currentScene;
    bool isPaused = false;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPaused)
            {
                Pause();
            }
            else
            {
                UnPause();
            }
        }
    }

    public void Respawn()
    {
        UnPause();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void ReturnToMenu()
    {
        UnPause();
        SceneManager.LoadScene(0);
    }

    public void UnPause()
    {
        Time.timeScale = 1f;
        isPaused = false;
        panelPaused.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;
        panelPaused.SetActive(true);
    }
}
