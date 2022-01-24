using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tool.Database;

public class SkeletonControl : MonoBehaviour
{
    public int enemyHP;
    public float enemyMoveSpeed;
    public GameObject coin;

    private PlayerControl pc;
    private Animator anim;
    private Rigidbody2D rig;

    private bool isWalk; 
    private bool isDie;
    private bool isEnemyAttack;

    private MonsterConfigData monster;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
        monster = ConfigDataMgr.Ins.getMonsterConfigData(1);

        isWalk = true;
        enemyHP = monster.baseHp;
    }

    void Update()
    {
        EnemyWalk();
        EnemyAttack();
        EnemyDie();
    }

    /// <summary>
    /// 判断怪物行走的方法。
    /// </summary>
    void EnemyWalk()
    {
        if (isDie)
        {
            return;
        }

        if (isWalk)
        {
            rig.velocity = new Vector2(-enemyMoveSpeed, rig.velocity.y);
            anim.SetBool("Walk", true);
        }
        else
        {
            rig.velocity = new Vector2(0, 0);
            anim.SetBool("Walk", false);
        }
    }

    /// <summary>
    /// 检测和Player面前发生碰撞。
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("AttackPoint"))
        {
            isWalk = false;
            isEnemyAttack = true;

            pc.isAttack = true;
        }
    }

    /// <summary>
    /// 判断怪物攻击的方法。
    /// </summary>
    void EnemyAttack()
    {
        if (isDie)
        {
            return;
        }

        if (isEnemyAttack)
        {
            anim.SetBool("Attack", true);    
        }
    }

    /// <summary>
    /// 怪物攻击导致英雄扣血的方法，挂在怪物的攻击动作中触发。
    /// </summary>
    void PlayerHurt()
    {
        if (pc == null)
        {
            return;
        }
        else
        {
            //英雄扣血，后面要读怪物攻击力
            pc.heroHp -= monster.baseAttack;
        }
    }

    /// <summary>
    /// 判断怪物死亡的方法。
    /// </summary>
    void EnemyDie()
    {
        if (isDie)
        {
            return;
        }
        else
        {
            if (enemyHP <= 0)
            {
                isDie = true;
                anim.SetBool("Die",true);
                anim.SetBool("Attack", false);

                //普通怪物击杀数量+1
                NormalMonsterNum.nowNormalNum++;
                GoldNum.nowGoldNum+=100;

                Invoke("GenCoin",0.3f);
            }
        }
    }

    void GenCoin()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y-0.3f, transform.position.z);
        Instantiate(coin, pos, Quaternion.identity);
    }



    /// <summary>
    /// 怪物回收方法，挂在死亡动作中触发。
    /// </summary>
    void EnemyDestroy()
    {
        Destroy(gameObject);
        EnemyBorn.Instance.enemyList.Remove(this);
    }

}
