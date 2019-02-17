using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBird : Brid {

    public override void ShowSkill()
    {
        base.ShowSkill();
        rg.velocity *= 1.5f;
    }
}
