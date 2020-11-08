using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Flicker : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Image image;
    public float timeToWait;
    private float timeNow;
    void Start()
    {
        timeNow = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - timeNow > timeToWait)
        {
            timeNow = Time.time;
            text.enabled = !text.enabled;
            image.enabled = !image.enabled;
        }
    }
}
