using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AreaAbstract : MonoBehaviour
{

    public AreaScriptable MyArea;
    protected List<GameObject> loaded = new List<GameObject>();
    public List<SpawningObject> ToLoad = new List<SpawningObject>();
    public abstract void PlayerEnterDirection(Vector2 direction);

    public void LoadArea()
    {
        if (LoadAreaSpecial())
        {
            loadArea(ToLoad);
        }
        
    }
    protected void loadArea(List<SpawningObject> loadObjects)
    {
        foreach (SpawningObject so in loadObjects)
        {
            //LoadObj(so.Prefab, so.MyLocation);
            LoadObj(so);
        }
    }
    public abstract bool LoadAreaSpecial();

    public void LoadObj(SpawningObject objOrigin)
    {
        GameObject prefab = objOrigin.Prefab;
        Vector2 spawnLocation = objOrigin.MyLocation;
        GameObject newObj = Instantiate(prefab, spawnLocation + MyArea.Location, Quaternion.identity);
        newObj.GetComponent<SpawnObject>().SetMyOrigin(objOrigin);
        //newObj.SendMessage("SetMyOrigin", objOrigin);
        loaded.Add(newObj);
    }

    public abstract void DestroyObj(GameObject obj); //object killed or permanently removed
    public abstract void RemoveObj(GameObject obj); //object removed until area reload

    public void removeObj(GameObject obj)
    {
        loaded.Remove(obj);
        Destroy(obj);
    }

    public void DeLoadArea()
    {
        while (loaded.Count > 0)
        {
            GameObject obj = loaded[0];
            removeObj(obj);
        }
    }

    public void AddObj(GameObject obj)
    {
        loaded.Add(obj);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().StartMoveArea(this);

            float xDelta = transform.position.x - collision.transform.position.x;
            float yDelta = transform.position.y - collision.transform.position.y;
            if (xDelta > 6)
            {
                PlayerEnterDirection(new Vector2(1, 0));
            }else if(xDelta < -6)
            {
                PlayerEnterDirection(new Vector2(-1, 0));
            }
            else if(yDelta > 4)
            {
                PlayerEnterDirection(new Vector2(0, 1));
            }
            else if(yDelta < -4)
            {
                PlayerEnterDirection(new Vector2(0, -1));
            }
        }

    }

    //public SpawningObject GetSpawingObject(GameObject obj)
    //{
    //    SpawningObject returnObj = null;
    //    for(int i = 0; i < ToLoad.Count; i += 1)
    //    {
    //        //needs to be filled in
    //    }
    //    return returnObj;
    //}
}

[System.Serializable]
public class SpawningObject
{
    public GameObject Prefab;
    public Vector2 MyLocation;
}