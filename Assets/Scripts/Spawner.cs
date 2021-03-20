using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameManager TheMan;
    public GameObject[] TheObjs;
    float SpawnRate = 1;
    public float MaxBound=3;
    public float MinBound=0.5f;
    float spawnTimestamp;
    public float SpawnBounds = 5;
    public Transform TargetPlayer;
    public int SpecialObj;
    float SpecialObjTimestamp;
    // Start is called before the first frame update
    void Start()
    {
        MaxBound-= PlayerPrefs.GetInt("TheLevel")/5;
        if (MaxBound < 0.5f)
        {
            MaxBound = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMan();
    }
    void SpawnMan()
    {
        if (spawnTimestamp < Time.time&& TheMan.GameStarted&&!TheMan.TheP.IsDead)
        {
            if (TheMan.IsDoubleSpeed)
            {
                SpawnRate = Random.Range(MinBound/2, (MaxBound/2)- PlayerPrefs.GetFloat("Difficulty"));
            }
            else
            {
                SpawnRate = Random.Range(MinBound, MaxBound- PlayerPrefs.GetFloat("Difficulty"));
            }
            SpawnObj();
            spawnTimestamp = Time.time + SpawnRate;
        }
    }
    void SpawnObj()
    {
        int rand = Random.Range(0, TheObjs.Length);

        if (SpecialObjTimestamp > Time.time)
        {
            while (rand == SpecialObj)
            {
                rand = Random.Range(0, TheObjs.Length);
            }
        }
        if (rand == SpecialObj)
        {
            SpecialObjTimestamp = Time.time + 3;
        }
        Vector3 RandPos = new Vector3(TargetPlayer.position.x+ Random.Range(-SpawnBounds, SpawnBounds), transform.position.y, 0);
        GameObject go = Instantiate(TheObjs[rand], RandPos, Quaternion.identity);
        go.GetComponent<Obstacle>().TheMan = TheMan;
        int WhichDir = Random.Range(0, 2);
        if (WhichDir == 1)
        {
            go.GetComponent<Rigidbody>().AddTorque(-Vector3.forward * 10);

        }
        else
        {
            go.GetComponent<Rigidbody>().AddTorque(Vector3.forward * 10 );

        }
    }
}
