using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaBoss : AreaAbstract {

    private bool areaLoading = false;
    private bool areaLoaded = true;
    private LevelHandler lh;


    public GameObject[] Doors;
    private bool doorsShut = false;
    public float DoorOpenDelayTime = 0.5f;
    private float doorOpenDelayTimeBegin = Mathf.NegativeInfinity;
    private bool doorOpenDelayBegun = false;
    public List<SpawnObject> Enemies = new List<SpawnObject>();


	private void Update()
	{
        if(!areaLoaded){
            //close doors
            Debug.Log("areaLoaded");
            doorsControl(true);
            areaLoaded = true;
        }else if(areaLoading){
            if (!lh) lh = GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>();
            else{
                if(lh.CurrentArea.gameObject == gameObject){
                    areaLoaded = false;
                    areaLoading = false;
                }
            }
        }
        if(doorsShut){
            
            if (Enemies.Count <= 0)
            {
                Debug.Log("Enemy Count: " + loaded.Count);
                if(!doorOpenDelayBegun){
                    Debug.Log("doorOpenDelayTime set.");
                    doorOpenDelayTimeBegin = Time.fixedTime;
                    doorOpenDelayBegun = true;
                }else if(doorOpenDelayTimeBegin + DoorOpenDelayTime < Time.fixedTime){
                    doorsControl(false);
                    doorOpenDelayBegun = false;
                }

            }
        }

	}
    private void doorsControl(bool open){
        foreach (GameObject d in Doors)
        {
            d.SetActive(open);
        }
        doorsShut = open;
    }
	public override bool LoadAreaSpecial()
    {
        areaLoading = true;
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
