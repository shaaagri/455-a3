using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script defines rendering for sprites, particularly, the "billboarding" 
    (always facing the camera).

    An example of usage can be seen in the oxygen tank power-up.
*/

/* CITATION */
/* used help from https://stackoverflow.com/questions/30416807/have-2d-sprite-face-3d-camera */

public class GenericSpriteController : MonoBehaviour
{
    Camera cameraToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        cameraToLookAt = GameObject.Find("FirstPersonCharacter").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (cameraToLookAt != null) {
            transform.LookAt(cameraToLookAt.transform);  
        }  
    }
}
