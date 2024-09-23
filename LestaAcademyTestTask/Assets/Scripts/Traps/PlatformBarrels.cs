using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBarrels : MonoBehaviour
{
    readonly private float[] angles = { 0f, 90f, 180f, 270f };

    private void Start()
    {
        int rand_index = Random.Range(0, angles.Length);
        Vector3 rand_vector = new (0f, angles[rand_index], 0f);
        transform.eulerAngles = rand_vector;
    }
}
