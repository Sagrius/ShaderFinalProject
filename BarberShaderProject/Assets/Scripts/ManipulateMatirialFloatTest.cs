using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateMatirialFloatTest : MonoBehaviour
{

   
    public GameObject testeSphere;
    public Material instanceMaterial; 
    public RaycastHit hitInfo;
    // Start is called before the first frame update
    void Start()
    {
        //instanceMaterial = testMatirial;
       //instanceMaterial = testeSphere.GetComponent<Renderer>().material; 
       // instanceMaterial.SetFloat("_ColorParameter", 0.5f);

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RaycastHit hithit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hithit, 100))
            {
                print("Hit something! " + hithit.collider.name);

                hithit.collider.gameObject.GetComponent<Renderer>().material.SetVector("_HitLocation", hithit.point);

               // instanceMaterial.SetVector("_HitLocation", hithit.point);



            }
        }
    }
}
