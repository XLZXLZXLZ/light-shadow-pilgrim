using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    //相邻结点
    [SerializeField]
    protected PathNode left, right, up, down;

    //寻路时到达该点的代价，默认为1，若在同等代价的路径上，希望优先选择该路径，可以将其略微降低，同理若希望尽量不选择该点，略微提高此值
    [SerializeField]
    private float price = 1;
    public float Price => price;    

    #region 属性
    public Vector3 pos => transform.position;
    public PathNode Left => left;
    public PathNode Right => right;
    public PathNode Up => up;
    public PathNode Down => down;
    public PathNode[] neighbors => new PathNode[4] { left, right, up, down };

    //创建的光路
    private List<GameObject> lightRoads = new();
    public LightState LightState
    {
        get
        {
            if (lightExtension == null)
            {
                lightExtension = GetComponent<LightExtension>();
            }
            return lightExtension.LightState;
        }
        private set {           // 只允许设置 LightState.LightCasted
            LightState = value;
        }

    }

    private LightExtension lightExtension;

    public virtual void Awake()
    {
        lightExtension = GetComponent<LightExtension>();
        EventManager.Instance.MapUpdate.OnEarlyStart +=   ClearLightRoad;
    }

    public void OnDestroy()
    {
        EventManager.Instance.MapUpdate.OnEarlyStart -=   ClearLightRoad;
    }

    #endregion

    public virtual bool ReachAble(LightState inputState)
    {
        return true;
    }
    
    public void UpdateLightRoad(GameObject road, Quaternion rot)
    {
        //更新光照信息
        lightExtension.isLightCasted = true;
        Vector3 newPos = new Vector3(transform.position.x, Mathf.Ceil(transform.position.y)-0.5f, transform.position.z);
        lightRoads.Add(Instantiate(road, newPos, rot)) ;
    }

    
    public void ClearLightRoad()
    {
        //Debug.Log("加载地图更新方法");
        lightExtension.isLightCasted = false;
        //if(lightRoads)
        foreach (var lightRoad in lightRoads)
        {
            Destroy(lightRoad);
        }
        lightRoads.Clear();

    }
}
