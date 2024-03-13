using System;
using System.Collections.Generic;
using Sirenix.Serialization;
using UnityEngine;

public class MainMenuStateMachine : StateMachine
{
    [OdinSerialize] private List<MainMenuStateBase> mainMenuStates;

    private void Awake()
    {
        mainMenuStates.ForEach(mainState =>
        {
            mainState.Init();
        });
    }
}

