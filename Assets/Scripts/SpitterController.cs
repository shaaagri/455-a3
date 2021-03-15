using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script defines the behaviour of "spitters" - the round blue-ish trampolines
    that allow player to jump to the asteroids that float further above.
*/

public class SpitterController : MonoBehaviour
{
    public float recuperationTime = 2.0f;

    AudioSource soundCatapult;
    Light emanator;
    float emanatorDefaultIntensity;
    AudioSource soundHum;
    float humDefaultVolume;
    bool recuperating = false;

    // Start is called before the first frame update
    void Start()
    {
        soundCatapult = GetComponent<AudioSource>();  
        emanator = transform.Find("Emanator").gameObject.GetComponent<Light>();
        emanatorDefaultIntensity = emanator.intensity;
        soundHum = transform.Find("SFXHum").gameObject.GetComponent<AudioSource>();
        humDefaultVolume = soundHum.volume;
    }

    // Update is called once per frame
    void Update()
    {
        /* After rocketing the player into space, the spitter stops working for 
        a while, in order to recharge */
        if (recuperating) {
            float frameDurationCoef = (Time.deltaTime / recuperationTime);
            emanator.intensity += frameDurationCoef * emanatorDefaultIntensity;
            soundHum.volume += frameDurationCoef * humDefaultVolume;

            if (emanator.intensity >= emanatorDefaultIntensity) {
                recuperating = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (recuperating) {
            return;
        }

        if (other.name == "FPSController") {
            soundCatapult.Play();
            other.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().Catapult(100.0f);
            emanator.intensity = 0;
            soundHum.volume = 0;
            recuperating = true;
        }    
    }
}
