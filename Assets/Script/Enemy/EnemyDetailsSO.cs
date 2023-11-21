using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDetails_", menuName = "Scriptable Objects/Enemy/EnemyDetails")]
public class EnemyDetailsSO : ScriptableObject
{
    #region Header

    [Header("기본 스탯")]

    #endregion

    public float enemyBaseHealth;

    public float enemyBaseMoveSpeed;

    public float enemyBaseAttackDamage;
}