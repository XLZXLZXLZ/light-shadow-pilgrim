using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(LightExtension))]
public class PlatformNode : PathNode, IInteractable
{
    private float searchRadius = 1.1f;
    private LightExtension lightComponent;
    private LightState lightState => lightComponent.LightState;

    //可达性判定，当玩家尝试寻路时在此判断是否可达，尝试获取实现了额外接口的脚本，若存在则还要执行它的规则判定
    public override bool ReachAble(LightState inputState)
    {
        return inputState == lightState;
    }

    //初始更新邻居结点状态
    private void Awake()
    {
        lightComponent = GetComponent<LightExtension>();
        UpdateNeighbors();

        EventManager.Instance.MapUpdate.OnFinished += UpdateNeighbors;
    }

    private void Update()
    {
        UpdateNeighbors();
    }

    //交互时以该点为目标路径唤起事件
    public void OnInteract()
    {
        EventManager.Instance.OnClickNode?.Invoke(this);
    }

    //自动搜索附近的结点并尝试连接
    private void UpdateNeighbors()
    {
        UpdatePlatform();
        //UpdateStep();
    }

    private void UpdatePlatform()
    {
        var nodes = Physics.OverlapSphere(transform.position, searchRadius, LayerMask.GetMask("Node"));

        foreach (var node in nodes)
        {
            var n = node.GetComponent<PlatformNode>();
            if (n == this || n == null) continue;

            var dir = transform.position - n.pos;
            dir.Normalize();

            if (Vector3.Dot(dir, Vector3.right) >= 0.95f)
            {
                right = n;
            }
            if (Vector3.Dot(dir, Vector3.left) >= 0.95f)
            {
                left = n;
            }
            if (Vector3.Dot(dir, Vector3.forward) >= 0.95f)
            {
                up = n;
            }
            if (Vector3.Dot(dir, Vector3.back) >= 0.95f)
            {
                down = n;
            }
        }

        if (left != null && Vector3.Distance(pos, left.pos) > searchRadius)
            left = null;
        if (right != null && Vector3.Distance(pos, right.pos) > searchRadius)
            right = null;
        if (up != null && Vector3.Distance(pos, up.pos) > searchRadius)
            up = null;
        if (down != null && Vector3.Distance(pos, down.pos) > searchRadius)
            down = null;
    }

    /*
    private void UpdateStep()
    {
        float radius = searchRadius * 1.2f;
        var nodes = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("Node"));

        foreach (var node in nodes)
        {
            var n = node.GetComponent<StepNode>();
            if (n == this || n == null) continue;

            var dir = transform.position - n.pos; //计算方向向量
            float deltaY = dir.y;
            dir -= Vector3.up * deltaY ; //减去
            dir.Normalize();

            if (Mathf.Sign(-deltaY) * Vector3.Dot(dir, n.faceTo) < 0.99f) continue; //满足和阶梯连接的条件: 比它低且dir与朝向相反，或比它高且dir与朝向相同  

            if (Vector3.Dot(dir, Vector3.right) >= 0.99f)
            {
                right = n;
            }
            if (Vector3.Dot(dir, Vector3.left) >= 0.99f)
            {
                left = n;
            }
            if (Vector3.Dot(dir, Vector3.forward) >= 0.99f)
            {
                up = n;
            }
            if (Vector3.Dot(dir, Vector3.back) >= 0.99f)
            {
                down = n;
            }
        }

        if (left != null && Vector3.Distance(pos, left.pos) > searchRadius)
            left = null;
        if (right != null && Vector3.Distance(pos, right.pos) > searchRadius)
            right = null;
        if (up != null && Vector3.Distance(pos, up.pos) > searchRadius)
            up = null;
        if (down != null && Vector3.Distance(pos, down.pos) > searchRadius)
            down = null;
    }
    */

    #region debug
    private void OnMouseDown()
    {
        if (Time.timeScale == 0)
            return; //暂停时不执行

        OnInteract();
        EventManager.Instance.OnPlayerMoveStart.Invoke();
    }

    private void OnDrawGizmos()
    {
        //UpdateNeighbors();

        Gizmos.color = Color.green;
        foreach (var n in neighbors)
        {
            if(n != null)
                Gizmos.DrawLine(transform.position, n.pos);
        }

        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

    }
    #endregion

}
