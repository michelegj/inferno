using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{

    [Header("Component")]
    public bool isEnabled = true;
    public TextMesh[] timerText;
    public TextMeshProUGUI timerPro;
    private Color timerColor;

    [Header("Timer Options")]
    //countDown = true goes backwards
    public float currentTime;
    public string timeFormat = "0.0000";
    public bool countDown = false;
    public bool timerActive = true;

    [Header("Limit Options")]  
    public bool hasLimit = false;
    public float timerLimit;
    public string setTimerColor;

    void Awake()
    {
        timerColor = timerPro.color;
    }

    void Update()
    { 
        ChangeTimerColor(setTimerColor);
        if (timerActive)
        {
            FlowTimerCheck();
            LimitTimerCount();
        }
        SetTimerText();
    }

    void SetTimerText()
    {
        if (isEnabled)
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].text = currentTime.ToString(timeFormat);
            timerPro.text = currentTime.ToString(timeFormat);
        } 
        else if (!isEnabled)
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].text = " ";
            timerPro.text = " ";
        }
    }

    void FlowTimerCheck()
    {
        if (countDown)
        {
            currentTime = currentTime -= Time.deltaTime;
 
        }
        else if (!countDown)
        {
            currentTime = currentTime += Time.deltaTime;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Stop"))
        {
            timerActive = false;
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = timerColor;
            timerPro.color = timerColor;
        }
        else if (collision.gameObject.tag.Equals("Start"))
        {
            hasLimit = false;
            timerActive = true;
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = timerColor;
            timerPro.color = timerColor;
            
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("Stop"))
        {
            timerActive = false;
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = timerColor;
            timerPro.color = timerColor;
        }
        else if (collision.gameObject.tag.Equals("Start"))
        {
            hasLimit = false;
            timerActive = true;
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = timerColor;
            timerPro.color = timerColor;
            
        }
    }

    void LimitTimerCount()
    {
        if (hasLimit && countDown && currentTime <= timerLimit) 
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = Color.red;
            timerPro.color = Color.red;
            timerActive = false;
        }
        else if (hasLimit && !countDown && currentTime >= timerLimit)
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = Color.red;
            timerPro.color = Color.red;
            timerActive = false;
        }
    }

    void ChangeTimerColor(string colorName)
    {
        if (colorName.Equals("aqua"))
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = Color.cyan;
            timerPro.color = Color.cyan;
        }
        else if (colorName.Equals("white"))
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = Color.white;
            timerPro.color = Color.white;
        }
        else if (colorName.Equals("green"))
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = Color.green;
            timerPro.color = Color.green;
        }
        else if (colorName.Equals("yellow"))
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = Color.yellow;
            timerPro.color = Color.yellow;
        }
        else
        {
            for (int i = 0; i < timerText.Length; i++) 
                timerText[i].color = timerColor;
            timerPro.color = timerColor;
        }
    }
}
