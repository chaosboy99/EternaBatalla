using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textDisplayTutorial : MonoBehaviour
{
    public GameObject _UiObject;
    // Start is called before the first frame update
    void Start()
    {
        _UiObject.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter (Collider player)
    {
        if(player.gameObject.tag == "Player") 
        {
            _UiObject.SetActive (true);
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(5);
        _UiObject.SetActive(false);
        Destroy(gameObject);
    }


}
