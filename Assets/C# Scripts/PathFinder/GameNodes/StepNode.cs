using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepNode : PathNode
{
    /*
    [SerializeField]
    private float searchRadius = 1.5f;

    public Vector3 faceTo => transform.rotation * Vector3.left;

    private void Awake()
    {
        UpdateNeighbors();
    }

    //自动搜索附近的结点并尝试连接 唉屎山唉
    private void UpdateNeighbors()
    {
        UpdatePlatform();
        UpdateStep();
    }

    private void UpdatePlatform()
    {
        var nodes = Physics.OverlapSphere(transform.position, searchRadius, LayerMask.GetMask("Node"));

        foreach (var node in nodes)
        {
            var n = node.GetComponent<PlatformNode>();
            if (n == this) continue;

            var dir = transform.position - n.pos;
            dir.Normalize();

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

    private void UpdateStep()
    {
        float radius = searchRadius;
        var nodes = Physics.OverlapSphere(transform.position, searchRadius, LayerMask.GetMask("Node"));

        foreach (var node in nodes)
        {
            var n = node.GetComponent<StepNode>();
            if (n == this || n == null) continue;

            var dir = transform.position - n.pos;
            dir.Normalize();

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

    private void OnMouseDown()
    {
        EventManager.Instance.OnClickNode?.Invoke(this);
    }

    private void OnDrawGizmos()
    {
        UpdateNeighbors();

        Gizmos.color = Color.green;
        foreach (var n in neighbors)
        {
            if (n != null)
                Gizmos.DrawLine(transform.position, n.pos);
        }

        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, searchRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + faceTo * 2);
    }
    */
}
