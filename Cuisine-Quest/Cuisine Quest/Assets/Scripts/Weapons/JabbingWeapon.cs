using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JabbingWeapon {

    public float AttackPowerMultiplier = 0;
    public float AttackWait = 0.4f;

    private float attackedLast = float.NegativeInfinity;

    public float Speed = 15;
    public float JabDistance = .5f;
    public float JabDuration = 0.3f;

    public float JabRightDistance = 0.5f;
    public float JabLeftDistance = 0.5f;
    public float JabUpDistance = 0.5f;
    public float JabDownDistance = 0.7f;

    public float JabXOffset = 0.1f;
    public float JabYOffset = -0.2f;

    private Vector2 JabVector;

    private float JabFinish;
    private bool jabFinishSet = false;
    

    public void JabAttack(Transform transform, ref bool Jabbing, bool HoldAtAttack)
    {
        if (Jabbing && Vector3.Magnitude(transform.localPosition) < Vector3.Magnitude(JabVector))
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, JabVector, Speed * Time.deltaTime);
        }
        else if (Jabbing && HoldAtAttack)
        {

            if (!jabFinishSet)
            {
                JabFinish = Time.fixedTime;
                jabFinishSet = true;
            }
        }
        else if (Jabbing)
        {
            Jabbing = false;
            if (!jabFinishSet)
            {
                JabFinish = Time.fixedTime;
                jabFinishSet = true;
            }
        }
        else if (JabFinish + JabDuration < Time.fixedTime)
        {
            transform.GetComponent<Weapon>().AttackAbort();
        }
    }
    public bool CanAttack()
    {
        
        if (attackedLast + AttackWait < Time.fixedTime)
        {
            return true;
        }
        else
        {
            return false;
        }
        }

    private void attack(Transform transform, ref bool Jabbing)
    {
        //JabFinish = Time.fixedTime + 2;
        if (attackedLast + AttackWait < Time.fixedTime)
        {
            Jabbing = true;
            jabFinishSet = false;
            transform.GetComponent<Weapon>().activateWeapon(true);
            //GetComponent<AudioSource>().Play();
            attackedLast = Time.fixedTime;
        }

    }

    public void AttackRight(Transform transform, ref bool Jabbing)
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 0);
        transform.localPosition = new Vector3(0, JabYOffset, 0);
        JabVector = new Vector2(JabRightDistance, JabYOffset);
        attack(transform, ref Jabbing);
    }

    public void AttackLeft(Transform transform, ref bool Jabbing)
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 180);
        transform.localPosition = new Vector3(0, JabYOffset, 0);
        JabVector = new Vector2(-JabLeftDistance, JabYOffset);

        attack(transform, ref Jabbing);
    }

    public void AttackDown(Transform transform, ref bool Jabbing)
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 270);
        transform.localPosition = new Vector3(-JabXOffset, 0, 0);
        JabVector = new Vector2(-JabXOffset, -JabDownDistance);
        attack(transform, ref Jabbing);
    }

    public void AttackUp(Transform transform, ref bool Jabbing)
    {
        if (attackedLast + AttackWait > Time.fixedTime) return;

        transform.localEulerAngles = new Vector3(0, 0, 90);
        transform.localPosition = new Vector3(JabXOffset, 0, 0);
        JabVector = new Vector2(JabXOffset, JabUpDistance);
        attack(transform, ref Jabbing);
    }
}
