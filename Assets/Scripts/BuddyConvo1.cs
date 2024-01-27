using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Conversations;

public class BuddyConvo1 : MonoBehaviour
{
    [SerializeField] private Conversations dialogueController;
    [SerializeField] private Player player;
    private bool hadThisConvo = false;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && player.IsStopped() && !hadThisConvo)
        {
            hadThisConvo = true;
            dialogueController.StartConversation("buddyconvo1");
        }
    }
}
