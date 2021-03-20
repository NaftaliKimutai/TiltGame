using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float radius = 5.0F;
    public float power = 10.0F;
    public float lift = 30;
    public float speed = 10;
    public int AmmoType;
    public GameObject BurnParticle;
    public GameObject ExplosionParticle;
    public Transform LookObj;
    private void Start()
    {
        Explode();
    }
    public void Explode()
    {
        if (ExplosionParticle != null)
        {
            ExplosionParticle.transform.localScale = new Vector3(radius, radius, radius);
        }
       
        if (transform.position.y > 0.7f && BurnParticle != null)
        {
            BurnParticle.SetActive(false);
        }
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
           
            if (hit.GetComponent<Rigidbody>() != null)
            {
                hit.GetComponent<Rigidbody>().AddExplosionForce(power* hit.GetComponent<Rigidbody>().mass, explosionPos, radius, lift);
            }
            if (hit.GetComponentInParent<Player>() != null)
            {
               hit.gameObject.GetComponentInParent<Player>().Damage();
               /* if (CheckObjInbtw(hit.transform))
                {
                    hit.GetComponent<Player>().IsDead = true;
                    LookObj.gameObject.SetActive(true);
                }*/
            }

        }

       
    }
    bool CheckObjInbtw(Transform TargetObj)
    {
        LookObj.LookAt(TargetObj);
        RaycastHit hit;
        if(Physics.Raycast(transform.position,LookObj.forward,out hit, Mathf.Infinity))
        {
            if (hit.transform == TargetObj)
            {
                Debug.Log("HasHit");
                return true;
            }
        }
       
        return false;
    }

}
