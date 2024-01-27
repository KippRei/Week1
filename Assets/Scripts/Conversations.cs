using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class Conversations : MonoBehaviour
{
    public struct Conversation
    {
        public string name;
        public string line;
    }

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
    private bool hadThisConvo = false;
    private string imgFilePath = "Images/CharacterImagesForDialogue/";

    public List<Conversation> convo = new List<Conversation>();

    // Update is called once per frame
    void Update()
    {
        if (convoCanvas.gameObject.activeSelf && textBoxAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !convoStarted)
        {
            convoStarted = true;
            player.SetPlayerInputEnabled(false);
            currentTextDisplay = StartConvo(convo[convoLineCounter].line);

        }

        if (convoStarted && Input.GetButtonDown("Fire1"))
        {
            if (!fullLineDisplayed)
            {
                StopCoroutine(currentTextDisplay);
                convoText.text = "";
                convoText.text = convo[convoLineCounter].line;
                fullLineDisplayed = true;
                convoLineCounter++;
            }
            else if (fullLineDisplayed && convoLineCounter < convo.Count)
            {
                convoText.text = "";
                currentTextDisplay = StartConvo(convo[convoLineCounter].line);
                convoImage.sprite = Resources.Load<Sprite>(imgFilePath + convo[convoLineCounter].name);
                fullLineDisplayed = false;
            }
            else if (convoLineCounter == convo.Count)
            {
                convoCanvas.gameObject.SetActive(false);
                convoStarted = false;
                fullLineDisplayed = false;
                convoLineCounter = 0;
                player.SetPlayerInputEnabled(true);
            }
        }
    }

    public void StartConversation(string convoFileName)
    {
        convoText.text = "";

        StreamReader convoFile = new StreamReader(Application.dataPath + "/Resources/Dialogue/" + convoFileName + ".txt");
        string convoLines = convoFile.ReadToEnd();
        convoFile.Close();
        string[] convoLinesArr = convoLines.Split('\n');

        foreach (string line in convoLinesArr)
        {
            string[] nameLine = line.Split(">>");
            Conversation tmp = new Conversation
            {
                name = nameLine[0].Trim(),
                line = nameLine[1].Trim()
            };
            convo.Add(tmp);
        }

        convoCanvas.gameObject.SetActive(true);
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
