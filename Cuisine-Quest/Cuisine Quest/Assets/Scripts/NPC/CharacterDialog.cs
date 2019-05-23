using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterDialog : MonoBehaviour
{
    public Dialog dialog;

    public void EnableDialog()
    {
        FindObjectOfType<DialogSystemController>().dialogPopup.SetActive(true);
        FindObjectOfType<DialogSystemController>().StartDialog(dialog);
    }
}
