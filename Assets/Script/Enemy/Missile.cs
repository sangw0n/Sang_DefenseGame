using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private float duration = 1.0f;
    [SerializeField] private float heightY = 3.0f;

    public void StartShot(Vector3 start, Vector2 target, Transform targetPos, float damage)
    {
        StartCoroutine(Curve(start, target, targetPos, damage));
    }

    private IEnumerator Curve(Vector3 start, Vector2 target, Transform targetPos, float damage)
    {
        float timePassed = 0f;

        Vector2 end = target;

        while (timePassed < duration)
        {
            if (Vector2.Distance(transform.position, targetPos.position) <= 0.1f && timePassed > duration / 2)
            {
                StopAllCoroutines();

                if (targetPos.GetComponent<RacoonBase>() != null)
                {
                    targetPos.GetComponent<RacoonBase>().TakeDamage(Mathf.CeilToInt(damage));
                }
                
                Destroy(gameObject);
            }
            
            timePassed += Time.deltaTime;

            float linearT = timePassed / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, heightY, heightT);

            Vector2 nextPos = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);
            Vector3 direction = transform.position - (Vector3)nextPos;
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            transform.position = nextPos;
            
            yield return null;
        }
        
        Destroy(gameObject);
    }
}