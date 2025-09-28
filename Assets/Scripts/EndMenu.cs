using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelEnd;
    bool isPaused = false;

    public void ReturnToMenu()
    {
        PanelDeactive();
        SceneManager.LoadScene(0);
    }

    public void PanelDeactive()
    {
        Time.timeScale = 1f;
        isPaused = false;
        panelEnd.SetActive(false);
    }

    public void PanelActive()
    {
        Time.timeScale = 0f;
        isPaused = true;
        panelEnd.SetActive(true);
    }
}
