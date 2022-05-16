using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Sprite[] sprites;
    public float size = 1.0f;
    public float minSize = 0.5f;
    public float maxSize = 2.5f;
    public float minSpeed = 10f;
    public float maxSpeed = 60f;
    public float maxLifeTime = 60.0f;
    public float halfAsteroidSpeed = 5.0f;
    public int asteroidDamage = 1;
    public int asteroiDamagaReduce=2;
    public ParticleSystem ExplosionEffect;
    private  SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    Player player;
    GameManager gm;

    void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer.sprite = sprites[Random.Range(0,sprites.Length)]; //Objenin Random Sprite/Resim ile oluşmasını sağlıyor
        this.transform.eulerAngles = new Vector3(0f,0f,(int)Random.value*360f); 
        this.transform.localScale = Vector3.one* this.size; //Altındaki ile aynı işevi yapan kod bu da farklı yazımı

        // new Vector3(this.size,this.size,this.size);

        rb.mass = this.size*5;
    }

    public void SetTrajectory(Vector2 direction){
        rb.AddForce(direction*AsteroidSpeed());

        Destroy(this.gameObject,this.maxLifeTime);
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            // ExplosionEffect.Play();
            if (this.size*0.5f >= this.minSize)
            {
                CreateSplit();        
                CreateSplit();        
            }
            
            gm.DestroyAsteroid(this);
            
            
        }
        else if (other.gameObject.tag == "Player")
        {
            player = other.gameObject.GetComponent<Player>();;
            player.SetPlayerHealth(AsteroidDamage());
        }
    }

    public void CreateSplit()
    {
        Vector2 position = this.transform.position;
        position += Random.insideUnitCircle *0.5f;
        Asteroid halfAsteroid = Instantiate(this, position,this.transform.rotation);
        halfAsteroid.size = this.size * 0.5f;
        halfAsteroid.SetTrajectory(Random.insideUnitCircle.normalized*halfAsteroidSpeed);
    }

    public float AsteroidSpeed(){
        return Random.Range(minSpeed,maxSpeed);
    }
    public int AsteroidDamage(){
        return asteroidDamage*(int)(AsteroidSpeed()/asteroiDamagaReduce);
    }

}
