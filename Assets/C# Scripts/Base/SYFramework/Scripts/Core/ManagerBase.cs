using System;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBase<T> : MonoSingleton<T> where T : MonoBehaviour
{
    private SYRoot root;
    protected SYRoot Root
    {
        get
        {
            if (root == null)
                root = GetComponentInParent<SYRoot>();
#if UNITY_EDITOR
            if(root == null)
                SYLog.LogError("不能找到SYUtility！");
#endif
            return root;
        }
    }
}

