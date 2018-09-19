using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class ScrollList : MonoBehaviour {
    private UIPanel uiPanel;
    private GComponent ui;
    private GList list;

    // Use this for initialization
	void Start ()
	{
	    UIPackage.AddPackage("ScrollList");

        uiPanel = GetComponent<UIPanel>();
	    ui = uiPanel.ui;
	    list = ui.GetChild("list").asList;
	    list.SetVirtualAndLoop();
        list.itemRenderer = ItemRenderer;
	    list.numItems = 5;

        list.scrollPane.onScroll.Add(DoSpecialEffect);
	    DoSpecialEffect();
	   
	}

    private void DoSpecialEffect()
    {
        float midX = list.scrollPane.posX + list.viewWidth / 2;
        int num = list.numChildren;

        for (int i = 0; i < num; i++)
        {
            GObject obj = list.GetChildAt(i);
            float itemCenter = obj.x + obj.width / 2;
            float distance = Mathf.Abs(midX - itemCenter);

            if (distance > obj.width)
            {
                obj.SetScale(1, 1);

            }
            else
            {
                float scale = 1 + (1 - distance / obj.width)  * 0.2f;
                obj.SetScale(scale, scale);
            }
        }
    }

    private void ItemRenderer(int index, GObject item)
    {
        GButton button = (GButton) item;
        button.SetPivot(0.5f, 0.5f);
        button.icon = UIPackage.GetItemURL("ScrollList", "n" + (index + 1));
    }

    private void OnClick(int index)
    {
        print(index);
    }

    // Update is called once per frame
	void Update () {
		
	}
}
