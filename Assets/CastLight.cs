using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastLight : MonoBehaviour
{
    [Header("投射方向")] 

    [SerializeField] private bool left,right,forward,back;

    [SerializeField] private bool bottom;
    [Header("向下投射位移")]
    [SerializeField] private Vector3 bottomPos;
    [Header("检测位置偏移")]
    [SerializeField] private Vector3 offset;
    [Header("光强")]
    public int lightStrength;
    [Header("光路")]
    public GameObject lightRoadEnd;
    public GameObject lightRoadMid;
    public GameObject lightRoadSingle;
    private List<GameObject> lightRoads = new();
   // public Material material;


    [Header("父物体跟随旋转挂载")]
    public Transform parentTransform;

    private bool TargetLeft
    {
        get { 
            if(parentTransform)
            {
                return JudgeDir(Vector3.left);
            }
            return left; 
        }
    }
    private bool TargetRight
    {
        get
        {
            if (parentTransform)
            {
                return JudgeDir(Vector3.right);
            }
            return right;
        }
    }
    private bool TargetForward
    {
        get
        {
            if (parentTransform)
            {
                return JudgeDir(Vector3.forward);
            }
            return forward;
        }
    }
    private bool TargetBack
    {
        get
        {
            if (parentTransform)
            {
                return JudgeDir(Vector3.back);
            }
            return back;
        }

    }
    
    private bool JudgeDir(Vector3 vector)
    {
        if (left && Vector3.Dot((parentTransform.rotation * Vector3.left), vector) > 0.98f)
            return true;
        if (right && Vector3.Dot((parentTransform.rotation * Vector3.right), vector) > 0.98f)
            return true;
        if (forward && Vector3.Dot((parentTransform.rotation * Vector3.forward), vector) > 0.98f)
            return true;
        if (back && Vector3.Dot((parentTransform.rotation * Vector3.back), vector) > 0.98f)
            return true;
        return false;

    }


    //检测是否撞到物体
    private void Awake()
    {


    }


    private void OnDestroy()
    {
 //       EventManager.Instance.MapUpdate.OnEarlyStart -= FadeOut;
    }
    private void Start()
    {
   //     EventManager.Instance.MapUpdate.OnEarlyStart += FadeOut;
    }
    
    public void AddCastEvent()=>EventManager.Instance.MapUpdate.OnEarlyFinished+=Cast;
    public void DeleteCastEvent() => EventManager.Instance.MapUpdate.OnEarlyFinished -= Cast;

    public void CastSingle(Vector3 _offset)
    {
        Vector3 newPos = offset + transform.position + _offset;
        var nodes = Physics.OverlapSphere(newPos, 0.2f, LayerMask.GetMask("Node"));
        foreach (var node in nodes)
        {
            Debug.Log("检测到结点");
            var pathNode = node.GetComponent<PathNode>();
            pathNode.UpdateLightRoad(lightRoadEnd, Quaternion.identity);
        }


    }
/*
    public void FadeOut()
    {
        if(material)
        material.DOFade(0, 1f);
    }
    public void FadeIn()
    {
        if(material)
        material.DOFade(1, 1f);
    }
*/

    /// <summary>
    /// 单独负责光线投射的代码
    /// </summary>
    public void Cast()
    {
       // FadeIn();
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
                Debug.Log(TargetRight);
                Debug.Log(transform.right);
                if (parentTransform)
                {
                    Debug.Log(parentTransform.right);
                }
                if (Vector3.Dot(dir, Vector3.left) >= 0.98f)
                {
                    Debug.Log("搜索到左结点");
                    int i = 0;
                    while (TargetLeft&&i < lightStrength && pathNode != null)
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
                if (TargetRight&&Vector3.Dot(dir, Vector3.right) >= 0.98f)
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
                if (TargetForward&&Vector3.Dot(dir, Vector3.forward) >= 0.98f)
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
                if (TargetBack&&Vector3.Dot(dir, Vector3.back) >= 0.98f)
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


        if(bottom)
        {
            CastSingle(bottomPos);
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

        Gizmos.color = Color.white;
        if(bottom)
        {
            Gizmos.DrawLine(transform.position + offset, transform.position + offset + bottomPos);
            Gizmos.DrawWireSphere(transform.position + offset + bottomPos, 0.2f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1);

    }
    #endregion

}
