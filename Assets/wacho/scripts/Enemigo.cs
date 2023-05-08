using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemigo : MonoBehaviour
{

    public float rangoArea;
    public LayerMask CapaPj;
    bool estarAlerta;
    public Transform Pj;
    public float vel;
    public int vida;

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
    private void OnCollisionEnter(Collision other)
    {


        if (other.gameObject.CompareTag("Bullet"))
        {
            vida--;
        }
        if (vida <= 0)
        {
            Destroy(gameObject);
        }

    }
}
