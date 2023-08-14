using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstucle : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField]
    float moveForce = 10f;
    [SerializeField] Vector3 rotation;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        rb.AddForce(Vector3.left * moveForce, ForceMode.VelocityChange);

        Destroy(gameObject, 5f);

    }

    private void Update()
    {
        transform.Rotate(rotation * Random.Range(0f, 2f) * Time.deltaTime);
    }
}
