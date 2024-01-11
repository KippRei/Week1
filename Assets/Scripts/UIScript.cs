using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    private bool restart = false;

    // Start is called before the first frame update
    void Start()
    {
        gameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (restart && Input.GetButton("Fire3"))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        }   
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        restart = true;
    }
}
