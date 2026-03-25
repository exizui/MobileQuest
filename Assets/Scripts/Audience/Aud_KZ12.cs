using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aud_KZ12 : Audience
{
    
    public override void Entry()
    {
        base.Entry();
        //Debug.Log("kz12");
        QuestManager.singleton.OnLocationEntered(this);
    }


    public override void Exit()
    {
        base.Exit();
        questTest.Exit();
    }

  
}
