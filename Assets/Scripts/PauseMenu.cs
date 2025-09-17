using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    Scene currentScene;

    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    void Update()
    {
        
    }

    public void Respawn()
    {
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void ReturnToMenu()
    {
        UnPause();
        SceneManager.LoadScene(0);
    }

    public void UnPause()
    {

    }

    public void Pause()
    {

    }
}
