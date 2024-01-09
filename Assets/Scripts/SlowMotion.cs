using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotion : MonoBehaviour
{
    public float slowMoSpeed = 0.2f;
    public float slowMoRegen = 0.002f;
    public float slowMoDrain = 0.0015f;
    public Slider slowMoSlider = null;
    

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
        if (Input.GetButton("Fire3") && slowMoAvailable)
        {
            Time.timeScale = slowMoSpeed;
            slowMoSlider.value -= slowMoDrain;
            if (slowMoSlider.value < 0.01 || Input.GetButtonUp("Fire1"))
            {
                slowMoAvailable = false;
            }
        }
        else
        {
            Time.timeScale = 1.0f;
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
}
