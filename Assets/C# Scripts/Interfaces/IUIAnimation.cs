using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//决定一个UI是否加入其主面板的动画流程中
public interface IUIAnimation
{
    float Delay { get; }
    void EnterUIAnimateAction();
    void ExitUIAnimateAction();
}
