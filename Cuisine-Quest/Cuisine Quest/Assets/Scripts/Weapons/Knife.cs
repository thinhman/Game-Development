using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Weapon {
    public int KnifeCount = 1;

    public float KnifeThrowingSpeed = 20f;
    public GameObject ThrowingKnife;

    public JabbingWeapon jw;


    //private float JabFinish;
    private bool Jabbing = false;

    //public GameObject Mesh;
    // Use this for initialization
    void Start () {
        activateWeapon(false);
    }
	
	// Update is called once per frame
	void Update () {

        jw.JabAttack(transform, ref Jabbing, false);

        
    }

    public override void Attack(Vector2 PlayerDirection)
    {
        if (jw.CanAttack() && KnifeCount > 0)
        {
            if (PlayerDirection.x < 0) jw.AttackLeft(transform, ref Jabbing);
            else if (PlayerDirection.x > 0) jw.AttackRight(transform, ref Jabbing);
            else if (PlayerDirection.y < 0) jw.AttackDown(transform, ref Jabbing);
            else if (PlayerDirection.y > 0) jw.AttackUp(transform, ref Jabbing);
        }
        
    }
    public override void AttackSecondary(Vector2 PlayerDirection, bool PrimaryAttacking)
    {
        if(KnifeCount > 0)
        {
            AttackAbortForced();
            GameObject throwingKnife = Instantiate(ThrowingKnife, gameObject.transform.position, Quaternion.identity);
            throwingKnife.GetComponent<Projectile>().SetLayer(gameObject.layer, Mesh.GetComponent<SpriteRenderer>().sortingOrder);
            
            throwingKnife.GetComponent<Rigidbody2D>().velocity = KnifeThrowingSpeed * PlayerDirection;
            throwingKnife.transform.right = PlayerDirection;

            KnifeCount -= 1;
        }
    }

    
    public void attackEnd()
    {
        transform.localPosition = new Vector3(0, 0, 0);
        //Mesh.SetActive(false);
        activateWeapon(false);
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
        Debug.Log("It's a knife, not a can opener!");
    }
}
