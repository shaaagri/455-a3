using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

/*
    This script build on top of FPSController to add game-specific things.
*/

public class PlayerController : MonoBehaviour
{
    [SerializeField] AudioSource breathingSound;
    [SerializeField] AudioSource oxygenTankPickupSound;
    [SerializeField] AudioSource fallingSound;
    [SerializeField] AudioSource chokingSound;
    [SerializeField] AudioSource backgroundMusic;
    [SerializeField] AudioSource travellingSound;
    [SerializeField] Animator blackScreen;
    [SerializeField] GameObject redScreen;
    [SerializeField] GameObject oxygenLevelWidget;
    [SerializeField] GameObject gameOverTitle;

    FirstPersonController firstPersonController;
    PlayerOxygenController oxygenController;
    
    bool choked = false;
    bool died = false;

    // Start is called before the first frame update
    void Start()
    {
        firstPersonController = GetComponent<FirstPersonController>();
        oxygenController = GetComponent<PlayerOxygenController>();
        breathingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!choked && oxygenController.oxygen <= 0.01f) {
            Choke();   
            choked = true;
            blackScreen.SetBool("death", true);  
        }

        string blackScreenCurrentClip = blackScreen.GetCurrentAnimatorClipInfo(0)[0].clip.name;

        if (!died && blackScreenCurrentClip == "Death") {
            Destroy(oxygenLevelWidget);
            breathingSound.Stop();
            died = true;
        }

        if (blackScreenCurrentClip == "GameOver") {
            gameOverTitle.SetActive(true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "FallingScreamTrigger") {
            fallingSound.Play();
        }  

        if (other.name == "FallingGameOverTrigger") {
            if (!choked) {
                blackScreen.SetBool("fadeOut", true);
            }    
        }

        if (other.name == "RemoveHelpersTrigger") {
            Destroy(GameObject.Find("helper_objective"));
            Destroy(GameObject.Find("helper_spitter"));
            Destroy(GameObject.Find("helper_tank"));
        }

        if (other.name == "puncture_portal") {
            redScreen.SetActive(true);
            firstPersonController.LockControls(true);
            breathingSound.Stop();
            backgroundMusic.Stop();
            travellingSound.Play();
        }
    }

    public void PowerUpOxygen() {
        oxygenTankPickupSound.Play();
        oxygenController.AddOxygen(70.0f);
    }

    void Choke() {
        chokingSound.Play();
        firstPersonController.LockControls(true);
    }

    // In GUI mode mouse and keyboard are disabled and the mouse cursor is visible
    public void SetGUIMode(bool value) {
        if (value) {
            firstPersonController.LockControls(true);
            firstPersonController.LockCursor(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

        }
        else {
            firstPersonController.LockControls(false);
            firstPersonController.LockCursor(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    // Mostly to support the GUI mode
    public void PauseGameplay(bool value) {
        if (value) {
            oxygenController.disabled = true;
        }
        else {
            oxygenController.disabled = false;
        }      
    }
}
