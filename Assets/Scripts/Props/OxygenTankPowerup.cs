using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankPowerup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FPSController") {
            PlayerController player = other.GetComponent<PlayerController>();
            player.PowerUpOxygen();
            Destroy(gameObject);
        }    
    }
}
