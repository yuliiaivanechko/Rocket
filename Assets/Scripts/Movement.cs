using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody; 
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 50f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>(); 
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
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
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
