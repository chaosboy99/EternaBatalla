using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disparar : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject bullet;
    public Transform spawn;

    public float fuerza = 1500;
    public float shotRate = 0.5f;

    private float ShotRateTime = 0;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Time.time>ShotRateTime)
            {

                GameObject NewBullet;

                NewBullet = Instantiate(bullet, spawn.position, spawn.rotation);
                NewBullet.GetComponent<Rigidbody>().AddForce(spawn.forward*fuerza);
                ShotRateTime = Time.time + shotRate;
                Destroy(NewBullet, 2);

            }
        }
        
    }

}
