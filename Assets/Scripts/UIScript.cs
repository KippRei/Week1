using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    private bool restart = false;
    private bool paused = false;
    private Animator continueBtn;
    private Animator exitBtn;
    private Vector3 mousePos;

    GraphicRaycaster gRayCaster;
    PointerEventData pointerEventData;

    // Start is called before the first frame update
    void Start()
    {
        continueBtn = pauseScreen.transform.Find("Continue").GetComponent<Animator>();
        exitBtn = pauseScreen.transform.Find("Level Select").GetComponent<Animator>();
        continueBtn.enabled = false;
        exitBtn.enabled = false;
        pauseScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pointerEventData = new PointerEventData(EventSystem.current);
        gRayCaster = pauseScreen.GetComponent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("pause") && !paused)
        {
            paused = true;
        }
        else if (Input.GetButtonDown("pause") && paused)
        {
            paused = false;
        }
        if (restart && Input.GetButton("Fire3"))
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }
        PauseGame();
    }

    private void PauseGame()
    {
        if (paused)
        {
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            PauseSelect();
        }
        if (!paused)
        {
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        restart = true;
    }

    public bool GamePaused()
    {
        return paused;
    }

    private void PauseSelect()
    {
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gRayCaster.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.name == "Continue")
            {
                continueBtn.enabled = true;
                exitBtn.enabled = false;
                if (Input.GetButtonDown("Fire1"))
                {
                    paused = false;
                }
            }
            else if (result.gameObject.name == "Level Select")
            {
                continueBtn.enabled = false;
                exitBtn.enabled = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    Time.timeScale = 1f;
                    SceneManager.LoadScene("Level Select");
                }
            }
            else
            {
                continueBtn.enabled = false;
                exitBtn.enabled = false;
            }
        }
    }
}
