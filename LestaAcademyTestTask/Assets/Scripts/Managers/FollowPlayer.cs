using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform target;
    [SerializeField] private float yOffset = -10;
    [SerializeField] private float zOffset = -10;

    private Vector3 temp_v;

    // Start is called before the first frame update
    void Start()
    {
        UpdatePosition();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        temp_v = new Vector3(target.position.x, target.position.y + yOffset, target.position.z + zOffset);
        cam.position = temp_v;
    }
}
