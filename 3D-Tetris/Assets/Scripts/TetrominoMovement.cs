using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TetrominoMovement : MonoBehaviour
{
    private CreateTetrominos script;
    private GameObject Camera;

    private float timeRemaining;

    private List<GameObject> enabled_block;
    private List<GameObject> disabled_block;

    private bool leftmove;
    private float leftmovetime;
    private bool rightmove;
    private float rightmovetime;
    private bool downmove;
    private float downmovetime;

    private int enable_tetromino;
    private int position;

    private bool upmove;
    private float upmovetime;

    private int Score;
    public Text Score_result;

    private int Level;
    public Text Level_result;

    private int Lines;
    public Text Lines_result;

    float[] timer;


    // Start is called before the first frame update
    void Start()
    {
        Camera = GameObject.Find("Main Camera");
        script = Camera.GetComponent<CreateTetrominos>();
        timeRemaining = 0.48f;

        enabled_block = new List<GameObject>();
        disabled_block = new List<GameObject>();

        leftmove = true;
        leftmovetime = 0.3f;
        rightmove = true;
        rightmovetime = 0.3f;
        downmove = true;
        downmovetime = 0.1f;
        

        enable_tetromino = -1;
        position = 1;

        upmove = true;
        upmovetime = 0.5f;

        Score = 0;
        Level = 1;
        Lines = 0;
        timer = new float[] {0.48f, 0.48f, 0.38f, 0.33f, 0.28f, 0.23f, 0.18f, 0.13f, 0.08f, 0.06f,
                                 0.05f, 0.05f, 0.05f, 0.04f, 0.04f, 0.04f, 0.03f, 0.03f, 0.03f,
                                  0.02f, 0.02f, 0.02f, 0.02f, 0.02f, 0.02f, 0.02f, 0.02f, 0.02f, 0.02f,
                                  0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, };
    }

    private bool DownLimit()
    {
        bool limit = false;

        foreach(GameObject enableblock in enabled_block.ToArray())
        {
            if(enableblock.transform.position.y <= 1f)
            {
                limit = true;
                break;
            }
        }

        

        if(!limit)
        {
            foreach(GameObject disableblock in disabled_block)
            {
                foreach(GameObject enableblock in enabled_block.ToArray())
                {
                if(enableblock.transform.position.x == disableblock.transform.position.x && 
                    enableblock.transform.position.y == disableblock.transform.position.y + 1f)
                    {
                        limit = true;
                        break;
                    } 
                }
                if(limit)
                {
                    break;
                } 
            }
        }


        return limit;
    }

    private bool LeftLimit()
    {
        bool limit = false;
        foreach(GameObject enableblock in enabled_block.ToArray())
        {
            if(enableblock.transform.position.x <= 1f)
            {
               limit = true;
            }

        }

        

        if(!limit)
        {
            foreach(GameObject disableblock in disabled_block)
            {
                foreach(GameObject enableblock in enabled_block.ToArray())
                {
                    if(enableblock.transform.position.y == disableblock.transform.position.y && 
                    enableblock.transform.position.x == disableblock.transform.position.x + 1f)
                    {
                        limit = true;
                        break;
                    }

                }
                if(limit)
                {
                    break;
                }
                 
            }
        }


        return limit;
    }

    private bool RightLimit()
    {
        bool limit = false;
        foreach(GameObject enableblock in enabled_block.ToArray())
        {
            if(enableblock.transform.position.x >= 10f)
            {
                limit = true;
            }

        }

        

        if(!limit)
        {
            foreach(GameObject disableblock in disabled_block)
            {
                foreach(GameObject enableblock in enabled_block.ToArray())
                {
                    if(enableblock.transform.position.y == disableblock.transform.position.y && 
                    enableblock.transform.position.x == disableblock.transform.position.x - 1f)
                    {
                        limit = true;
                        break;
                    } 
                }
                if(limit)
                {
                    break;
                }
            }
        }


        return limit;
    }

    private void Check_Fulled_Rows()
    {
        int clear_line_count = 0;
        for(int i = 1; i < 21; i++)
        {
            int fulled_rows = 0;

            foreach(GameObject disableblock in disabled_block.ToArray())
            {
                if(disableblock.transform.position.y == i)
                {
                    fulled_rows++;
                }
            }
            if(fulled_rows >9)
            {
                foreach(GameObject disableblock in disabled_block.ToArray())
                {
                    if(disableblock.transform.position.y == i)
                    {
                        disabled_block.Remove(disableblock);
                        Destroy(disableblock);
                    }
                }

                foreach(GameObject disableblock in disabled_block.ToArray())
                {
                    if(disableblock.transform.position.y > i)
                    {
                        disableblock.transform.position += new Vector3(0, -1, 0);
                    }
                }

                clear_line_count++;
                Lines++;
                Lines_result.text = Lines.ToString();

                if(Lines % 10 == 0)
                {
                    Level++;
                    Level_result.text = Level.ToString();
                }

                i--;
            }
        }
        if(clear_line_count == 1)
        {
            Score += 100;
        }
        else if(clear_line_count == 2)
        {
            Score += 300;
        }
        else if(clear_line_count == 3)
        {
            Score += 500;
        }
        else if(clear_line_count == 4)
        {
            Score += 800;
        }
        Score_result.text = Score.ToString();
    }

    public void Enabled_to_Disabled()
    {
        string[] colors = {"Red", "Orange", "Yellow", "Green", "Cyan", "Blue", "Magenta"};

        foreach(GameObject block in enabled_block.ToArray())
        {
            disabled_block.Add(block);
            Material newMat = Resources.Load(colors[enable_tetromino], typeof(Material)) as Material;
            block.GetComponent<Renderer>().material = newMat;
        }
        enabled_block.Clear();
        Check_Fulled_Rows();
        script.Set_Tetromino_Block(true);
        position = 1;
    }

    private void SoftDrop()
    {
        if(!DownLimit())
        {
            foreach(GameObject block in enabled_block.ToArray())
            {
                block.transform.position = new Vector3(block.transform.position.x, block.transform.position.y - 1f, 0);
            }

        }
        
        else
        {
            Enabled_to_Disabled();
        }
    }

    private bool Rotation_Limit(GameObject[] blocks, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        bool my_limit = false;

        float new_x1 = blocks[0].transform.position.x + x1;
        float new_x2 = blocks[1].transform.position.x + x2;
        float new_x3 = blocks[2].transform.position.x + x3;
        float new_x4 = blocks[3].transform.position.x + x4;

        float new_y1 = blocks[0].transform.position.y + y1;
        float new_y2 = blocks[1].transform.position.y + y2;
        float new_y3 = blocks[2].transform.position.y + y3;
        float new_y4 = blocks[3].transform.position.y + y4;

        if((new_x1 < 1 || new_x1 > 10) || (new_x2 < 1 || new_x2 > 10)
            || (new_x3 < 1 || new_x3 > 10) || (new_x4 < 1 || new_x4 > 10)) 
        {
            my_limit = true;
        }

        if((new_y1 < 1 || new_y1 > 20) || (new_y2 < 1 || new_y2 > 20)
            || (new_y3 < 1 || new_y3 > 20) || (new_y4 < 1 || new_y4 > 20)) 
        {
            my_limit = true;
        }

        if(!my_limit)
        {
            foreach(GameObject disableblock in disabled_block.ToArray())
            {
                if((disableblock.transform.position.x == new_x1 && disableblock.transform.position.y == new_y1) 
                   || (disableblock.transform.position.x == new_x2 && disableblock.transform.position.y == new_y2)
                   || (disableblock.transform.position.x == new_x3 && disableblock.transform.position.y == new_y3) 
                   || (disableblock.transform.position.x == new_x4 && disableblock.transform.position.y == new_y4) )
                {
                    my_limit = true;
                    break;
                }
            }
        }

        return my_limit;
    }

    private void Set_to_New_Position(GameObject[] blocks, int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4)
    {
        if(!Rotation_Limit(blocks, x1, y1, x2, y2, x3, y3, x4, y4))
        {
            blocks[0].transform.position = new Vector3(blocks[0].transform.position.x + x1, blocks[0].transform.position.y + y1, 0);
            blocks[1].transform.position = new Vector3(blocks[1].transform.position.x + x2, blocks[1].transform.position.y + y2, 0);
            blocks[2].transform.position = new Vector3(blocks[2].transform.position.x + x3, blocks[2].transform.position.y + y3, 0);
            blocks[3].transform.position = new Vector3(blocks[3].transform.position.x + x4, blocks[3].transform.position.y + y4, 0);
        }
        else
        {
            if(position == 0)
            {
                position = 4;
            }
            position--;
        }
    }

    private void Rotate_Tetromino()
    {
        position++;
        GameObject[] blocks = enabled_block.ToArray();

        if(enable_tetromino == 0)  //Z Tetromino için
        {
            if(position == 2)
            {
                Set_to_New_Position(blocks, 1 , 1 , 0 , 0, -1, 1, -2 , 0);
            }
            else if(position == 3)
            {
                Set_to_New_Position(blocks, 1 , -1 , 0 , 0, 1, 1, 0 , 2);
            }
            else if(position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1 , -1 , 0 , 0, 1, -1, 2 , 0);
            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1 , 1 , 0 , 0, -1, -1, 0 , -2);
            }
        }
        else if(enable_tetromino == 1) //L
        {
            if(position == 2)
            {
                Set_to_New_Position(blocks, 1 , 1 , 0 , 0, -1, -1, 0 , -2);
            }
            else if(position == 3)
            {
                Set_to_New_Position(blocks, 1 , -1 , 0 , 0, -1, 1, -2 , 0);
            }
            else if(position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1 , -1 , 0 , 0, 1, 1, 0 , 2);
            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1 , 1 , 0 , 0, 1, -1, 2 , 0);
            }
        }

        else if(enable_tetromino == 3)  //S
        {
            if(position == 2)
            {
                Set_to_New_Position(blocks, 1 , 1 , 0 , 0, 1, -1, 0 , -2);
            }
            else if(position == 3)
            {
                Set_to_New_Position(blocks, 1 , -1 , 0 , 0, -1, -1, -2 , 0);
            }
            else if(position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1 , -1 , 0 , 0, -1, 1, 0 , 2);
            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1 , 1 , 0 , 0, 1, 1, 2 , 0);
            }
        }

        else if(enable_tetromino == 4) //I
        {
            if(position == 2)
            {
                Set_to_New_Position(blocks, 2 , -1 , 1 , 0, 0, 1, -1 , 2);
            }
            else if(position == 3)
            {
                Set_to_New_Position(blocks, 1 , 2 , 0 , 1, -1, 0, -2 , -1);
            }
            else if(position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -2 , 1 , -1 , 0, 0, -1, 1 , -2);
            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1 , -2 , 0 , -1, 1, 0, 2 , 1);
            }
        }

        else if(enable_tetromino == 5) //J
        {
            if(position == 2)
            {
                Set_to_New_Position(blocks, 1 , 1 , 0 , 0, -1, -1, 2 , 0);
            }
            else if(position == 3)
            {
                Set_to_New_Position(blocks, 1 , -1 , 0 , 0, -1, 1, 0 , -2);
            }
            else if(position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1 , -1 , 0 , 0, 1, 1, -2, 0);
            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1 , 1 , 0 , 0, 1, -1, 0, 2);
            }
        }

        else if(enable_tetromino == 6) //T
        {
            if(position == 2)
            {
                Set_to_New_Position(blocks, 1 , 1 , 0 , 0, -1, -1, 1 , -1);
            }
            else if(position == 3)
            {
                Set_to_New_Position(blocks, 1 , -1 , 0 , 0, -1, 1, -1 , -1);
            }
            else if(position == 4)
            {
                position = 0;
                Set_to_New_Position(blocks, -1 , -1 , 0 , 0, 1, 1, -1, 1);
            }
            else if (position == 1)
            {
                Set_to_New_Position(blocks, -1 , 1 , 0 , 0, 1, -1, 1, 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timeRemaining -= Time.deltaTime;

        if(timeRemaining < 0)
        {
            timeRemaining = timer[Level];
            SoftDrop();
        }

        leftmovetime -= Time.deltaTime;
        if(leftmovetime < 0)
        {
            leftmovetime = 0.3f;
            leftmove = true;
        }
        rightmovetime -= Time.deltaTime;
        if(rightmovetime < 0)
        {
            rightmovetime = 0.3f;
            rightmove = true;
        }
        downmovetime -= Time.deltaTime;
        if(downmovetime < 0)
        {
            downmovetime = 0.05f;
            downmove = true;
        }
        upmovetime -= Time.deltaTime;
        if(upmovetime < 0)
        {
            upmovetime = 0.5f;
            upmove = true;
        }

        if(Input.GetKey(KeyCode.LeftArrow) && leftmove && !LeftLimit())
        {
            leftmove = false;
            foreach(GameObject block in enabled_block.ToArray())
            {
                block.transform.position = new Vector3(block.transform.position.x - 1f, block.transform.position.y, 0);
            }
        }
        if(Input.GetKey(KeyCode.RightArrow) && rightmove && !RightLimit())
        {
            rightmove = false;
            foreach(GameObject block in enabled_block.ToArray())
            {
                block.transform.position = new Vector3(block.transform.position.x + 1f, block.transform.position.y, 0);
            }
        }
        if(Input.GetKey(KeyCode.DownArrow) && downmove)
        {
            downmove = false;
            SoftDrop();
            Score++;
            Score_result.text = Score.ToString();
        }

        if(Input.GetKey(KeyCode.UpArrow) && upmove)
        {
            upmove = false;
            Rotate_Tetromino();
        }
    }

    public void Add_enabled_block(GameObject value)
    {
        enabled_block.Add(value);
    }
    public void Set_Enabled_Tetromino(int value)
    {
        enable_tetromino = value;

    }
}
