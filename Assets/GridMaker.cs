using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GridMaker : MonoBehaviour
{
    public const int WIDTH = 5;
    public const int HEIGHT = 7;
    
    public int PlayerX;
    public int PlayerY;

    float xOff = WIDTH / 2f - 0.5f;
    float yOff = HEIGHT / 2f - 0.5f;

    public GameObject[,] tilesObj;
    public GameObject tilePrefab;
    public GameObject gridHolder;//holds 
    public GameObject playerPrefab;

    public SpriteRenderer SRen;
    //public GameObject Player;


    public float playerPosX;
    public float playerPosY;

    public static float sideLerp = -1;
    public float lerpSpeed = 0.25f;
    
    //public Par
    
    public Text score;
    public int scoreInt;
    
    

   

    // Start is called before the first frame update
    void Start()
    {
        //ParticleSystem.isPlaying = false;
        scoreInt = 0;
        tilesObj = new GameObject[WIDTH,HEIGHT]; // make array in start 
        gridHolder = new GameObject();//creates empty game obj
        gridHolder.transform.position = new Vector3(-1f,-0.5f,0);//set position of grid
        
        
        
        
        //double for loops

        for (int x = 0; x < WIDTH; x++)//Sets the x coordinats
        {
            for (int y = 0; y < HEIGHT; y++)//sets y coordinates
            {
                GameObject newTile = Instantiate(tilePrefab);//instantiates prefab (here for organization)
                newTile.transform.parent = gridHolder.transform;//makes tiles oparent the gridholder
                newTile.transform.localPosition = new Vector2(WIDTH - x - xOff,HEIGHT-y-yOff);//set position width-colloum-offset
                tilesObj[x, y] = newTile;
                //runs stuff in tile script
                Tiles tilescript = newTile.GetComponent<Tiles>();
                tilescript.SetColor(Random.Range(0, tilescript.colors.Length));

            }
        }
        
        /*
        PlayerX = WIDTH / 2;
        PlayerY = HEIGHT / 2;
        //Instantiate(player(PlayerX,PlayerY));
        //Vector2(PlayerX, PlayerY);
        float playerPosX = tilesObj[PlayerX, PlayerY].transform.localPosition.x;
        float playerPosY = tilesObj[PlayerX, PlayerY].transform.localPosition.y;
        Vector2 StartPlayer = new Vector2 (playerPosX,playerPosY);
        Debug.Log("Start Player Gridmaker: " + StartPlayer.ToString());
        Destroy(tilesObj[PlayerX,PlayerY]);
        //SRen=GetComponent<SpriteRenderer>();
        GameObject newPlayer = Instantiate(player);
        newPlayer.transform.parent = gridHolder.transform;
        
        player.GetComponent<PlayerControler>().playerPosition=new Vector2(PlayerX,PlayerY);

        newPlayer.transform.localPosition = StartPlayer;
        //get pos pre destruction
        //instantiate prefab there
        //do last : take out token from array and insert player
        */
        //player moved multiple tiles diagonal 
        
        
        
        
        
        Vector2 playerLocation = new Vector2(WIDTH / 2, HEIGHT / 2);
        
        // find tile that is in player's location
        GameObject removeTile = tilesObj[(int) playerLocation.x, (int) playerLocation.y];
        
        //create player and put it under grid holder
        GameObject player = Instantiate(playerPrefab);
        player.transform.parent = gridHolder.transform;
        
        // add player to tile 2d array
        player.transform.position = removeTile.transform.position;
        
        //set player's pos in its movement script
        player.GetComponent<PlayerControler>().playerPosition = playerLocation;
        tilesObj[WIDTH / 2, HEIGHT / 2] = player;
        
        Destroy(removeTile);
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreInt.ToString();
        //score text
        if (sideLerp < 0 && !Repopulate() && HasMatch() == true)
        {
            Debug.Log("A MATCH YEEHAW" + scoreInt.ToString());
            RemoveMatches();
            
        }
        else if(sideLerp>= 0)
        {
            sideLerp += Time.deltaTime / lerpSpeed;
            if (sideLerp >= 1)
            {
                sideLerp = -1;
            }

           
        }
        
    }


    //public void PressedGM(int X, int Y)
    //{
        //Debug.Log("NOTHING IS FUCKING WORKING");
        //Vector2 playerTempPos = new Vector2 (playerPosX,playerPosY);
        //Vector2 tileTempPos = new Vector2 (playerPosX +X, playerPosY +Y);
        //Debug.Log("PlayerTempPos: " + playerTempPos.ToString());
       //Debug.Log("TileTempPos: " + tileTempPos.ToString());
        //playerTempPos = (tileTempPos.x, tileTempPos.y);
        //tileTempPos = (playerTempPos.x, playerTempPos.y);
    //}

    public Tiles HasMatch()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            { 
                PlayerControler playerScript = playerPrefab.GetComponent<PlayerControler>();
                //par particle = particles.GetComponent<par>();
                Tiles tilescript = tilesObj[x, y].GetComponent<Tiles>();
                if (tilescript != null)
                {
                    Debug.Log(tilesObj[x+2,y]);
                    if (x<WIDTH-2 && tilescript.ismatch(tilesObj[x+1,y], tilesObj[x+2,y]))
                    {
                        return tilescript;
                    }
                    if (y<HEIGHT-2 && tilescript.ismatch(tilesObj[x,y+1], tilesObj[x,y+2]))
                    {
                        /*
                        c
                        */
                        playerScript.mvmnt = 6;
                        //par.DoEmit();
                        return tilescript;
                        //Debug.Log("MVMNT Aft a match: " +playerScript.mvmnt.ToString());
                    }
                }
            }
        }

        return null;
    }



    public void RemoveMatches()
    {
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Tiles tilescript = tilesObj[x, y].GetComponent<Tiles>();
                
                if (tilescript != null)
                {
                    if (x < WIDTH - 2 && tilescript.ismatch(tilesObj[x + 1, y], tilesObj[x + 2, y]))

                    {
                        Destroy(tilesObj[x, y]);
                        Destroy(tilesObj[x + 1, y]);
                        Destroy(tilesObj[x + 2, y]);

                    }

                    if (y < HEIGHT - 2 && tilescript.ismatch(tilesObj[x, y + 1], tilesObj[x, y + 2]))
                    {
                        Destroy(tilesObj[x, y]);
                        Destroy(tilesObj[x, y + 1]);
                        Destroy(tilesObj[x, y + 2]);
                        scoreInt = scoreInt + 3;//I don't think this is 100% in the right place
                        //PARTICLES EMIT PARTICLES 
                        //ParticleSystem.isPlaying=true;


                    }

                }
            }
        }

    }
    

    public bool Repopulate()//a type has to return something the type is a bool
    {
        bool repop = false;// return to what calls repop
        for (int x= 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
              if (tilesObj[x,y]==null)
              {
                  repop = true;
                  if (y == 0)
                  {
                      tilesObj[x, y] = Instantiate(tilePrefab);
                      Tiles tilescript = tilesObj[x, y].GetComponent<Tiles>();
                      tilescript.SetColor(Random.Range(0, tilescript.colors.Length));
                      tilesObj[x, y].transform.parent = gridHolder.transform;
                      tilesObj[x, y].transform.localPosition = new Vector2(WIDTH - x - xOff, HEIGHT - y - yOff);
                  }
                  else
                  {
                      sideLerp = 0;
                      tilesObj[x, y] = tilesObj[x, y - 1];
                      Tiles tilescript = tilesObj[x, y].GetComponent<Tiles>();
                      if(tilescript != null)
                      {
                          tilescript.SetupSlide(new Vector2(WIDTH - x- xOff, HEIGHT-y-yOff));
                      }
                      PlayerControler playerScript = tilesObj[x,y].GetComponent<PlayerControler>();
                      if (playerScript != null)
                      {
                          playerScript.playerPosition.Set(x,y);
                          playerScript.mvmnt = 6;
                      }

                      tilesObj[x, y - 1] = null;

                      tilesObj[x, y - 1 ]= null;
                  }
              }
            }  
        }

        return repop;
        //return false;
    }
 }





