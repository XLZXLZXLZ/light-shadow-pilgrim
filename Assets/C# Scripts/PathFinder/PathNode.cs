using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PathNode : MonoBehaviour
{
    //相邻结点
    [SerializeField]
    protected PathNode left, right, up, down;

    #region 属性
    public Vector3 pos => transform.position;
    public PathNode Left => left;
    public PathNode Right => right;
    public PathNode Up => up;
    public PathNode Down => down;
    public PathNode[] neighbors => new PathNode[4] { left, right, up, down };
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
    }

    private LightExtension lightExtension;
    
    #endregion

    public virtual bool ReachAble(LightState inputState)
    {
        return true;
    }
}
