using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoliderType { Solider, Tank }

public class Solider : RacoonBase
{
    [Header("# Solider Type ")]
    [SerializeField] private SoliderType soliderType;

    [Header("# Attack Var")]
    [SerializeField] private float attDelayTime = 0;
    [SerializeField] private float coolTime = 0;
    [SerializeField] private GameObject Bullet = null; // Tank 

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
        Debug.DrawRay(R_rigid.position, Vector2.right * rayLength, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(R_rigid.position, Vector2.right, rayLength, LayerMask.GetMask("Enemy"));

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                isFindEnemy = true;
                R_anim.SetBool("Run", false);
                // Attack
                if (attDelayTime >= coolTime)
                {
                    switch (soliderType)
                    {
                        case SoliderType.Solider:
                            // 총쏘기
                            hit.collider.GetComponent<Enemy>().TakeDamage(R_att);
                            R_anim.SetTrigger("Attack");
                            break;

                        case SoliderType.Tank:
                            // 총알발사 
                            Instantiate(Bullet, transform.position, Quaternion.identity);
                            break;
                    };

                    attDelayTime = 0;
                }
                else if (attDelayTime <= coolTime) attDelayTime += Time.deltaTime;
            }
        }
        else if (hit.collider == null)
        {
            R_anim.SetBool("Run", true);
            isFindEnemy = false;
        }
    }
}
