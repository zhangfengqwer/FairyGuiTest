using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class BagTest : MonoBehaviour {
    private GComponent mainView;

    // Use this for initialization
	void Start ()
	{
	    //UIPackage.AddPackage("BagTest");
	    GRoot.inst.SetContentScaleFactor(800, 600);

        mainView = GetComponent<UIPanel>().ui;
	    BagTestWindow bagTestWindow = new BagTestWindow();
	    mainView.GetChild("n0").onClick.Add(() =>
	    {
	        if (bagTestWindow.isShowing)
	        {
	            bagTestWindow.Hide();
            }
	        else
	        {
	            bagTestWindow.Show();
            }
        });
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
