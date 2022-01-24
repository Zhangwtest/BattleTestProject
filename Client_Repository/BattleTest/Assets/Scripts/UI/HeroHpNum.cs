using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tool.Database;

public class HeroHpNum : MonoBehaviour
{
    public Text heroHp;
    public Text baseHeroHP;
    public Image hpBar;
    public Image hpBarReduce;

    private HeroConfigData hero;
    private PlayerControl pc;

    // Start is called before the first frame update
    void Start()
    {
        hero = ConfigDataMgr.Ins.getHeroConfigData(1);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        //血条上数字的显示
        baseHeroHP.text = hero.baseHp.ToString();
        heroHp.text = pc.heroHp.ToString();

        //血条的增减
        hpBar.fillAmount = (float)pc.heroHp / hero.baseHp;
        if (hpBarReduce.fillAmount > hpBar.fillAmount)
        {
            hpBarReduce.fillAmount -= 0.003f;
        }
        else
        {
            hpBarReduce.fillAmount = hpBar.fillAmount;
        }


    }
}
