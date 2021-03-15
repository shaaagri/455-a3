using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script defines the behaviour of "asteroids", particularly
    their eerie slow rotations in space.
*/

public class AsteroidController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 angVel;

    void Awake() {
        rb = GetComponent<Rigidbody>();    
    }

    /* CITATION */
    /* used help from https://docs.unity3d.com/ScriptReference/Rigidbody.MoveRotation.html */

    void Start()
    {
        angVel = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));

    }

    void FixedUpdate()
    {
        Quaternion deltaRotation = Quaternion.Euler(angVel * Time.deltaTime * 30.0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }
}
