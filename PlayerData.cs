using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int diamonds;
    public int health;
    public float[] position;
    public bool HasFlameSword;
    public bool HasBootsOfFlight;
    public bool HasKeytoCastle;

    public PlayerData (Player player)
    {
        diamonds = player.diamonds;
        health = player.Health;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

        HasFlameSword = GameManager.Instance.HasFlameSword;
        HasBootsOfFlight = GameManager.Instance.HasBootsOfFlight;
        HasKeytoCastle = GameManager.Instance.HasKeyToCastle;

    }
}
