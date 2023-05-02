using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody; 
    AudioSource audioSource;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 50f;
    [SerializeField] AudioClip engine;

    [SerializeField] ParticleSystem thrustParticles;
    [SerializeField] ParticleSystem rightParticles;
    [SerializeField] ParticleSystem leftParticles;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessRotation();
    }

    void ProcessInput()
    {
       if (Input.GetKey(KeyCode.Space))
       {    
            StartThrust();
       }
       else
       {
            StopThrust();
       }
    }

    void StopThrust()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }

    void StartThrust()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engine);
        }
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }


    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            TurnLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            TurnRight();
        }
        else
        {
            StopTurning();
        }
    }

    void TurnLeft()
    {
        ApplyRotation(rotationThrust);
        if (!leftParticles.isPlaying)
        {
            leftParticles.Play();
        }
    }

    void TurnRight()
    {
        ApplyRotation(-rotationThrust);
        if (!rightParticles.isPlaying)
        {
            rightParticles.Play();
        }
    }

    void StopTurning()
    {
        rightParticles.Stop();
        leftParticles.Stop();
    }

    void ApplyRotation(float frame)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * frame * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }

}
