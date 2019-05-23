using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AreaDungeonBridge : AreaAbstract {
    public List<SpawningObject> ToLoadOnBridge = new List<SpawningObject>();
    public int BridgeObjectOrderInLayer = 100;

    public GameObject UnderBridgeWall;
    public Tilemap BridgeWalls;
    public Tilemap BridgeFloor;
    public float BridgeDefaultAlpha = 0.5f;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override bool LoadAreaSpecial()
    {
        loadArea(ToLoad);

        foreach (SpawningObject so in ToLoadOnBridge)
        {
            //LoadObj(so.Prefab, so.MyLocation);
            GameObject prefab = so.Prefab;
            Vector2 spawnLocation = so.MyLocation;
            GameObject newObj = Instantiate(prefab, spawnLocation + MyArea.Location, Quaternion.identity);
            newObj.GetComponent<SpawnObject>().SetMyOrigin(so);
            //newObj.SendMessage("SetMyOrigin", objOrigin);
            newObj.gameObject.GetComponent<SpriteRenderer>().sortingOrder = BridgeObjectOrderInLayer;
            newObj.gameObject.layer =  9;
            loaded.Add(newObj);

            //LoadObj(so);
        }

        return false;
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
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        
        if(direction.y > 0 || direction.y < 0)
        {
            int newLayerOrder = 0;
            if (player.GetComponent<SpriteRenderer>().sortingOrder < BridgeObjectOrderInLayer) newLayerOrder = BridgeObjectOrderInLayer;
            player.GetComponent<CiscoTesting>().ChangeLayer(9, newLayerOrder);
            //player.layer = 9;
            //player.GetComponent<SpriteRenderer>().sortingOrder = BridgeObjectOrderInLayer;

        }else{
            UnderBridgeWall.SetActive(true);
            BridgeFloor.color = new Color(1, 1, 1, BridgeDefaultAlpha);
            BridgeWalls.color = new Color(1, 1, 1, BridgeDefaultAlpha);
            //BridgeFloor.SetColor(new Vector3Int(0,0,0),);
        }
        
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

             int newOrderLayer = 0;
            if (player.GetComponent<SpriteRenderer>().sortingOrder > BridgeObjectOrderInLayer) newOrderLayer = -BridgeObjectOrderInLayer;
            player.GetComponent<CiscoTesting>().ChangeLayer(0, newOrderLayer);

            //player.layer = 0;
            //player.GetComponent<SpriteRenderer>().sortingOrder = 20;

            UnderBridgeWall.SetActive(false);
            BridgeFloor.color = new Color(1, 1, 1, 1);
            BridgeWalls.color = new Color(1, 1, 1, 1);
        }
    }
}
