using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    public float parallaxFactor = 0.5f;
    public float framesParllaxFactor = 8f;
    public float smoothX = 4;
    public float smoothY = 4;
    public Transform[] background;

    private Transform cam;
    private Vector3 camPrePos;

    private void Awake()
    {
        cam = Camera.main.transform;
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //摄像机位置作为上次位置存放
        camPrePos = cam.position;
    }
    void bkParallax()
    {
        float fparallax = (camPrePos.x - cam.position.x)*parallaxFactor;
        for(int i=0;i<background.Length;i++)
        {
            float bkNewX = background[i].position.x + fparallax*(1 + i * framesParllaxFactor);
            Vector3 bkNewPos = new Vector3(bkNewX, background[i].position.y, background[i].position.z);
            background[i].position = Vector3.Lerp(background[i].position, bkNewPos, Time.deltaTime * smoothX);
            if(i==0)
                Debug.Log(bkNewX);
        }
        camPrePos = cam.position;
    }
    void bkParallax2()
    {
        float fparallax = (camPrePos.y - cam.position.y) * parallaxFactor;
        for (int i = 0; i < background.Length; i++)
        {
            float bkNewY = background[i].position.y + fparallax * (1 + i * framesParllaxFactor);
            Vector3 bkNewPos = new Vector3( background[i].position.x,bkNewY, background[i].position.z);
            background[i].position = Vector3.Lerp(background[i].position, bkNewPos, Time.deltaTime * smoothY);

        }
        camPrePos = cam.position;
    }


    // Update is called once per frame
    void Update()
    {
        bkParallax();
        bkParallax2();
    }
}
