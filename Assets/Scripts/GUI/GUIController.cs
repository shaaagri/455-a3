using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
    This script is GUI barebones.
*/

public class GUIController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject oxygenLevelWidget;
    [SerializeField] GameObject narrativeScreen;
    [SerializeField] Button btnNarrativeScreenClose;
    [SerializeField] AudioSource sndButtonBeep;
    [SerializeField] Animator blackScreen;

    GUIState state = GUIState.UNDEFINED;
    bool stateChanged = false;

    // Start is called before the first frame update
    void Start()
    {
        btnNarrativeScreenClose.onClick.AddListener(closeNarrativeScreen);
        changeState(GUIState.NARRATIVE_SCREEN);
    }

    // Update is called once per frame
    void Update()
    {
        // update things only when the state has changed, to save cpu power
        if (stateChanged) {
            switch (state) {
                case GUIState.NARRATIVE_SCREEN:
                    playerController.SetGUIMode(true);
                    playerController.PauseGameplay(true);
                    oxygenLevelWidget.SetActive(false);
                    narrativeScreen.SetActive(true);
                    blackScreen.SetBool("narrativeScreenOverlay", true);
                    break;

                case GUIState.GAMEPLAY:
                    playerController.SetGUIMode(false);
                    playerController.PauseGameplay(false);
                    oxygenLevelWidget.SetActive(true);
                    narrativeScreen.SetActive(false);
                    blackScreen.SetBool("narrativeScreenOverlay", false);
                    break;

                default:
                    break;
            }

            stateChanged = false;
        } 
    }

    void changeState(GUIState value) {
        state = value;
        stateChanged = true;
    }

    void closeNarrativeScreen() {
        sndButtonBeep.Play();
        changeState(GUIState.GAMEPLAY);
    }
}
