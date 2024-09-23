using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGhost : MonoBehaviour
{
    [SerializeField] private float reactTime = 0.5f;
    [SerializeField] private Collider boxCollider;
    [SerializeField] private MeshRenderer mr;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Disappear());
        }
    }

    private void Reappear()
    {
        boxCollider.enabled = true;
        mr.enabled = true;
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(reactTime);
        boxCollider.enabled = false;
        mr.enabled = false;
        yield return new WaitForSeconds(3f);
        Reappear();
        yield return null;
    }
}
