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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engine);
            }
            
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
       }
       else if (audioSource.isPlaying)
       {
            audioSource.Stop();
       }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float frame)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * frame * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }

}
