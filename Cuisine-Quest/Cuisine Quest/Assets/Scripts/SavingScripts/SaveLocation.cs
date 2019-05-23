using UnityEngine;

public class SaveLocation : MonoBehaviour 
{
    public GameObject saveMenu;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            saveMenu.SetActive(true);
            SaveGameMenu.paused = true;
        }
    }
}
