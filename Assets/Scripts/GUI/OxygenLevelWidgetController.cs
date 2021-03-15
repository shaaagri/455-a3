using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This script makes the oxygen level part of GUI work.
*/

public class OxygenLevelWidgetController : MonoBehaviour
{
    [SerializeField] PlayerOxygenController oxygenLevelInput;
    [SerializeField] bool criticalLevel = false;

    bool criticalLevelPrev;
    GameObject indicator;     // the yellow bar that depicts the level
    RectTransform indicatorRT;  // handle to manipulate scale
    float indicatorMaxXScale;  //  the indicator's width when full
    Color32 defaultUIColor = new Color32(0x73, 0x73, 0x73, 0xFF); 
    Color32 criticalUIColor = new Color32(0xAA, 0x00, 0x00, 0xFF); 

    Animator animator;
    AudioSource sndAlarmLoop;

    // Start is called before the first frame update
    void Start()
    {
        indicator = transform.Find("Indicator").gameObject;   
        indicatorRT =  indicator.GetComponent<RectTransform>();
        indicatorMaxXScale = indicatorRT.localScale.x;
        criticalLevelPrev = criticalLevel;
        animator = GetComponent<Animator>();
        sndAlarmLoop = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // apply input
        float oxygenLevel = oxygenLevelInput.oxygen;
        float indicatorScale = (oxygenLevel / 100.0f) * indicatorMaxXScale;
        indicatorRT.localScale = new Vector3(indicatorScale, indicatorRT.localScale.y, indicatorRT.localScale.z);

        // apply low level condition
        criticalLevel = (oxygenLevel < 15.0f);

        // paint to red if the level is critical, otherwise keep it original
        // and do it only when there was a change, so no cpu power is wasted
        if (criticalLevel != criticalLevelPrev) {
            foreach (Transform child in transform) {
                Image guiElementImage = child.gameObject.GetComponent<Image>();

                if (guiElementImage != null)  {

                    if (!criticalLevel) {
                        guiElementImage.color = defaultUIColor;
                    }

                    else {
                        guiElementImage.color = criticalUIColor;
                    }         
                }    
            }

            animator.SetBool("isCritical", criticalLevel);
            criticalLevelPrev = criticalLevel;

            if (criticalLevel) {
                sndAlarmLoop.Play();
            }
            else {
                sndAlarmLoop.Stop();
            }
        } 
    }
}
