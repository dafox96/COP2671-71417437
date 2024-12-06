using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float acceleration;
    private Rigidbody enemyRb;
    private float xEdge = 125;
    private float zEdge = 100;
    public float speed;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Math.Abs(transform.position.z) > zEdge || Math.Abs(transform.position.x) > xEdge)
        {
            Destroy(gameObject);
        }
        if (gameManager.gameActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameManager.GameOver(false);
        }
        else
        {
            gameManager.updateScore(gameManager.waveNumber);
        }
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}