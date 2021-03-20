using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheCam : MonoBehaviour
{
    public Transform Target;
    public float MoveSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 AlterdPos = new Vector3(Target.position.x, transform.position.y, 0);
        transform.position = Vector3.Lerp(transform.position, AlterdPos, MoveSpeed * Time.deltaTime);
    }
}
