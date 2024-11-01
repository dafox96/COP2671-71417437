using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float acceleration;
    private Rigidbody enemyRb;
    public float xEdge, zEdge;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if (Math.Abs(transform.position.z) > zEdge)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z);
        }
        if (Math.Abs(transform.position.x) > xEdge)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}