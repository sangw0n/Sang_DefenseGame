using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 0;
    [SerializeField] private int bulletAttack = 0;
    
    private Rigidbody2D rigid;
    
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        BulletMove();
    }

    private void BulletMove()
    {
        rigid.velocity = Vector2.right * bulletSpeed;
    }

    private void OnTriggerEnter2D(Collider2D Trigger) 
    {
        if(Trigger.gameObject.CompareTag("Enemy"))
        {
            Trigger.gameObject.GetComponent<Enemy>().TakeDamage(bulletAttack);
            Destroy(this.gameObject);
        }
    }
}
