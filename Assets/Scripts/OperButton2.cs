using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class OperButton2 : MonoBehaviour
{
    public GameObject sign;
    private float timeNow;
    public float timeToWait;
    private bool show;

    public GameObject recordingSign;
    private bool giffing;
    public Record record;
    public TextMeshProUGUI tipText;
    // Start is called before the first frame update
    void Start()
    {
        show = false;
        giffing = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            if(Time.time - timeNow > timeToWait)
            {
                timeNow = Time.time;
                show = false;
                sign.SetActive(false);
            }
        }

        if(record.m_IsSaving)
        {
            tipText.enabled = true;
            tipText.text = record.strToShow;
        }
        else
        {
            tipText.enabled = false;
        }

    }


    public void Capture()
    {
        CamCapture.Instance.Capture(Application.dataPath + "/Capture");
        timeNow = Time.time;
        show = true;
        sign.SetActive(true);
    }

    public void Gif()
    {
        
        giffing = !giffing;
        if (giffing)
        {
            recordingSign.SetActive(true);
            record.StartToRecord();
        }
        if (!giffing)
        {
            recordingSign.SetActive(false);
            record.StopRecording();
            tipText.enabled = true;
            
            
            
            
            
            
        }
    }
}
