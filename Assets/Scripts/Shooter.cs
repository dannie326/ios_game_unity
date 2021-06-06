using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Enemy enemyPref = null;
    [SerializeField]
    int dir = 0;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Shoot()
    {
        Enemy e=Instantiate(enemyPref, transform);
        e.Fly(dir);
    }
    public void Pause()
    { 
        foreach(Enemy e in transform.GetComponentsInChildren<Enemy>())
        {
            e.Pause();
        }
    }
    public void Clear()
    {
        foreach (Enemy e in transform.GetComponentsInChildren<Enemy>())
        {
            Destroy(e.gameObject);
        }
    }
    public void Resume()
    {
        foreach (Enemy e in transform.GetComponentsInChildren<Enemy>())
        {
            e.Fly(dir);
        }
    }
}
