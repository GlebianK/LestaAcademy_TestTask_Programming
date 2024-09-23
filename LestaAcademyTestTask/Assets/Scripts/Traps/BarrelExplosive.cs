using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelExplosive : MonoBehaviour
{
    [SerializeField] private Collider barrelCollider;
    [SerializeField] private MeshRenderer mr;
    [SerializeField] private AudioSourceController asc;
    [SerializeField] private float damage;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            barrelCollider.enabled = false;
            mr.enabled = false;
            col.gameObject.TryGetComponent<Health>(out Health health);
            StartCoroutine(HitAndDestroy(health));
        }
    }

    private IEnumerator HitAndDestroy(Health health)
    {
        asc.PlayClip("Bang");
        health.TakeDamage(damage);
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }
}
