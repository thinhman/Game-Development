using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JabbingProperties  {
    public float AttackPowerMultiplier = 0;
    public float AttackWait = 0.4f;

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
    private bool Jabbing = false;
}
