using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float speed = 10.0f;
    public float xRange = 10;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Keep player in bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-10, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(10, transform.position.y, transform.position.z);
        }

        //Horizontal input moves player horizontally
        horizontalInput = Input.GetAxis("Horizontal");
        
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        //Spacebar to throw food
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Launch food
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }
}
