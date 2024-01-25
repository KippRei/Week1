using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private GameObject loadScreen;
    [SerializeField] private TextMeshProUGUI loadingPercentage;
    private Vector2 mousePos;
    private TextMeshPro _textMeshPro = null;

    // Start is called before the first frame update
    void Start()
    {
        loadScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);
        if (hit != null )
        {
            if (hit.gameObject.name == "Planet1")
            {
                _textMeshPro =  hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    StartCoroutine(LoadLevel("Level1"));
                }
            }
            else if (hit.gameObject.name == "Planet2")
            {
                _textMeshPro = hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    StartCoroutine(LoadLevel("Intro Level"));
                }
            }
            else if (hit.gameObject.name == "Planet3")
            {
                _textMeshPro = hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("load 3");
                }
            }
            else if (hit.gameObject.name == "Planet4")
            {
                _textMeshPro = hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("load 4");
                }
            }
            else if (hit.gameObject.name == "Planet5")
            {
                _textMeshPro = hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("load 5");
                }
            }
        }
        else
        {
            if (_textMeshPro != null)
            {
                _textMeshPro.enabled = false;
            }
        }
    }

    IEnumerator LoadLevel(string lvlName)
    {
        loadScreen.SetActive(true);
        AsyncOperation loadingLvl = SceneManager.LoadSceneAsync(lvlName);

        while (!loadingLvl.isDone)
        {
            loadingPercentage.text = Mathf.Clamp01(loadingLvl.progress / 0.9f) * 100 + " %";
            yield return null;
        }
        loadScreen.SetActive(false);
    }
}
