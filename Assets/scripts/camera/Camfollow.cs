using DA_Assets.FCU;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camfollow : MonoBehaviour
{

    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Fix the camera at a -65-degree angle along the x-axis
        Quaternion fixedRotation = Quaternion.Euler(55f, 45f, 0f);
        transform.rotation = fixedRotation;
        //first-person perspective:  transform.position = Player.transform.position;
        transform.position = Player.transform.position + new Vector3((float)(-8), 12, -6);

        // lay vi tri camera va ap dung cho ui
        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
    }
    void UpdateUI()
    {
        
    }
}
