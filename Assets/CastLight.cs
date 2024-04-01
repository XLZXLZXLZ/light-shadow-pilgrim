using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLight : MonoBehaviour
{
    [Header("投射方向")] 

    [SerializeField] private bool left,right,forward,back;
    [Header("检测位置偏移")]
    [SerializeField] private Vector3 offset;
    [Header("光强")]
    public int lightStrength;
    [Header("光路")]
    public GameObject lightRoadEnd;
    public GameObject lightRoadMid;
    private List<GameObject> lightRoads = new();
    //检测是否撞到物体
    private void Awake()
    {
         //EventManager.Instance.MapUpdate.OnEarlyStart += lightClear;

    }

    private void OnDisable()
    {

        EventManager.Instance.MapUpdate.OnStart-=Cast;
    }
    private void Start()
    {
       // Invoke(nameof(Cast), 20f);
      // Invoke(nameof(lightClear), 40f);
    }
    
    public void AddCastEvent()=>EventManager.Instance.MapUpdate.OnStart+=Cast;
    public void DeleteCastEvent() => EventManager.Instance.MapUpdate.OnStart -= Cast;

    public void lightClear()
    {
   //检索周围点
        var nodes = Physics.OverlapSphere(transform.position+offset, 1, LayerMask.GetMask("Node"));

        foreach (var node in nodes)
        {
            Debug.Log("开始摧毁全方位光路");

            var pathNode = node.GetComponent<PathNode>();
            var dir = (transform.position + offset) - node.transform.position;
            dir.Normalize();


                if (Vector3.Dot(dir, Vector3.left) >= 0.98f)
                {
                    Debug.Log("搜索到左结点");
                    int i = 0;
                    while (i < lightStrength&& pathNode != null)
                    {
                        pathNode.ClearLightRoad();
                        pathNode = pathNode.Left;

                        i++;
                    }
                    //Vector3 newPos = new Vector3(node.transform.position.x, Mathf.Ceil(node.transform.position.y)-0.5f, node.transform.position.z);
                    //     lightRoads.Add(Instantiate(lightRoad,newPos,Quaternion.identity));
                    //       lightNode.LightState = LightState.LightCasted;
                }

                if (Vector3.Dot(dir, Vector3.right) >= 0.98f)
                {
                    Debug.Log("搜索到右结点");
                    int i = 0;
                    while (i < lightStrength&& pathNode != null)
                    {
  
                        pathNode.ClearLightRoad();
                        pathNode = pathNode.Right;

                        i++;
                    }
                }

                if (Vector3.Dot(dir, Vector3.forward) >= 0.98f)
                {
                    Debug.Log("搜索到前结点");
                    int i = 0;
                    while (i < lightStrength && pathNode != null)
                    {
                        Debug.Log(pathNode.pos);
                        pathNode.ClearLightRoad();
                        pathNode = pathNode.Up;
                        i++;
                    }
                }

                if (Vector3.Dot(dir, Vector3.back) >= 0.98f)
                {
                    Debug.Log("搜索到后结点");
                    int i = 0;
                    while (i < lightStrength && pathNode != null)
                    {
                        pathNode.ClearLightRoad();
                        Debug.Log(pathNode);
                        pathNode = pathNode.Down;

                        i++;
                    }
                }
            
        }
    }
    
    // public void CastDir(Vector3 lightDir)
    // {
    //     //检索周围点
    //     var nodes = Physics.OverlapSphere(transform.position+offset, 1, LayerMask.GetMask("Node"));
    //     
    //     foreach (var node in nodes)
    //     {
    //         Debug.Log("开始建立光路"); 
    //         var lightNode = node.GetComponent<LightExtension>();
    //         if (lightNode == this || lightNode == null) continue;
    //
    //         var dir = transform.position+offset - node.transform.position;
    //         dir.Normalize();
    //
    //         if (Vector3.Dot(dir, lightDir) >= 0.98f)
    //         {
    //             Vector3 newPos = new Vector3(node.transform.position.x, Mathf.Ceil(node.transform.position.y)-0.5f, node.transform.position.z);
    //             lightRoads.Add(Instantiate(lightRoad,newPos,Quaternion.identity));
    //             lightNode.LightState = LightState.LightCasted;
    //         }
    //     }
    // }
    //
    public void Cast()
    {
        //检索周围点
        var nodes = Physics.OverlapSphere(transform.position+offset, 1, LayerMask.GetMask("Node"));
        
        foreach (var node in nodes)
        {
            Debug.Log("开始建立全方位光路");

            var pathNode = node.GetComponent<PathNode>();
            var dir = (transform.position+offset)-node.transform.position;
            dir.Normalize();

            if (pathNode != null)
            {
                 if (Vector3.Dot(dir, Vector3.left) >= 0.98f)
                {
                    Debug.Log("搜索到左结点");
                    int i = 0;
                    while (left&&i < lightStrength && pathNode != null)
                    {
                        i++;
                        if (pathNode == null)
                            break;

                        if (i == lightStrength || pathNode.Left == null)
                        {
                            pathNode.UpdateLightRoad(lightRoadEnd, Quaternion.identity);
                        }
                        else
                        {
                            pathNode.UpdateLightRoad(lightRoadMid, Quaternion.identity);
                        }
                        
                        
                        pathNode = pathNode.Left;
                    }
                    //Vector3 newPos = new Vector3(node.transform.position.x, Mathf.Ceil(node.transform.position.y)-0.5f, node.transform.position.z);
                    //     lightRoads.Add(Instantiate(lightRoad,newPos,Quaternion.identity));
                    //       lightNode.LightState = LightState.LightCasted;
                }
                if (right&&Vector3.Dot(dir, Vector3.right) >= 0.98f)
                {
                    Debug.Log("搜索到右结点");
                    int i = 0;
                    while (i < lightStrength && pathNode != null)
                    {
                        i++;
                        if (pathNode == null)
                            break;

                        if (i == lightStrength || pathNode.Right == null)
                        {
                            pathNode.UpdateLightRoad(lightRoadEnd, Quaternion.Euler(0, 180, 0));
                        }
                        else
                        {
                            pathNode.UpdateLightRoad(lightRoadMid, Quaternion.Euler(0, 180, 0));
                        }


                        pathNode = pathNode.Right;

                    }
                }
                if (forward&&Vector3.Dot(dir, Vector3.forward) >= 0.98f)
                {
                    Debug.Log("搜索到前结点");
                    int i = 0;
                    while (i < lightStrength&&pathNode!=null)
                    {
                        i++;
                        if (pathNode == null)
                            break;

                        if (i == lightStrength || pathNode.Up == null)
                        {
                            pathNode.UpdateLightRoad(lightRoadEnd, Quaternion.Euler(0, 90, 0));
                        }
                        else
                        {
                            pathNode.UpdateLightRoad(lightRoadMid, Quaternion.Euler(0, 90, 0));
                        }

                        pathNode = pathNode.Up;
                    }
                }
                if (back&&Vector3.Dot(dir, Vector3.back) >= 0.98f)
                {
                    Debug.Log("搜索到后结点");
                    int i = 0;
                    while (i < lightStrength&&pathNode!=null)
                    {
                        i++;
                        if (pathNode == null)
                            break;

                        if (i == lightStrength || pathNode.Down == null)
                        {
                            pathNode.UpdateLightRoad(lightRoadEnd, Quaternion.Euler(0, 270, 0));
                        }
                        else
                        {
                            pathNode.UpdateLightRoad(lightRoadMid, Quaternion.Euler(0, 270, 0));
                        }


                        pathNode = pathNode.Down;
                    }
                }
            }
           
            
        }
    }
    
    
    #region debug

    private void OnDrawGizmos()
    {
        //UpdateNeighbors();

        Gizmos.color = Color.yellow;
        foreach (var n in lightRoads)
        {
            if(n != null)
                Gizmos.DrawLine(transform.position, n.transform.position);
        }

        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);

    }
    #endregion

}
