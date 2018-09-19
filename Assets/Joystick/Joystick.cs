using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class Joystick : EventDispatcher
{
    public EventListener onMove { get; private set; }
    public EventListener onEnd { get; private set; }

    private GComponent mainView;
    private GObject touchArea;
    private GObject center;
    private GButton joystickButton;
    private GObject thumb;

    //中心点初始位置
    private float initX;
    private float inity;
    /// <summary>
    /// 摇杆距离中心的位置
    /// </summary>
    private int radius = 150;
    private int touchId;
    private float startStageY;
    private float startStageX;
    private GTweener tweener;
    public Joystick(GComponent mainView)
    {
        onMove = new EventListener(this, "onMove");
        onEnd = new EventListener(this, "onEnd");

        this.mainView = mainView;
        touchArea = mainView.GetChild("touchArea");
        center = mainView.GetChild("center");
        joystickButton = mainView.GetChild("joystick").asButton;
        joystickButton.changeStateOnClick = false;
        thumb = joystickButton.GetChild("thumb");

        initX = center.x + center.width / 2;
        inity = center.y + center.height / 2;
        touchId = -1;

        //添加移动事件
        touchArea.onTouchBegin.Add(OnTouchBegin);
        touchArea.onTouchMove.Add(OnTouchMove);
        touchArea.onTouchEnd.Add(OnTouchEnd);
    }

    private void OnTouchBegin(EventContext context)
    {
        //第一次调用
        if (touchId == -1)
        {
            if (tweener != null)
            {
                tweener.Kill();
                tweener = null;
            }

            InputEvent inputEvent = (InputEvent)context.data;
            touchId = inputEvent.touchId;

            Vector2 localPos = GRoot.inst.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
            center.SetXY(localPos.x - center.width / 2, localPos.y - center.height / 2);
            joystickButton.SetXY(localPos.x - joystickButton.width / 2, localPos.y - joystickButton.height / 2);
            joystickButton.selected = true;
            center.visible = true;

            startStageX = localPos.x;
            startStageY = localPos.y;

            float deltaX = localPos.x - initX;
            float deltaY = localPos.y - inity;

            float degress = Mathf.Atan2(deltaY, deltaX) * Mathf.Rad2Deg;
            thumb.rotation = degress + 90;
            //调用后，后续才会触发OnTouchMove
            context.CaptureTouch();
        }
        
    }

    private void OnTouchMove(EventContext context)
    {
        InputEvent inputEvent = (InputEvent)context.data;
        if (touchId != -1 && touchId == inputEvent.touchId)
        {
            Vector2 localPos = GRoot.inst.GlobalToLocal(new Vector2(inputEvent.x, inputEvent.y));
            float buttonX = localPos.x;
            float buttonY = localPos.y;

            //点击距离开始点的偏移
            float deltaX = buttonX - startStageX;
            float deltaY = buttonY - startStageY;

            float radians = Mathf.Atan2(deltaY, deltaX);
            float degress = radians * Mathf.Rad2Deg;

            float maxX = radius * Mathf.Cos(radians);
            float maxY = radius * Mathf.Sin(radians);

            if (Mathf.Abs(deltaX) > Mathf.Abs(maxX))
            {
                deltaX =  maxX;
            }
            if (Mathf.Abs(deltaY) > Mathf.Abs(maxY))
            {
                deltaY =  maxY;
            }

            buttonX = deltaX + startStageX;
            buttonY = deltaY + startStageY;
            joystickButton.SetXY(buttonX - joystickButton.width / 2, buttonY - joystickButton.height / 2);
            thumb.rotation = degress + 90;
            onMove.Call(thumb.rotation);
        }
    }

    private void OnTouchEnd(EventContext context)
    {
        InputEvent inputEvent = (InputEvent)context.data;
        if (touchId != -1 && touchId == inputEvent.touchId)
        {
            touchId = -1;
            center.SetXY(initX - center.width / 2, inity - center.height / 2);
            center.visible = false;
            thumb.rotation += 180;
            tweener = joystickButton.TweenMove(new Vector2(initX - joystickButton.width / 2, inity - joystickButton.height / 2), 0.3f).OnComplete(() =>
            {
                center.visible = true;
                joystickButton.selected = false;
                thumb.rotation = 0;
                onEnd.Call();
            });
        }
    }
}
