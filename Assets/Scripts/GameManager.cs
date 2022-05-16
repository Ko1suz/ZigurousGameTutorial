using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public  ParticleSystem explosion;
    public Transform PlayerPrefab;
    public Transform ReSpawnPoint;
    public int RespawnBackCount = 3;
    public int score;
    public int scoreIncreas = 100;
    public static GameObject gameOverUI;
    public GameObject gameOverUIRef;
    public static GameObject scoreUI;
    public GameObject scoreUIRef;

    void Awake()
    {
        gameOverUI = gameOverUIRef;
        scoreUI = scoreUIRef;
        if (gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        }
    }
    
    public IEnumerator ReSpawnPlayer()
    {
        
        yield return new WaitForSeconds(RespawnBackCount);

        Instantiate(PlayerPrefab,ReSpawnPoint.position,ReSpawnPoint.rotation);

    }

    public static void KillPlayer(Player player){
        gameOverUI.SetActive(true);
        scoreUI.SetActive(false);
        gm.explosion.transform.position = player.transform.position;
        ExplosionEffect();
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.ReSpawnPlayer());
        gm.score = 0;
    }
    public void DestroyAsteroid(Asteroid asteroid){
        gm.explosion.transform.position = asteroid.transform.position;
        ExplosionEffect();
        Destroy(asteroid.gameObject);
        if (asteroid.size<.75)
        {
            score+= scoreIncreas;
        }
        else if (asteroid.size<1)
        {
            score+= scoreIncreas*2;
        }
        else
        {
            score+= scoreIncreas*3;
        }
        
    }

    public static void ExplosionEffect(){
        gm.explosion.Play();
    }
    public void EndGame(){
        gameOverUI.SetActive(true);
    }
}
