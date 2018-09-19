using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class BagTestWindow : Window
{
    private GList list;
    private GButton selectButton;

    protected override void OnInit()
    {
        this.contentPane = UIPackage.CreateObject("BagTest", "BagWindow").asCom;
        selectButton = contentPane.GetChild("n5").asButton;

        this.Center();
        this.modal = true;

        list = contentPane.GetChild("list").asList;
        list.itemRenderer = ItemRenderer;
        list.numItems = 50;
        list.onClickItem.Add(OnClickItem);
    }

    private void OnClickItem(EventContext context)
    {
        GButton button = (GButton) context.data;

        selectButton.icon = button.icon;
        selectButton.title = button.title;
    }

    private void ItemRenderer(int index, GObject obj)
    {
        GButton button = (GButton) obj;
        int i = index % 10;
        button.icon = UIPackage.GetItemURL("BagTest", "i" + i);
        button.title = index + "";
    }

    protected override void DoShowAnimation()
    {
        this.SetScale(0.8f, 0.8f);
        this.SetPivot(0.5f, 0.5f);
        this.TweenScale(new Vector2(1, 1), 0.1f).OnComplete(this.OnShown);
    }

    protected override void DoHideAnimation()
    {
        this.TweenScale(new Vector2(0.8f, 0.8f), 0.1f).OnComplete(this.HideImmediately);
    }

    protected override void OnShown()
    {
        base.OnShown();
        Debug.Log("开始动画");
    }

//    protected override void HideImmediately()
//    {
//        base.HideImmediately();
//        Debug.Log("隐藏动画");
//    }
}