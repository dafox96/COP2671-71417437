using System;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 40.0f;
    public float xEdge, zEdge;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        if(Math.Abs(transform.position.x) > xEdge || Math.Abs(transform.position.z) > zEdge)
        {
            Destroy(gameObject);
        }
    }
}
