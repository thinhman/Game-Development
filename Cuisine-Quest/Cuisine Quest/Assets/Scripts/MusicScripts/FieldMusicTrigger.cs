using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldMusicTrigger : MonoBehaviour 
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        float dot = Vector3.Dot(new Vector2(0, -1), collision.GetComponent<Rigidbody2D>().velocity);

        if (dot > 0)
        {
            AudioSourceController.Instance.StartCoroutine(AudioSourceController.Instance.PlayFieldMusic());
        }
    }
}
