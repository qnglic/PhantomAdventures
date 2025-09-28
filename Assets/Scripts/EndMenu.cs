using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private GameObject panelEnd;

    public void ReturnToMenu()
    {
        PanelDeactive();
        SceneManager.LoadScene(0);
    }

    public void PanelDeactive()
    {
        Time.timeScale = 1f;
        panelEnd.SetActive(false);
    }

    public void PanelActive()
    {
        Time.timeScale = 0f;
        panelEnd.SetActive(true);
    }
}
