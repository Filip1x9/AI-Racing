using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapTimeManager : MonoBehaviour
{
    public static int MinuteCount;
    public static int SecondCount;
    public static float MillisecondCount;
    public static string MillisecondDisplay;

    public GameObject MinuteBox;
    public GameObject SecondBox;
    public GameObject MillisecondBox;

    void Update(){

        MillisecondCount += Time.deltaTime * 10;
        MillisecondDisplay = MillisecondCount.ToString("F0");
        MillisecondBox.GetComponent<Text>().text = "" + MillisecondDisplay;

        if(MillisecondCount >= 10){

            MillisecondCount = 0;
            SecondCount++;

        }

        if(SecondCount <=9){

            SecondBox.GetComponent<Text>().text = "0" + SecondCount + "."; 

        }else{

            SecondBox.GetComponent<Text>().text = "" + SecondCount + "."; 

        }

        if(SecondCount >= 60){

            SecondCount = 0;
            MinuteCount++;

        }

        if(MinuteCount <= 9){

            MinuteBox.GetComponent<Text>().text = "0" + MinuteCount + ":";

        }else{

            MinuteBox.GetComponent<Text>().text = "" + MinuteCount + ":";

        }
        
    }
}
