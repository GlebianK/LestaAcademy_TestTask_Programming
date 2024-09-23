using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

public class PlatformWind : MonoBehaviour
{
    [SerializeField] private float strength;

    private Vector3 direction;
    private Rigidbody target;

    private void Start()
    {
        direction = Vector3.zero;
        StartCoroutine(ChangeBlowDirection());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            col.gameObject.TryGetComponent<Rigidbody>(out target);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (target != null)
        {
            target.AddForce(direction * strength, ForceMode.Impulse);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            target = null;
        }
    }

    private IEnumerator ChangeBlowDirection()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            float x = Random.Range(-1f, 1f);
            float z = Random.Range(-1f, 1f);
            direction = new Vector3(x, 0f, z);
            yield return null;
        }
    }
}
