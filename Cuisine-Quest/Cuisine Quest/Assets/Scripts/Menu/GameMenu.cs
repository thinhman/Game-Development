using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    public Image weaponImage;
    public Sprite lockSprite;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI potionCountText;
    public CiscoTesting player;

    public Sprite tridentImage;

    private int potionCount;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CiscoTesting>();
        descriptionText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update () 
    {
        if(player.items != null && player.items.Count > 0)
        {
            if (player.items.ContainsKey(player.potion))
            {
                potionCount = player.items[player.potion];
            }
        }

        potionCountText.SetText(string.Format("x{0}", potionCount));
        //The true in this statement allows this to get components in children
        //even if they are disabled on disabled gameobjects
        if (player.CurrentWeapon == null)
        {
            return;
        }

        weaponImage.sprite = player.CurrentWeapon.gameObject.GetComponentInChildren<SpriteRenderer>(true).sprite;

        if(player.CurrentWeapon.isActiveAndEnabled)
        {
            switch (weaponImage.sprite.name)
            {
                case "Sword":
                    descriptionText.gameObject.SetActive(false);
                    break;
                case "trident_1_55":
                    weaponImage.sprite = tridentImage;
                    descriptionText.gameObject.SetActive(false);
                    break;
                case "Knife":
                    descriptionText.gameObject.SetActive(true);
                    descriptionText.SetText(string.Format("x{0}", player.CurrentWeapon.GetComponent<Knife>().KnifeCount));
                    break;
            }
        }
        else
        {
            weaponImage.sprite = lockSprite;
            descriptionText.gameObject.SetActive(false);
        }
	}
}
