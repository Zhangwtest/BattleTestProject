using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tool.Database;

public class MonsterHpNum : MonoBehaviour
{
    public Text monsterHp;
    public Text baseMonsterHP;
    public Image hpBar;
    public Image hpBarReduce;
    public GameObject monsterInfo;

    private MonsterConfigData monster;
    private SkeletonControl sc;

    // Start is called before the first frame update
    void Start()
    {
        monster = ConfigDataMgr.Ins.getMonsterConfigData(1);

    }

    // Update is called once per frame
    void Update()
    {
        sc = EnemyBorn.Instance.getEnemy();
        Debug.Log(sc);
        //血条上数字的显示
        if (sc == null)
        {
            monsterHp.text = 0.ToString();
            baseMonsterHP.text = 0.ToString();
            monsterInfo.SetActive(false);
        }
        else
        {
            monsterInfo.SetActive(true);
            baseMonsterHP.text = monster.baseHp.ToString();
            monsterHp.text = sc.enemyHP.ToString();

            //血条的增减
            hpBar.fillAmount = (float)sc.enemyHP / monster.baseHp;
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
}
