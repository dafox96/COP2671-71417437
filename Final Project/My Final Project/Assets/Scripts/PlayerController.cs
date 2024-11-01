using System;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float turnSpeed =90.0f;
    private float turnInput, forwardInput;
    public float xEdge, zEdge;
    public AudioClip fireSound;
    private AudioSource playerAudio;

    public GameObject projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        turnInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * speed * forwardInput * Time.deltaTime);
        transform.Rotate(Vector3.up, turnSpeed * turnInput * Time.deltaTime);

        if(Math.Abs(transform.position.z) > zEdge)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -transform.position.z);
        }
        if (Math.Abs(transform.position.x) > xEdge)
        {
            transform.position = new Vector3(-transform.position.x, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectilePrefab, transform.position, transform.rotation);
            playerAudio.PlayOneShot(fireSound, 1.0f);
        }
    }
}
