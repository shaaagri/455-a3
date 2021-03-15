using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script is responsible for handling the player's oxygen level.
*/

public class PlayerOxygenController : MonoBehaviour
{
    public float oxygen = 100.0f;   // percentage (min = 0.0, max = 100.0)
    public float decreaseRate = 0.5f;     // pct. per second

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float frameDurationCoef = (Time.deltaTime / 1.0f);  

        // the oxygen depletion mechanic
        if (oxygen > 0.0f) {
            oxygen -= frameDurationCoef * decreaseRate; 
        }
    }

    public void AddOxygen(float value) {
        oxygen += value;
        if (oxygen > 100.0f) {
            oxygen = 100.0f;
        }
    }
}
