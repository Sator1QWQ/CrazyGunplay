using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DushState : PlayerState
{
    public override StateType Type => StateType.Dush;

    public override bool OnExecute(PlayerEntity owner)
    {

        return false;
    }
}
