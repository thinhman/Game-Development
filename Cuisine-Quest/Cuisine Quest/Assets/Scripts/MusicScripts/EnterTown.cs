using System.Collections;
using UnityEngine;

public class EnterTown : MonoBehaviour 
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            float dot = Vector3.Dot(new Vector2(-1, 0), collision.GetComponent<Rigidbody2D>().velocity);

            if (dot > 0)
            {
                AudioSourceController.Instance.StopAllCoroutines();
                AudioSourceController.Instance.PlayAudioLooped("Town");
            }
            else
            {
                AudioSourceController.Instance.StartCoroutine(AudioSourceController.Instance.PlayFieldMusic());
            }
        }
    }


}
