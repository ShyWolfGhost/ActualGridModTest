using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UI.Button;
using Debug = UnityEngine.Debug;

public class PlayerControler : MonoBehaviour
{

    public KeyCode UpW;
    public KeyCode UpArr;
    public KeyCode DownS;
    public KeyCode DownArr;
    public KeyCode LeftA;
    public KeyCode LeftArr;
    public KeyCode RightD;
    public KeyCode RightArr;
    public Button Up;
    public Button Down;
    public Button Left;
    public Button Right;
    
    public GridMaker gridMaker;
    public Vector2 playerPosition;
    
    public TextMesh movement;
    public int mvmnt;
    //public Text score;
    //public int scoreInt;
    //Does score go into the tiles script?
    
    

    //public int C;
    //public int W;
   
    // Start is called before the first frame update
    
    void Start()
    {
        //GridMaker gridScript = gridMaker.GetComponent<GridMaker>();
        gridMaker = GameObject.Find("Grid").GetComponent<GridMaker>();
        mvmnt = 10;
        movement.text = mvmnt.ToString();
        //scoreInt = 0
        //Up.onClick.AddListener(delegate {Pressed(0,-1, "Button up"); });
        //Down.onClick.AddListener(delegate {Pressed(0,1, "Button Down"); });
        //Left.onClick.AddListener(delegate {Pressed(1,0, "Button Left"); });
        //Right.onClick.AddListener(delegate {Pressed(-1,0, "Button Right"); });
    
    }

    // Update is called once per frame
    void Update()
    {
        //score.text = "Score: " +scoreInt.ToString();
        
        if (mvmnt <= 10 && mvmnt > 0)
        {
            if (Input.GetKeyDown(UpW) || Input.GetKeyDown(UpArr)|| Input.GetButtonDown("Up"))
            {
                Debug.Log("UP");
                Pressed(0, -1, "Key UP");
                movement.text = mvmnt.ToString();

            }
            else if (Input.GetKeyDown(DownS) || Input.GetKeyDown(DownArr)|| Input.GetButtonDown("Down"))
            {
                Debug.Log("Down");
                Pressed(0, 1, "Key Down");
                movement.text = mvmnt.ToString();
            }
            else if (Input.GetKeyDown(LeftA) || Input.GetKeyDown(LeftArr)|| Input.GetButtonDown("Left"))
            {
                Debug.Log("Left");
                Pressed(1, 0, "Key Left");
                movement.text = mvmnt.ToString();
            }
            else if (Input.GetKeyDown(RightD) || Input.GetKeyDown(RightArr)|| Input.GetButtonDown("Right"))
            {
                Debug.Log("Right");
                Pressed(-1, 0, "Key Right");
                movement.text = mvmnt.ToString();
            }
        }

        else
        {
            Debug.Log("MVMNT=0!!! GAME OVER!!!");
            SceneManager.LoadScene("End");
        }

    }

    public void Pressed(int X, int Y, string f)
    {
        Debug.Log(f);
        if (mvmnt <= 10 && mvmnt > 0)
        {
            mvmnt--;
            movement.text = mvmnt.ToString();
            Vector2 oldPos = new Vector2(playerPosition.x, playerPosition.y);
            Vector2 newPos = new Vector2(playerPosition.x + X, playerPosition.y + Y);
            if (((int) newPos.x >= 0 && (int) newPos.x < GridMaker.WIDTH) &&
                ((int) newPos.y >= 0 && (int) newPos.y < GridMaker.HEIGHT))
            {
                GameObject swapTile = gridMaker.tilesObj[(int) newPos.x, (int) newPos.y];
                Vector3 swapPos = swapTile.transform.localPosition;

                swapTile.transform.localPosition = transform.localPosition; //tile goes to player
                transform.localPosition = swapPos; //player goes to tile

                //grid updating
                gridMaker.tilesObj[(int) oldPos.x, (int) oldPos.y] = swapTile;
                gridMaker.tilesObj[(int) newPos.x, (int) newPos.y] = gameObject;

                playerPosition = newPos;
                Debug.Log("MVMNT INT: " + mvmnt.ToString());

            }

            //GridMaker.StartPlsayer;
        }
        else
        {
            Debug.Log("MVMNT=0!!! GAME OVER!!!");
            SceneManager.LoadScene("End");
        }
    }
    
}

//get array position of the tile you want to move to
//debug it in consol
// store tiles position (NEW VECTOR)
//create a newvectors w players current position,
//give them to eachother
