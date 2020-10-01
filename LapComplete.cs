using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class LapComplete : MonoBehaviour
{
    public GameObject lapCompleteTrig;
    
    public GameObject MinuteDisplay;
    public GameObject SecondDisplay;
    public GameObject MillisecondDisplay;
    public GameObject lapsCompletedDisplay;

    public int lapsCompletedCount = 0;

    void OnTriggerEnter(){

        if(LapTimeManager.SecondCount <= 9){

            SecondDisplay.GetComponent<Text>().text = "0" + LapTimeManager.SecondCount + ".";

        }else{

            SecondDisplay.GetComponent<Text>().text = "" + LapTimeManager.SecondCount + ".";

        }

        if(LapTimeManager.MinuteCount <= 9){

            MinuteDisplay.GetComponent<Text>().text = "0" + LapTimeManager.MinuteCount + ":";

        }else{

            MinuteDisplay.GetComponent<Text>().text = "" + LapTimeManager.MinuteCount + ":";

        }
        
        MillisecondDisplay.GetComponent<Text>().text = "" + (int)LapTimeManager.MillisecondCount;

        LapTimeManager.MinuteCount = 0;
        LapTimeManager.SecondCount = 0;
        LapTimeManager.MillisecondCount = 0;

        Debug.Log(lapsCompletedCount);
        lapsCompletedCount++;
        lapsCompletedDisplay.GetComponent<Text>().text = "" + lapsCompletedCount;

        lapCompleteTrig.SetActive(true);

    }

}
