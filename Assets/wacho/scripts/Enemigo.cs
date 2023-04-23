using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{

    public float rangoArea;
    public LayerMask CapaPj;
    bool estarAlerta;
    public Transform Pj;
    public float vel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        estarAlerta = Physics.CheckSphere(transform.position, rangoArea, CapaPj);

        if(estarAlerta == true)
        {
            transform.LookAt(new Vector3(Pj.position.x, transform.position.y, Pj.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(Pj.position.x, transform.position.y, Pj.position.z), vel * Time.deltaTime);
        }


    }
}
