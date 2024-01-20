using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public LightState currentPlayerState = LightState.Light;
    public int currentLevel;
}
