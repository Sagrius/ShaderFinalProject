using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManipulateMatirialFloatTest : MonoBehaviour
{

   
    //public GameObject testeSphere;
    //public Material instanceMaterial; 
    public RaycastHit hitInfo;

    [SerializeField] private Shader _drawShader;
    private RenderTexture splatMap;
    private Material _drawMaterial, _currentMaterial;

    [SerializeField] [Range(1, 500)] private float size;
    [SerializeField] [Range(0,1)] private float strength;


    // Start is called before the first frame update
    void Start()
    {
        _drawMaterial = new Material(_drawShader);
        _drawMaterial.SetVector("_Color", Color.red);
        _currentMaterial = GetComponent<Renderer>().material;
        splatMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        _currentMaterial.SetTexture("_SplatMap", splatMap);



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
                // hithit.collider.gameObject.GetComponent<Renderer>().material.SetVector("_HitLocation", hithit.point);

                _drawMaterial.SetVector("_Coordinates", new Vector4(hithit.textureCoord.x, hithit.textureCoord.y, 0, 0));
                _drawMaterial.SetFloat("_Strength", strength);
                _drawMaterial.SetFloat("_Size", size);


                RenderTexture temp = RenderTexture.GetTemporary(splatMap.width,splatMap.height,0,RenderTextureFormat.ARGBFloat);
                Graphics.Blit(splatMap, temp);
                Graphics.Blit(temp, splatMap, _drawMaterial);
                RenderTexture.ReleaseTemporary(temp);


               // instanceMaterial.SetVector("_HitLocation", hithit.point);



            }
        }
    }
}
