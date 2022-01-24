using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NormalMonsterNum : MonoBehaviour
{
    public int firstNum;
    public Text NormalText;
    public static int nowNormalNum;


    void Start()
    {
        nowNormalNum = firstNum;
    }

    // Update is called once per frame
    void Update()
    {
        NormalText.text = nowNormalNum.ToString();
    }
}
