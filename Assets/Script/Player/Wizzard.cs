using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizzard : RacoonBase
{
    [Header("# Attack Var")]
    [SerializeField] private float attDelayTime = 0;
    [SerializeField] private float coolTime = 0;
    [SerializeField] private GameObject magicObj = null;
    [SerializeField] private GameObject magicPos = null;

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
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                isFindEnemy = true;
                R_anim.SetBool("Run", false);

                // Attack
                if (attDelayTime >= coolTime)
                {
                    hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(R_att);

                    R_anim.SetTrigger("Attack");
                    Instantiate(magicObj, magicPos.transform.position, Quaternion.identity);

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
