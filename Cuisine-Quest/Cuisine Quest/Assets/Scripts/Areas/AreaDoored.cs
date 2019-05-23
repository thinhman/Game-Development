using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDoored : AreaAbstract {

    public override bool LoadAreaSpecial()
    {
        return true;
    }
    //remove permanently or until specified time
    public override void DestroyObj(GameObject obj)
    {
        //Default behavior

        ToLoad.Remove(obj.GetComponent<SpawnObject>().GetMyOrigin());
        removeObj(obj);
    }

    //remove until Area reload
    public override void RemoveObj(GameObject obj)
    {
        //Default behavior
        removeObj(obj);

    }

    public override void PlayerEnterDirection(Vector2 direction)
    {

    }
}
