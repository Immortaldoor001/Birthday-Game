using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrallaxefect : MonoBehaviour
{
    [SerializeField] private float VelParallax = 0f;
    private Transform cameraTransform;
    private Vector3 previusCamaraPosition;
    private float spriteWidth, startPosition;
   

    void Start()
    {
        cameraTransform = Camera.main.transform;
        previusCamaraPosition = cameraTransform.position;
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        startPosition = transform.position.x;

    }

    void LateUpdate()
    {
        float deltaX = (cameraTransform.position.x - previusCamaraPosition.x)*VelParallax;
        float moveAmount = cameraTransform.position.x * (1 - VelParallax);
        transform.Translate(new Vector3( deltaX, 0, 0));
        previusCamaraPosition=cameraTransform.position;
        
        if(moveAmount > startPosition + spriteWidth*0.75f)
        {
            transform.Translate(new Vector3(spriteWidth, 0, 0));
            startPosition += spriteWidth;
        }
        else if(moveAmount < startPosition - spriteWidth*0.70f)
        {
            transform.Translate(new Vector3(-spriteWidth,0,0));
            startPosition -= spriteWidth;
        }
        

    }
}
