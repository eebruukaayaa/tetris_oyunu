using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTetrominos : MonoBehaviour
{

    public bool create_tetromino;

    private TetrominoMovement Tetromino_Enabled;
    private GameObject Camera;
    private string[] colors = {"RedLight", "OrangeLight", "YellowLight", "GreenLight", "CyanLight", "BlueLight", "MagentaLight"};


    // Start is called before the first frame update
    void Start()
    {
        create_tetromino = true;

        Camera = GameObject.Find("Main Camera");
        Tetromino_Enabled = Camera.GetComponent<TetrominoMovement>();
    }

   

    private void Create_Tetromino()
    {
        GameObject[] block = new GameObject[4]; 
        int randomcolor = UnityEngine.Random.Range(0,7);

        for(int i = 0; i < 4; i++)
        {
            block[i] = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block[i].gameObject.tag = "Block";

            Material newMat = Resources.Load(colors[randomcolor], typeof(Material)) as Material;
            block[i].GetComponent<Renderer>().material = newMat;

            Tetromino_Enabled.Add_enabled_block(block[i]);
        }

        if(randomcolor == 0) //Z
        {
            block[0].transform.position = new Vector3(4, 22, 0);
            block[1].transform.position = new Vector3(5, 22, 0);
            block[2].transform.position = new Vector3(5, 21, 0);
            block[3].transform.position = new Vector3(6, 21, 0);
        }

        else if(randomcolor == 1) //L
        {
            block[0].transform.position = new Vector3(4, 21, 0);
            block[1].transform.position = new Vector3(5, 21, 0);
            block[2].transform.position = new Vector3(6, 21, 0);
            block[3].transform.position = new Vector3(6, 22, 0);
        }

        else if(randomcolor == 2) //O
        {
            block[0].transform.position = new Vector3(5, 21, 0);
            block[1].transform.position = new Vector3(6, 21, 0);
            block[2].transform.position = new Vector3(5, 22, 0);
            block[3].transform.position = new Vector3(6, 22, 0);
        }

        else if(randomcolor == 3) //S
        {
            block[0].transform.position = new Vector3(4, 21, 0);
            block[1].transform.position = new Vector3(5, 21, 0);
            block[2].transform.position = new Vector3(5, 22, 0);
            block[3].transform.position = new Vector3(6, 22, 0);
        }

        else if(randomcolor == 4) //I
        {
            block[0].transform.position = new Vector3(4, 21, 0);
            block[1].transform.position = new Vector3(5, 21, 0);
            block[2].transform.position = new Vector3(6, 21, 0);
            block[3].transform.position = new Vector3(7, 21, 0);
        }

        else if(randomcolor == 5) //J
        {
            block[0].transform.position = new Vector3(4, 21, 0);
            block[1].transform.position = new Vector3(5, 21, 0);
            block[2].transform.position = new Vector3(6, 21, 0);
            block[3].transform.position = new Vector3(4, 22, 0);
        }

        else if(randomcolor == 6) //T
        {
            block[0].transform.position = new Vector3(4, 21, 0);
            block[1].transform.position = new Vector3(5, 21, 0);
            block[2].transform.position = new Vector3(6, 21, 0);
            block[3].transform.position = new Vector3(5, 22, 0);
        }

        Tetromino_Enabled.Set_Enabled_Tetromino(randomcolor);
        create_tetromino = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (create_tetromino)
        {
            Create_Tetromino();
        }
    }

    public void Set_Tetromino_Block(bool value)
    {
        create_tetromino = value;
    }
}
