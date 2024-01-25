using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuddyConvo1 : MonoBehaviour
{
    [SerializeField] private Canvas convoCanvas;
    [SerializeField] private Player player;
    [SerializeField] private Image convoImage;
    [SerializeField] private Animator textBoxAnimator;
    [SerializeField] private TextMeshProUGUI convoText;
    [SerializeField] private float textSpeed = 0.04f;
    private int convoLineCounter = 0;
    private bool fullLineDisplayed = false;
    private bool convoStarted = false;
    private Coroutine currentTextDisplay = null;
    private string[] conversation = {
        "Hello friend, you better get working. What's your name?",
        "Uhh, where am I?",
        "Wow, did you hit your head or something? You're on the mining planet Wash."
    };


    // Start is called before the first frame update
    void Start()
    {
        convoText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (convoCanvas.gameObject.activeSelf && textBoxAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !convoStarted)
        {
            convoStarted = true;
            currentTextDisplay = StartConvo(conversation[convoLineCounter]);
        }

        if (convoStarted && Input.GetButtonDown("Fire1"))
        {
            if (!fullLineDisplayed)
            {
                StopCoroutine(currentTextDisplay);
                convoText.text = "";
                convoText.text = conversation[convoLineCounter];
                fullLineDisplayed = true;
                convoLineCounter++;
            }
            else if (fullLineDisplayed && convoLineCounter < conversation.Length)
            {
                convoText.text = "";
                currentTextDisplay = StartConvo(conversation[convoLineCounter]);
                fullLineDisplayed= false;
            }
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.IsStopped())
        {
            convoCanvas.gameObject.SetActive(true);
        }
    }

    Coroutine StartConvo(string line)
    {
        return StartCoroutine(TextAnimate(line));
    }

    IEnumerator TextAnimate(string textToDisplay)
    {
        foreach (char c in textToDisplay)
        {
            convoText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        fullLineDisplayed = true;
        convoLineCounter++;
    }
}
