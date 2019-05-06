using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaiviour : MonoBehaviour
{

    // Configuration parameters
    [SerializeField] PaddleController paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;

    // State
    Vector2 paddleToBallVector;
    bool hasStarted = false;


    // Cached component references
    AudioSource ballAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (paddle1 != null)
        {
            paddleToBallVector = transform.position - paddle1.transform.position;
        }
        ballAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            if (paddle1 != null) {
                LockBallToPaddle();
                LaunchOnMouseClick();
            }
            else
            {
                hasStarted = true;
                GetComponent<Rigidbody2D>().velocity = new Vector2(xPush,yPush);
            }
        }
    }

    private void LockBallToPaddle()
    {

        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePosition + paddleToBallVector;
    }
    
    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush,yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasStarted)
        {
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            ballAudioSource.PlayOneShot(clip);      // Play the whole way though, and not be interupted
        }
    }
}
