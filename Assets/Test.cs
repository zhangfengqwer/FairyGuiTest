using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
	    GRoot.inst.SetContentScaleFactor(800, 600);

	    GComponent mainView = GetComponent<UIPanel>().ui;
	    GObject gObject = mainView.GetChild("n0");


	    gObject.onTouchBegin.Add(OnTouchBegin);
	    gObject.onTouchMove.Add(OnTouchMove);
	    gObject.onTouchEnd.Add(OnTouchEnd);
	}

    private void OnTouchEnd(EventContext context)
    {
        print("OnTouchEnd");
    }

    private void OnTouchMove(EventContext context)
    {
        print("OnTouchMove");
    }

    private void OnTouchBegin(EventContext context)
    {
        print("OnTouchBegin");
    }

    // Update is called once per frame
	void Update () {
		
	}
}
