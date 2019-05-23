using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour {
    public GameObject Player;
    public Transform Camera;

    public AreaAbstract CurrentArea;
    //public Area[] Areas;
    public AreaScriptable[] AreaTest;

    private bool hasNewArea = true;
    private bool loadedNewArea = false;
    public float AreaLoadDelay = 1.0f;
    private float loadDelayStart = 0;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (hasNewArea && !loadedNewArea)
        {
            if (AreaLoadDelay + loadDelayStart < Time.fixedTime && !Camera.GetComponent<CameraController>().GetTransitioning()) FinishAreaMove();
            
        }
	}

    public void TeleportPlayer(Vector3 cameraLocation, Vector2 playerLocation)
    {

        Camera.position = cameraLocation;
        Player.transform.position = (Vector2)cameraLocation + playerLocation;

        hasNewArea = true;
        loadedNewArea = false;
    }

    public void TeleportPlayer(string location, Vector2 playerLocation){
        AreaScriptable locationArea = getAreaScriptable(location);
        if (locationArea != null)
        {
            TeleportPlayer((Vector3)locationArea.Location + new Vector3(0, 0, Camera.transform.position.z), playerLocation);
        }
        else
        {
            Debug.Log("Area not found");
        }
    }

    public void TeleportPlayer(string location){
        AreaScriptable locationArea = getAreaScriptable(location);
        if(locationArea != null){
            TeleportPlayer((Vector3)locationArea.Location + new Vector3(0,0,Camera.transform.position.z), locationArea.DefaultPlayerLocation);
            
        }
        else{
            Debug.Log("Area not found");
        }
    }


    private AreaScriptable getAreaScriptable(string location){
        AreaScriptable locationArea = null;
        foreach(AreaScriptable a in AreaTest){
            if (a.name == location) locationArea = a;
        }


        return locationArea;
    }

    public void StartMoveArea(AreaAbstract newArea)
    {
        if(newArea != CurrentArea)
        {
            CurrentArea.DeLoadArea();
            CurrentArea = newArea;
            hasNewArea = true;
            loadedNewArea = false;
            //CurrentArea.LoadArea();
            loadDelayStart = Time.fixedTime;
        }
    }

    public void FinishAreaMove(AreaAbstract newArea)
    {
        if (newArea != CurrentArea & !loadedNewArea)
        {
            CurrentArea.LoadArea();
            loadedNewArea = true ;
            hasNewArea = false;
        }
    }

    public void FinishAreaMove()
    {
        CurrentArea.LoadArea();
        loadedNewArea = true;
        hasNewArea = false;
    }

}


