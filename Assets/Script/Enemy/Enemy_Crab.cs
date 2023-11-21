using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Crab : EnemyBase
{
    #region Header

    [Space(10)]
    [Header("Missile Prefab")]

    #endregion
    [SerializeField] private Missile missilePrefab;
    
    private void ShotAttack()
    {
        var missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        
        missile.StartShot(transform.position, target.transform.position, target.transform, enemyDetailsSo.enemyBaseAttackDamage);
    }
}
