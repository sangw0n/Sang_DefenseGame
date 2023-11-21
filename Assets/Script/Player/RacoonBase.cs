using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RacoonBase : MonoBehaviour
{
    // R - Racoon ( 너구리 )
    [Header("# Racoon Var")]
    [SerializeField] protected int R_hp = 0;
    [SerializeField] protected float R_speed = 0;
    [SerializeField] protected int R_att = 0;

    [Header(" Racoon Bool")]
    // 적을 찾아내면 멈추는 변수
    [SerializeField] protected bool isFindEnemy = false;
    [SerializeField] protected bool isDie = false;

    protected Rigidbody2D R_rigid;
    protected Animator R_anim;

    public virtual void Start()
    {
        R_rigid = GetComponent<Rigidbody2D>();
        R_anim = GetComponent<Animator>();
    }

    public virtual void Update()
    {
        Move();
    }   

    private void Move()
    {
        if(isFindEnemy == false)
        {
            Vector2 _vec2 = Vector2.right * R_speed;
            R_rigid.velocity = _vec2;
        }
        else return;
    }

    public void TakeDamage(int value)
    {
        R_hp -= value;
        Debug.Log("플레이어 체력: " + R_hp);
    }
}
