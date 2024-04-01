using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

//A*算法实现类
public class AStar : Singleton<AStar>
{

#region A*搜索的临时结点内部类
    private class Node
    {
        public PathNode current; //该结点存储结点
        public PathNode last; //该结点的上一结点
        public float reachPrice; //到达该结点的代价
        public float targetPrice; //到达目的地的代价

        public float Price => reachPrice + targetPrice; //总代价
        public PathNode[] neighbors => current.neighbors; //邻居

        public Node(PathNode node,PathNode last,float reachPrice,PathNode target)
        {
            this.current = node;
            this.last = last;
            this.reachPrice = reachPrice;
            this.targetPrice = (target.pos - node.pos).magnitude;
        }
    }
    #endregion

#region A*寻路
    public bool FindPath(PathNode start,PathNode target,out List<PathNode> path, out bool isOrigin)
    {
        if (start == target) //特殊情况: 选定点刚好为终点时，返回该结点，通知这是起点
        {
            path = new List<PathNode>();
            path.Add(start);
            isOrigin = true;
            return true;
        }

        isOrigin = false;
        path = new List<PathNode>(); //结点

        List<Node> openNodes = new List<Node>(); //可访问的结点表
        Dictionary<PathNode,Node> visitedNodes = new Dictionary<PathNode,Node>(); //已访问的结点表

        openNodes.Add(new Node(start,null,0,target)); //向可访问表添加初始结点
        visitedNodes.Add(start, new Node(start, null, 0, target));
        bool success = false; //搜索成功flag

        int count = 0;

        if (!start.ReachAble(GameManager.Instance.CurrentPlayerState))
            return false; //若自身本身就处于一个禁用地块，直接返回失败

        while(openNodes.Count > 0 && !success && count ++ < 1000) 
        {
            var node = openNodes[0]; //取顶部结点

            if (node.current == target) //访问到终点，路径构建成功
                success = true;

            foreach (var n in node.neighbors)
            {
                if (n == null || !n.ReachAble(GameManager.Instance.CurrentPlayerState)) //目标结点不可达时，返回
                    continue;

                float newPrice = node.reachPrice + n.Price;
                var newNode = new Node(n, node.current, newPrice, target);

                if (visitedNodes.ContainsKey(n)) //当该结点已被存储时，检查新值与该值的到位代价，更小时替换
                {
                    if (visitedNodes[n].reachPrice > newPrice)
                    {
                        visitedNodes[n].reachPrice = newPrice;
                        visitedNodes[n].last = node.current;
                        openNodes.Add(newNode);
                    }
                }
                else
                {
                    visitedNodes.Add(n, newNode);
                    openNodes.Add(newNode);
                }
            }
            openNodes.Remove(node);
            openNodes = openNodes.OrderBy(n => n.Price).ToList();
        }

        if(success) //当成功时，将结点依次置入，然后逆序
        {
            Node temp = visitedNodes[target];
            while(temp.current != start)
            {
                path.Add(temp.current);
                temp = visitedNodes[temp.last];
            }
            //path.Add(start);

            path.Reverse();
            return true; 
        }
        return false;
    }
    #endregion
}
