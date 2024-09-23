using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformMine : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float reactTime = 0.5f;
    [SerializeField] private AudioSourceController asc;
    [SerializeField] private MeshRenderer mr;
    [SerializeField] private Material[] materials; // 3 элемента: нормальный, зар€женный, нанести урон

    private bool readyToBoom;
    private bool isInTouch;

    // Start is called before the first frame update
    void Start()
    {
        ChangeMaterial(materials[0]);
        readyToBoom = true;
        isInTouch = false;
    }

    private void OnCollisionEnter(Collision col)
    {
        isInTouch = true;
        if (readyToBoom)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                ChangeMaterial(materials[1]);
                col.gameObject.TryGetComponent<Health>(out Health health);
                StartCoroutine(CountdownToBurst(health));
            }
        }
    }

    private void OnCollisionStay(Collision col)
    {
        if (readyToBoom)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                ChangeMaterial(materials[1]);
                col.gameObject.TryGetComponent<Health>(out Health health);
                StartCoroutine(CountdownToBurst(health));
            }
        } 
    }

    private void OnCollisionExit(Collision col)
    {
        isInTouch = false;
    }

    private void ChangeMaterial(Material mat)
    {
        mr.material = mat;
    }

    private IEnumerator CountdownToBurst(Health health)
    {
        readyToBoom = false;
        yield return new WaitForSeconds(reactTime);
        ChangeMaterial(materials[2]);

        if (isInTouch)
        {
            health.TakeDamage(damage);
        }

        asc.PlayClip("Bang");

        StartCoroutine(Cooldown());
        yield return null;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.15f);
        ChangeMaterial(materials[0]);
        yield return new WaitForSeconds(5);
        readyToBoom = true;
    }
}
