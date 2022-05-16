using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public static PlayerStatsScript instance;
    public float thrustSpeed = 1f; 
    public float turnSpeed = 1f;
    public int maxHealth = 100;
    private int _currnetHealth;

    public int currnetHealth
    {
        get{ return _currnetHealth;}
        set{_currnetHealth = Mathf.Clamp(value,0,maxHealth);}
    } 

    private void Awake() 
        {
            if (instance==null)
            {
                instance = this;
            }
            //  currnetHealth =maxHealth;
        }

}
