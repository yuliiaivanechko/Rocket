using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    const float tau = Mathf.PI * 2;
    [SerializeField] float period = 2f;
    Vector3 startingPosition;
    [SerializeField] Vector3 movement;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon)
        {
            return;
        }
        float cycle = Time.time / period;
        float rawSinWave = Mathf.Sin(cycle * tau);
        float movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movement * movementFactor;
        transform.position = startingPosition + offset;

    }
}
