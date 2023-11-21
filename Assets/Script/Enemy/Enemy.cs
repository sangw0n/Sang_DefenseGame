using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int Hp;
    [SerializeField] private float enemyMoveSpeed;

    private bool isMove = true;

    private Rigidbody2D rigid;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PlayerCheck();
        EnemyMove();
    }

    private void PlayerCheck()
    {
        Debug.DrawRay(rigid.position, Vector2.left * 0.75f, Color.blue);
        RaycastHit2D hit = Physics2D.Raycast(rigid.position, Vector2.left, 0.75f, LayerMask.GetMask("Racoon"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                isMove = false;
            }
        }
        else isMove = true;
    }

    private void EnemyMove()
    {
        if (!isMove) return;
        else if (isMove)
        {
            transform.position += Vector3.left * enemyMoveSpeed * Time.deltaTime;

        }
    }
    public void TakeDamage(int value)
    {
        Hp = Hp - value;
        if (Hp <= 0) Destroy(this.gameObject);
    }
}
