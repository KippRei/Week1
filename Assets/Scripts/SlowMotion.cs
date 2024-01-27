using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    public UIScript gameLoop;
    public float slowMoSpeed = 0.2f;
    public float slowMoRegen = 0.002f;
    public float slowMoDrain = 0.0015f;
    public Slider slowMoSlider;
    public Player player;
    

    private float fixedDeltaTime;
    private bool slowMoAvailable = true;


    // Start is called before the first frame update
    void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameLoop.GamePaused() && !gameLoop.gameOver)
        {
            if (Input.GetButton("Fire3") && slowMoAvailable)
            {
                player.trail.enabled = true;
                Time.timeScale = slowMoSpeed;
                slowMoSlider.value -= slowMoDrain;
                player.invincible = true;
                if (slowMoSlider.value < 0.01)
                {
                    slowMoAvailable = false;
                }
            }
            if (!Input.GetButton("Fire3") || !slowMoAvailable)
            {
                player.trail.enabled = false;
                Time.timeScale = 1f;
                player.invincible = false;
                if (slowMoSlider.value < 1f)
                {
                    slowMoSlider.value += slowMoRegen;
                }
                if (slowMoSlider.value >= 0.99f)
                {
                    slowMoAvailable = true;
                }
            }
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        }   
        if (gameLoop.gameOver)
        {
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        }
    }
}
