using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class Ending : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject win;

    public static Ending instance;

    private void Awake()
    {
        instance = this;
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        
    }

    public void DefeatUI()
    {
        gameOver.SetActive(true);
    }

    public void VictoryUI()
    {
        gameOver.SetActive(true);
    }
}
