using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : RacoonBase
{
    [Header("# Attack Var")]
    [SerializeField] private float attDelayTime = 0;
    [SerializeField] private float coolTime = 0;
    [SerializeField] private Transform attBoxPos = null;
    [SerializeField] private Vector2 attBoxSize = new Vector2(0, 0);

    [Header("# RayCast Var")]
    [SerializeField] private float rayLength;

    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
        Attack();
    }

    private void Attack()
    {
        EnemyCheck();

        //OverlapBoxAll(point(박스의 생성위치), size(박스의 크기), angle(박스의 회전))
        Collider2D[] attCollider = Physics2D.OverlapBoxAll(attBoxPos.position, attBoxSize, 0);
        foreach (Collider2D collider in attCollider)
        {
            if (collider.gameObject.CompareTag("Enemy"))
            {
                if (attDelayTime >= coolTime)
                {
                    R_anim.SetTrigger("Attack");
                    attDelayTime = 0;
                    collider.gameObject.GetComponent<Enemy>().TakeDamage(R_att);
                }
                else if (attDelayTime <= coolTime) attDelayTime += Time.deltaTime;
            }
        }
    }

    private void EnemyCheck()
    {
        // 몬스터 사거리에 들어왔는지 체크하는 코드 
        Debug.DrawRay(R_rigid.position, Vector2.right * rayLength, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(R_rigid.position, Vector2.right, rayLength, LayerMask.GetMask("Enemy"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                isFindEnemy = true;
                R_anim.SetBool("Run", false);
            }
        }
        else if (hit.collider == null)
        {
            isFindEnemy = false;
            R_anim.SetBool("Run", true);
        }
    }

    // attCollider 표시
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attBoxPos.position, attBoxSize);
    }
}
