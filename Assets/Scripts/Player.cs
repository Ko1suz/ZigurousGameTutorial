using System.Dynamic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool thrusting; //itme demek 
    private float turnDireaction;
    private Rigidbody2D rb;
    private PlayerStatsScript playerStats;


    // public float thrustSpeed = 1f; 
    // public float turnSpeed = 1f;
    public ParticleSystem particles;
    // public Transform kıc;
    public Bullet  bulletPrefab;
    public Transform firePoint;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = PlayerStatsScript.instance;
    }
    void Start()
    {
        PlayerStatsScript.instance.currnetHealth = PlayerStatsScript.instance.maxHealth;
        playerStats = PlayerStatsScript.instance;
    }
    void Update()
    {
        thrusting = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            turnDireaction = 1f;
           
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            turnDireaction = -1f;
           
        }
        else
        {
            turnDireaction = 0;
        }

        if (Input.GetButtonDown("Fire1")||Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
       
    }

    void FixedUpdate()
    {
        // Debug.Log(playerStats.thrustSpeed+"NO BİTCHES");
        if (thrusting)
        {    
            rb.AddForce(this.gameObject.transform.up* playerStats.thrustSpeed);
        }
        if (turnDireaction != 0f)
        {
            rb.AddTorque(turnDireaction*playerStats.turnSpeed);
        }
    }
    public void Shoot(){
        Bullet bullet = Instantiate(this.bulletPrefab,firePoint.transform.position,this.transform.rotation);
        bullet.Project(this.gameObject.transform.up);
    }
    
    public void SetPlayerHealth(int damage)
    {
        PlayerStatsScript.instance.currnetHealth -= damage;
        Debug.LogWarning(damage+" Kadar hasar yedin");

        if (PlayerStatsScript.instance.currnetHealth<=0)
        {
            GameManager.KillPlayer(this);
        }
    }

}
