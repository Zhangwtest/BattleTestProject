using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBorn : MonoBehaviour
{
    public static EnemyBorn Instance { get; private set; }
    public GameObject enemyPrefab;

    private Vector3 enemyPos;
    public List<SkeletonControl> enemyList = new List<SkeletonControl>();

    private void Awake()
    {
        Instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 生成怪物的方法，作为按键事件使用。
    /// </summary>
    public void Born()
    {
        if (enemyList.Count > 0)
        {
            return;
        }
        else
        {
            enemyPos = new Vector3(3.0f, -0.25f, 0);
            GameObject go = Instantiate(enemyPrefab, enemyPos, Quaternion.identity);
            enemyList.Add(go.GetComponent<SkeletonControl>());
        }
    }

    /// <summary>
    /// 获得怪物LIST中的第一个对象。
    /// </summary>
    /// <returns></returns>
    public SkeletonControl getEnemy()
    {
        if (enemyList.Count == 0)
        {
            return null;
        }
        return enemyList[0];
    }
}
