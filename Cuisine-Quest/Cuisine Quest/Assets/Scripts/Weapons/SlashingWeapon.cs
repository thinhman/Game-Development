using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlashingWeapon  {

    public float RotationSpeed = 650;
    public float RotationBegin = -45f;
    public float RotationLength = 90f;

    public Vector2 HiltLocation = new Vector2(0.368f, -0.2f);
    public Vector2 HiltUpLocation = new Vector2(0.211f, -0.414f);
    public Vector2 HiltLeftLocation = new Vector2(-0.371f, -0.2f);
    public Vector2 HiltDownLocation = new Vector2(-0.3f, -0.46f);

    /// <summary>
    /// Displacement based on facing right
    /// </summary>
    public Vector2 HiltDisplacement = new Vector2(0, 1);

    public float WaitOnFinish = 0.3f;

    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }
    public Direction myDirection;

    private bool rotating = false;
    private float slashWaiting = float.NegativeInfinity;
    private float targetAngle = 0;
    private float currentDisplacement = 0;
    private bool startRotating = true;
    private Vector2 TargetDisplacement;

    public void SlashAttack(Transform weapon, ref bool attacking) //, Vector2 PlayerDirection)
    {

        Vector2 Displacement = HiltDisplacement;
        TargetDisplacement = HiltLocation;// + Displacement;

        float rotationOffset = 0;
        if (myDirection == Direction.Left)
        {
            rotationOffset = 180;
            Displacement = new Vector2(-HiltDisplacement.x, -HiltDisplacement.y);
            TargetDisplacement = HiltLeftLocation;// + Displacement;
        }
        else if (myDirection == Direction.Up)
        {
            rotationOffset = 90;
            Displacement = new Vector2(-HiltDisplacement.y, HiltDisplacement.x);
            TargetDisplacement = HiltUpLocation;// + Displacement;
        }
        else if (myDirection == Direction.Down)
        {
            rotationOffset = 270;
            Displacement = new Vector2(HiltDisplacement.y, -HiltDisplacement.x);
            TargetDisplacement = HiltDownLocation;// + Displacement;
        }

        if (attacking && startRotating)
        {
            weapon.localEulerAngles = new Vector3(0, 0, RotationBegin + rotationOffset);
            targetAngle = RotationLength; // - sp.RotationBegin;// + rotationOffset;
            currentDisplacement = 0;
            startRotating = false;
        }



        if (attacking)
        {

            float zRot = weapon.localEulerAngles.z;
            zRot = RotationSpeed * Time.deltaTime;

            //Debug.Log(weapon.localEulerAngles.z);
            //if (zRot - sp.RotationBegin <  sp.RotationLength + rotationOffset || zRot  > 180 + rotationOffset)
            if (currentDisplacement < targetAngle)
            {

                weapon.Rotate(new Vector3(0, 0, RotationSpeed * Time.deltaTime));
                currentDisplacement += zRot;
                //Debug.Log("rotating");
                rotating = true;
            }
            else if (attacking && rotating)
            {
                slashWaiting = Time.fixedTime;
                rotating = false;
            }
            else if (slashWaiting + WaitOnFinish < Time.fixedTime)
            {
                weapon.GetComponent<Weapon>().AttackAbort();
                attacking = false;
                startRotating = true;
            }

            float percentDone = currentDisplacement / targetAngle;
            weapon.localPosition = TargetDisplacement + percentDone * Displacement;
        }else if (!startRotating)
        {
            startRotating = true;
        }
        //else
        //{

        //}

    }
}
