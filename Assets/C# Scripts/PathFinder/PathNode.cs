using DG.Tweening;
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

    //寻路时到达该点的代价，默认为1，若在同等代价(物理等长)的路径上，希望优先选择该路径，可以将其略微降低，同理若希望尽量不选择该点，略微提高此值
    //这一设定通常用在某些踩上即触发的机关，将其设置为1.1可以使在两条等长的路径上避免选它，设置为100可以使除了不得不走它的其他情况尽量避开它
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

    public Transform blockTransform;

    [Header("需要光照变化的物体")]
    public List<GameObject> castedObjects;

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

    #endregion

    public virtual void Awake()
    {
        lightExtension = GetComponent<LightExtension>();
        EventManager.Instance.MapUpdate.OnEarlyStart +=   ClearLightRoad;
    }

    public void OnDestroy()
    {
        EventManager.Instance.MapUpdate.OnEarlyStart -=   ClearLightRoad;
    }


    public virtual bool ReachAble(LightState inputState)
    {
        return true;
    }
    
    public void UpdateLightRoad(GameObject road, Quaternion rot)
    {
        //更新光照信息
        lightExtension.isLightCasted = true;
        FadeIn();
        Vector3 newPos = new Vector3(transform.position.x, Mathf.Ceil(transform.position.y)-0.5f, transform.position.z);

        if(blockTransform)
            lightRoads.Add(Instantiate(road, newPos, rot, blockTransform));
        else
            lightRoads.Add(Instantiate(road, newPos, rot,transform)) ;
    }

    


    public void ClearLightRoad()
    {
        //Debug.Log("加载地图更新方法");
        lightExtension.isLightCasted = false;
        FadeOut();
        //if(lightRoads)
        foreach (var lightRoad in lightRoads)
        {
            var roadCtrl = lightRoad.GetComponent<Road>();
            if (roadCtrl) {
                roadCtrl.FadeOutAndDestroy();
            }
        }
        lightRoads.Clear();

    }


    private void FadeIn()
    {
        foreach (var castedObject in castedObjects)
        {
            var material = castedObject.GetComponent<MeshRenderer>().materials[1];
            material.DOFade(1, 1f);
        }
    }

    private void FadeOut()
    {
        foreach (var castedObject in castedObjects)
        {
            var material = castedObject.GetComponent<MeshRenderer>().materials[1];
            material.DOFade(0, 1f);
        }
    }
}
