using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class JoystickTest : MonoBehaviour {

    private GComponent mainView;
    private GTextField degressText;

    // Use this for initialization
	void Start ()
	{
	    GRoot.inst.SetContentScaleFactor(800, 600);

	    mainView = GetComponent<UIPanel>().ui;
	    degressText = mainView.GetChild("n4").asTextField;
	    Joystick joystick = new Joystick(mainView);

	    joystick.onEnd.Add(OnEnd);
	    joystick.onMove.Add(OnMove);
	}

    private void OnMove(EventContext context)
    {
        float degress = (float)context.data;
        degressText.text = degress + "";
    }

    private void OnEnd(EventContext context)
    {
        degressText.text = "";
    }
}
