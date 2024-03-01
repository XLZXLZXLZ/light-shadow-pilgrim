using System;
using System.Collections.Generic;
using UnityEngine;

public class StageSwitchRequest
{
    public StageBase NextStage { get; private set; }

    public StageSwitchRequest(StageBase nextStage)
    {
        NextStage = nextStage;
    }
}

