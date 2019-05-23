using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    private SpawningObject myOrigin;
    private int Layer;

    public void SetMyOrigin(SpawningObject myOrigin)
    {
        this.myOrigin = myOrigin;
    }

    public SpawningObject GetMyOrigin()
    {
        return myOrigin;
    }

    public int GetLayer() { return Layer; }
    public void SetLayer(int Layer) { this.Layer = Layer; }
}
