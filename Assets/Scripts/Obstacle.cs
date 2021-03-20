using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public bool IsTrashBag;
    public float SlowTime = 2;
    public bool IsBomb;
    public float BombTime = 3;
    public GameObject BombObj;
    public GameObject DeadParticles;
    public GameObject TheObstacle;
    public GameManager TheMan;
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.GetComponentInParent<Player>() != null&&
            collision.gameObject.GetComponentInParent<Obstacle>()==null)
        {
            if (IsTrashBag)
            {
                collision.gameObject.GetComponentInParent<Player>().SlowDown(SlowTime);
            }
            else if (IsBomb)
            {
                StartCoroutine(StartBomb(0));
            }
            else
            {
                collision.gameObject.GetComponentInParent<Player>().Damage();
            }
        }
        else
        {
            

        }
        if (IsBomb&& collision.gameObject.GetComponentInParent<Obstacle>() == null)
        {
            StartCoroutine(StartBomb(BombTime));
        }
        else
        {
            if (collision.gameObject.GetComponentInParent<Obstacle>() == null)
            {
                Disable();
            }

        }

    }
    public void Disable()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        TheObstacle.SetActive(false);
        DeadParticles.SetActive(true);
        TheMan.AddScore(1);
        Destroy(gameObject, 3f);
    }
    IEnumerator StartBomb(float TheT)
    {
        yield return new WaitForSeconds(TheT);
        BombObj.SetActive(true);
        Disable();
    }
    
}
