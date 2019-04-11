using System.Collections;
using System.Collections.Generic;
using UnityEngine;



//using UnityEngine.Sprite


public class Tiles : MonoBehaviour
{
    public int type;


    public Color[] colors;//make tiles of diff color

    public Sprite[] tileSprites;
    
    public Vector3 startPosition;
    public Vector3 destPosition;
    private bool inSlide = false;

    public GameObject particles;

    private void Awake()
    {
        
    }
    
    // Start is called before the first frame update
    void Start()
    {
       
        if (GetComponent<PlayerControler>())
        {
            type = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inSlide)
        {
            if (GridMaker.sideLerp < 0)
            {
                transform.localPosition = destPosition;
                inSlide = false;
            }
            else
            {
                transform.localPosition= Vector3.Lerp(startPosition,destPosition,GridMaker.sideLerp);
            }
        }
    }

    public void setType(int i)
    {
        type = i;
        GetComponent<SpriteRenderer>().sprite = tileSprites [type];
    }
    


/*
    public void SetSprite(int rand)
    {
    type = rand;
        GetComponent<SpriteRenderer>().sprite = tileSprites [type];

        //GetComponent<SpriteRenderer>().material = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        //in game we are setting the color of a tile not sprite
    }
    */

   
public void SetColor(int rand)
    {
        type = rand;
        GetComponent<SpriteRenderer>().color =colors[type];
    }
    
    
    public bool ismatch(GameObject gameObject1,GameObject gameObject2)
    {
        Debug.Log(gameObject2);
        Tiles ts1 = gameObject1.GetComponent<Tiles>();
        Tiles ts2 = gameObject2.GetComponent<Tiles>();
        return ts1 != null && ts2 != null && type == ts1.type && type == ts2.type;
    }

    public void SetupSlide(Vector2 newDestPos)
    {
        inSlide = true;
        startPosition = transform.localPosition;
        destPosition = newDestPos;
    }

}
