using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaDefault : AreaAbstract {
	
	// Update is called once per frame
	void Update () {
		
	}
    public override bool LoadAreaSpecial()
    {
        return true;
    }
    //remove permanently or until specified time
    public override void DestroyObj(GameObject obj)
    {
        //Default behavior
        removeObj(obj);
        //ToLoad.Remove(obj.GetComponent<SpawnObject>().GetMyOrigin());
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
