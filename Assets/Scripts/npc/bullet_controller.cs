using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{ 
    public float timer;
    void Start()
    {  
        timer = 4 ;
    }

    void Update()
    {
        timer -= Time.deltaTime ;
        if(timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
