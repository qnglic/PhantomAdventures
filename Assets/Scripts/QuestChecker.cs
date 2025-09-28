using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class QuestChecker : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox, finishedText, unfinishedText;
    [SerializeField] private int questGoal = 10;
    [SerializeField] private int levelToLoad;
    [SerializeField] private bool isLastScene = false;

    private Animator anim;
    private bool levelIsLoading = false;
    private EndMenu endMenu;

    void Start()
    {
        anim = GetComponent<Animator>();
        endMenu = FindFirstObjectByType<EndMenu>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().coinsCollected >= questGoal)
            {
                dialogueBox.SetActive(true);
                finishedText.SetActive(true);
                anim.SetTrigger("Lootbox");

                // Change level
                if (isLastScene == true)
                {
                    Invoke("EndGame", 2f);
                }
                else
                {
                    Invoke("LoadNextLevel", 2f);
                }
                levelIsLoading = true;
            }
            else
            {
                dialogueBox.SetActive(true);
                unfinishedText.SetActive(true);
            }
        }
    }

    private void EndGame()
    {
        endMenu.PanelActive();
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !levelIsLoading)
        {
            dialogueBox.SetActive(false);
            finishedText.SetActive(false);
            unfinishedText.SetActive(false);
        }
    }
}
