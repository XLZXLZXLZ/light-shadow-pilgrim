using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent (typeof (Player))]
public class MoveController : Singleton<MoveController>
{
    private Player player;
    private List<PathNode> strategy = new(); //当前移动策略
    public List<PathNode> Strategy
    {
        get { return strategy; }
        set { strategy = value; }
    }

    [SerializeField]
    private PathNode currentNode; //当前处在的结点
    public PathNode CurrentNode => currentNode; //外界只读
    private PathNode nextNode => strategy[0]; //下一访问结点
    private bool CanMove => player.CanMove;
    private float Speed => player.Speed;

    protected override void Awake()
    {
        base.Awake();
        EventManager.Instance.OnClickNode += UpdateStrategy;
        EventManager.Instance.MapUpdate.OnStart += OnMapUpdateStart;
        EventManager.Instance.MapUpdate.OnFinished += OnMapUpdateFinished; //开始时先触发一下脚下的扳机
        
        player = GetComponent<Player>();

    }

    private void FixedUpdate()
    {
        if (strategy.Count > 0) //列表中还有下一结点，尝试移动向下一节点
        {
            MoveTo(transform.position, nextNode.pos, Speed);
            if ((transform.position - nextNode.pos).sqrMagnitude < 0.001f) //到达目标点，更新结点表和当前节点
                UpdateCurrentNode();
        }
        else //否则，持续靠向currentNode
        {
            MoveTo(transform.position, currentNode.pos, Speed);
        }
    }

    private void MoveTo(Vector3 origin,Vector3 target,float speed)
    {
        transform.position = Vector3.MoveTowards(origin, target, speed * Time.fixedDeltaTime);
    }

    private void UpdateCurrentNode() //更新当前处于的结点
    {
        TriggerNodeOver();

        currentNode = strategy[0];
        strategy.RemoveAt(0);
        
        // 结束更新完节点后，如果没有策略，那么就说明移动完成
        if(strategy.Count == 0)
            EventManager.Instance.OnPlayerMoveFinished.Invoke();

        TriggerNode();

        transform.parent = currentNode.transform;
        EventManager.Instance.OnMoveToNewNode?.Invoke(currentNode);
    }

    private void UpdateStrategy(PathNode node) //点按时，检查路径是否可用，可用时，更新策略
    {
        if (!CanMove) //不可移动时直接退出
            return;
        if(AStar.Instance.FindPath(currentNode,node,out var path))
            strategy = path;
        else
            strategy.Clear();
    }

    private void TriggerNode()
    {
        var triggers = currentNode.GetComponents<ITriggerable>();
        if (triggers.Length > 0)
            foreach (var t in triggers)
                t.OnTrigger();
    }

    private void TriggerNodeOver()
    {
        var triggers = currentNode.GetComponents<ITriggerable>();
        if (triggers.Length > 0)
            foreach (var t in triggers)
                t.OnTriggerOver();
    }

    private void OnMapUpdateStart()
    {
        strategy.Clear();
    }

    private void OnMapUpdateFinished()
    {
        TriggerNode();
        if(currentNode.LightState != GameManager.Instance.CurrentPlayerState)
            EventManager.Instance.OnPlayerLightStateChangedStart.Invoke(currentNode.LightState);
    }
}
