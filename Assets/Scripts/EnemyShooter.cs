using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    Shooter[] shooters = null;
    public float shootTimeStep = 0;
    public float levelTimeStep = 0;
    void Awake()
    {
        shootTimeStep = 3;
        levelTimeStep = 10;
        shooters = new Shooter[this.transform.childCount];
        for (int i = 0; i < shooters.Length; i++)
        {
            shooters[i] = this.transform.GetChild(i).GetComponent<Shooter>();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GameStart()
    {
        InvokeRepeating("RandomShoot", 1, shootTimeStep);
        InvokeRepeating("IncreaseLevel", levelTimeStep, levelTimeStep);
        foreach (Shooter s in shooters)
        {
            s.Resume();
        }
    }
    public void GamePause()
    {
        CancelInvoke();

        foreach(Shooter s in shooters)
        {
            s.Pause();
        }
    }
    public void GameEnd()
    {
        CancelInvoke();
        foreach (Shooter s in shooters)
        {
            s.Clear();
        }
        shootTimeStep = 3;
        levelTimeStep = 10;
    }
    void RandomShoot()
    {
        shooters[Random.Range(0, 6)].Shoot();
    }
    void IncreaseLevel()
    {
        if(shootTimeStep >= 0.4f)
            shootTimeStep -= 0.2f;
    }
}
