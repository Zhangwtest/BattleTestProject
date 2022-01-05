using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public bool isAttack;

    private Animator anim;
    private SkeletonControl sc;


    void Start()
    {
        anim = GetComponent<Animator>();       
    }

    void Update()
    {
        //PlayerBorn();    还没开始写
        PlayerAttack();
        sc = EnemyBorn.Instance.getEnemy();
    }

    /// <summary>
    /// 判断角色攻击的方法
    /// </summary>
    void PlayerAttack()
    {
        if (isAttack)
        {
            anim.SetBool("Attack", true);
        }

        if (sc == null)
        {
            return;
        }
        else
        {
            if (sc.enemyHP <= 0)
            {
                isAttack = false;
                anim.SetBool("Attack", false);
            }
        }
    }

    /// <summary>
    /// 怪物扣血的方法，在player的攻击动作最后一帧调用
    /// </summary>
    /// <returns></returns>
    void AttackNum()
    {
        if (sc == null)
        {
            return;
        }
        else
        {
            sc.enemyHP -= 1;
        }
    }








    /// <summary>
    /// 角色move和flip的方法
    /// </summary>
    //void PlayerRun()
    //{
    //    float x = Input.GetAxis("Horizontal");
    //    rig.velocity = new Vector2(x * moveSpeed, rig.velocity.y);
    //    Debug.Log(x);
    //    if (x != 0)
    //    {
    //        anim.SetBool("Run", true);
    //    }
    //    else
    //    {
    //        anim.SetBool("Run", false);
    //    }

    //    bool faceDir = Mathf.Abs(rig.velocity.x) > Mathf.Epsilon;
    //    if (faceDir)
    //    {
    //        //判断方向
    //        if (rig.velocity.x > 0.1f)
    //        {
    //            sr.flipX = false;
    //        }
    //        if (rig.velocity.x < -0.1f)
    //        {
    //            sr.flipX = true;
    //        }
    //    }
    //}



}
