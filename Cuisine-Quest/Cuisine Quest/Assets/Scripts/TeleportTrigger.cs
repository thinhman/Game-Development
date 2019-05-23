using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrigger : MonoBehaviour {
    
    //public string TeleportLocation;
    public AreaAbstract TeleportLocation;
    public Vector2 PlayerLocation;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(TeleportLocation != null){
                LevelHandler lh = GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>();
                //lh.StartMoveArea(TeleportLocation);
                lh.TeleportPlayer(TeleportLocation.name, PlayerLocation);
            }else{
                Debug.Log("TeleportLocation missing!");
            }



        }
    }
}
