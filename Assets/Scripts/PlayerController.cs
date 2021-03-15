using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerOxygenController oxygenController;

    [SerializeField] AudioSource breathingSound;
    [SerializeField] AudioSource oxygenTankPickupSound;
    [SerializeField] AudioSource fallingSound;
    [SerializeField] Animator blackScreen;

    // Start is called before the first frame update
    void Start()
    {
        oxygenController = GetComponent<PlayerOxygenController>();
        breathingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PowerUpOxygen() {
        oxygenTankPickupSound.Play();
        oxygenController.AddOxygen(70.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "FallingScreamTrigger") {
            fallingSound.Play();
        }  

        if (other.name == "FallingGameOverTrigger") {
            blackScreen.SetBool("fadeOut", true);
        }
    }
}
