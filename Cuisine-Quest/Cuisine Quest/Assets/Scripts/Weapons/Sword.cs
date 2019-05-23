using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon {
    //public float AttackWait = 0.5f;
    //private float attackedLast = float.NegativeInfinity;

    //private Vector2 JabVector;

    //private float JabFinish;
    private bool Jabbing = false;

    private bool Slashing = false;

    //public JabbingProperties JP;
    //public SlashingProperties SP;

    public SlashingWeapon sw;
    public JabbingWeapon jw;

    //public GameObject Mesh;

    public enum AttackType
    {
        Jab,
        Slash
    }
    public AttackType MyAttack;

	// Use this for initialization
	void Start () {
        activateWeapon(false);
	}
	
	// Update is called once per frame
	void Update () {

        //if (MyAttack == AttackType.Jab) jw.JabAttack(transform, ref JabVector, ref JabFinish, ref Jabbing);
        if (MyAttack == AttackType.Jab) jw.JabAttack(transform, ref Jabbing, false);
        else if (MyAttack == AttackType.Slash)
        {
            
            sw.SlashAttack(transform, ref Slashing);
        }
	}

    public override void Attack(Vector2 PlayerDirection)
    {
        if (MyAttack == AttackType.Jab)
        {
            if (jw.CanAttack()) GetComponent<AudioSource>().Play();
            else return;

            if (PlayerDirection.x < 0) jw.AttackLeft(transform, ref Jabbing);
            else if (PlayerDirection.x > 0) jw.AttackRight(transform, ref Jabbing);
            else if (PlayerDirection.y < 0) jw.AttackDown(transform, ref Jabbing);
            else if (PlayerDirection.y > 0) jw.AttackUp(transform, ref Jabbing);

        }

        else if(MyAttack == AttackType.Slash && !Slashing)
        {
            transform.localPosition = sw.HiltLocation;
            transform.localEulerAngles = new Vector3(0, 0, sw.RotationBegin);

            //transform.right = PlayerDirection;

            if (PlayerDirection.x < 0)
            {
                transform.localPosition = sw.HiltLeftLocation;
                sw.myDirection = SlashingWeapon.Direction.Left;
            }
            else if (PlayerDirection.x > 0)
            {
                transform.localPosition = sw.HiltLocation;
                sw.myDirection = SlashingWeapon.Direction.Right;
            }
            else if (PlayerDirection.y > 0)
            {
                transform.localPosition = sw.HiltUpLocation;
                sw.myDirection = SlashingWeapon.Direction.Up;
            }
            else if (PlayerDirection.y < 0)
            {
                transform.localPosition = sw.HiltDownLocation;
                sw.myDirection = SlashingWeapon.Direction.Down;
            }

            GetComponent<AudioSource>().Play();
            Slashing = true;
            //Mesh.SetActive(true);
            activateWeapon(true);
            //SlashAttack(SP);
        }
    }
    public override void AttackSecondary(Vector2 PlayerDirection, bool PrimaryAttacking)
    {
        Debug.Log("I ain't throw'n me blade!");
    }

    
    public void attackEnd()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        //Mesh.SetActive(false);
        activateWeapon(false);
        Slashing = false;
        Jabbing = false;
    }

    public override bool AttackAbort()
    {
        attackEnd();

        return true;
    }
    public override void AttackAbortForced()
    {
        attackEnd();
    }

    protected override void weaponTriggered(Collider2D collision)
    {
        Debug.Log("Sword is all the trigger I need!");
    }
}
