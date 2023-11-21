using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected EnemyDetailsSO enemyDetailsSo;

    #region Header Attack Check

    [Space(10)]
    [Header("Attack Check")]

    #endregion
    [SerializeField] private Transform attackCheckPos;
    [SerializeField] private Vector3 attackCheckSize;
    [SerializeField] private LayerMask attackLayer;

    #region Header Material

    [Space(10)]
    [Header("Material")]

    #endregion
    [SerializeField] private Material hitMaterial;
    [SerializeField] private Material defaultMaterial;

    protected Collider2D target;
    
    private float maxHealth;
    private float curHealth;
    private float moveSpeed;
    private float attackDamage;

    protected bool canMove = true;
    protected bool isAttacking = false;
    protected bool attackEnd = true;
    protected bool isMoving = false;

    private Animator anim;
    private WaitForSeconds hitDelay;
    private SpriteRenderer sr;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        hitDelay = new WaitForSeconds(0.1f);
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        InitEnemy();
    }

    private void OnEnable()
    {
        canMove = true;
    }

    private void Update()
    {
        MoveUpdate();
        AttackUpdate();
        AnimationUpdate();
    }

    private void FixedUpdate()
    {
        AttackCheck();
    }

    private void MoveUpdate()
    {
        if (!canMove)
        {
            return;
        }
        
        if(isMoving)
        {
            transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
        }
    }
    
    private void AttackCheck()
    {
        target = Physics2D.OverlapBox(attackCheckPos.position, attackCheckSize, 0, attackLayer);

        if (target != null)
        {
            canMove = false;
            isAttacking = true;
        }
        else
        {
            canMove = true;
            isAttacking = false;
        }
    }

    protected virtual void AttackUpdate()
    {
        if (!isAttacking)
        {
            return;
        }

        if (attackEnd)
        {
            attackEnd = false;
            anim.SetTrigger("Attack");
        }
    }

    private void AnimationUpdate()
    {
        anim.SetBool("isWalk", canMove);
    }
    
    private void InitEnemy()
    {
        maxHealth = enemyDetailsSo.enemyBaseHealth;
        moveSpeed = enemyDetailsSo.enemyBaseMoveSpeed;
        attackDamage = enemyDetailsSo.enemyBaseAttackDamage;

        curHealth = maxHealth;
    }

    #region 애니메이션 이벤트에서 사용할 함수들

    private void AttackEnd()
    {
        attackEnd = true;
    }
    
    private void Moving()
    {
        isMoving = true;
    }
    
    private void MovingStop()
    {
        isMoving = false;
    }
    
    private void AttackDamage()
    {
        target.GetComponent<RacoonBase>().TakeDamage(Mathf.CeilToInt(attackDamage));
    }
    
    #endregion
    
    public void OnDamage(float damage)
    {
        curHealth = Mathf.Max(0, curHealth - damage);

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }

        StartCoroutine(HitRoutine());
    }
    
    private IEnumerator HitRoutine()
    {
        sr.material = hitMaterial;

        yield return hitDelay;

        sr.material = defaultMaterial;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireCube(attackCheckPos.position, attackCheckSize);
    }
}