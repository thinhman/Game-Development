using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SlashingProperties  {

    public float RotationSpeed = 100f;
    public float RotationBegin = -45f;
    public float RotationLength = 90f;

    public Vector2 HiltLocation = new Vector2(0.5f, -0.2f);
    public Vector2 HiltUpLocation = new Vector2(0.211f, -0.414f);
    public Vector2 HiltLeftLocation = new Vector2(-0.371f, -0.2f);
    public Vector2 HiltDownLocation = new Vector2(-0.3f, -0.26f);

    public float WaitOnFinish = 0.3f;

    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }
    public Direction myDirection;
}
