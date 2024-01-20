using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : Singleton<EventManager>
{
    //protected override bool IsDontDestroyOnLoad => true;

    public UnityAction<PathNode> OnClickNode; //选定某一结点时

    public UnityAction OnMapUpdate; //当输入一个地图更新时

    public UnityAction<PathNode> OnMoveToNewNode; //当玩家移动到一个新结点时

    public UnityAction OnMapGenerated; //地图生成动画完毕时
}
