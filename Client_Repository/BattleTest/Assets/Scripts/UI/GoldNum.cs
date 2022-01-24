using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldNum : MonoBehaviour
{
    public int firstGoldNum;
    public Text GoldText;
    public static int nowGoldNum;


    void Start()
    {
        nowGoldNum = firstGoldNum;
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = nowGoldNum.ToString();
    }
}
