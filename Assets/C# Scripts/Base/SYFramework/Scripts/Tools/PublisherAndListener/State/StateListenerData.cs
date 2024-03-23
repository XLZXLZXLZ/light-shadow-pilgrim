#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine.Events;

[Serializable]
public class StateListenerData
{
    //通过Inspector窗口挂载相应的监听者
    [field: OdinSerialize,
     LabelText("订阅者列表")]
    public List<IStatePublisher> publishers { get; set; }
    
    //通过Inspector窗口设置特定情况下发生的事件
    [OdinSerialize,
     DictionaryDrawerSettings(KeyLabel = "触发情况", ValueLabel = "触发动作"),
     LabelText("特定情况下的事件")] 
    public List<PLStateCase> stateActionDic = new();
    
    //在其他情况下发生的事件
    [OdinSerialize,
     LabelText("其他情况下的事件")] 
    public UnityAction elseAction;
    
    //当前的Publisher的状态
    [NonSerialized] public Dictionary<IStatePublisher, int> currentStateDic;
    
    public void OnPublisherStateChanged(IStatePublisher statePublisher, int state)
    {
        if (!currentStateDic.ContainsKey(statePublisher)) return;
        currentStateDic[statePublisher] = state;

        UnityAction action = GetRelevantAction();
        if(action == null)
            elseAction?.Invoke();
        else
            action.Invoke();
    }
    
    public UnityAction GetRelevantAction()
    {
        foreach (var stateAction in stateActionDic)
        {
            if (stateAction.IsMatchCase(currentStateDic))
                return stateAction.action;
        }
        
        return null;
    }
}

#endif



