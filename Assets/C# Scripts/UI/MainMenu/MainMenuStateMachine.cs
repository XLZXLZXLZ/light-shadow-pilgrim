using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;

public class MainMenuStateMachine : StateMachine
{
    [OdinSerialize] private List<MainMenuStateBase> mainMenuStates;

    private void Awake()
    {
        stateDic = new()
        {
            { typeof(MainMenuChapter0State), mainMenuStates[0]},
            { typeof(MainMenuChapter1State), mainMenuStates[1]},
            // { typeof(MainMenuChapter2State), mainMenuStates[2]},
        };
        
        mainMenuStates.ForEach(mainState =>
        {
            mainState.Init();
        });
    }
}



