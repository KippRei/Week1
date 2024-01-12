using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    private Vector2 mousePos;
    private TextMeshPro _textMeshPro = null;

    // Start is called before the first frame update
    void Start()
    {
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
                if (Input.GetButton("Fire1"))
                {
                    SceneManager.LoadSceneAsync("Level1");
                }
            }
            else if (hit.gameObject.name == "Planet2")
            {
                _textMeshPro = hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButton("Fire1"))
                {
                    Debug.Log("load 2");
                }
            }
            else if (hit.gameObject.name == "Planet3")
            {
                _textMeshPro = hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButton("Fire1"))
                {
                    Debug.Log("load 3");
                }
            }
            else if (hit.gameObject.name == "Planet4")
            {
                _textMeshPro = hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButton("Fire1"))
                {
                    Debug.Log("load 4");
                }
            }
            else if (hit.gameObject.name == "Planet5")
            {
                _textMeshPro = hit.gameObject.GetComponentInChildren<TextMeshPro>();
                _textMeshPro.enabled = true;
                if (Input.GetButton("Fire1"))
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
}
