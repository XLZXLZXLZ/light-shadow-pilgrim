using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 当一个机关允许玩家踩在上面时，可以重复执行其效果时，实现该接口
/// </summary>
public interface IRepeatTriggerable
{
    void RepeatTrigger();
}
