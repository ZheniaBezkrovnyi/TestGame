using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public GameObject wall;
    public static int[,] pozit;
    public int numberSide;
    public int wideWall;
    public GameObject mainField;
    public GameObject greenField;
    private void Awake()
    {
        wideWall = (int)wall.transform.localScale.z;
        mainField.transform.localScale = new Vector3(numberSide * wideWall + 40, 1, numberSide * wideWall);
        mainField.transform.position = new Vector3(numberSide * wideWall / 2, -0.5f, numberSide * wideWall / 2 - wideWall / 2f);
    }
    private void Start()
    {
        pozit = new int[numberSide, numberSide]; // 2 в массиве это открытая точка, 1 доп, просто продолжение хода
        greenField.transform.position = new Vector3(wideWall*numberSide + 3,0,2);
        StartCoroutine(SpawnWall());
    }
    public IEnumerator FullPosit()
    {
        yield return null;
        for(int x = 0; x < pozit.GetLength(0); x++)
        {
            if (x == 0)
            {
                for (int z = 0; z < pozit.GetLength(1); z++)
                {
                    if (Random.Range(0, Random.Range(3,3 + (int)(numberSide/15))) != 1) //количество начальных открытых путей посчитает
                    {
                        continue;
                    }
                    pozit[x,z] = 2;
                }
            }
            else
            {
                for (int z = 0; z < pozit.GetLength(1); z++)
                {
                    if (pozit[x-1,z] != 2)
                    {
                        if(pozit[x - 1, z] == 0 && Random.Range(0,8) == 4)
                        {
                            pozit[x - 1, z] = 1;
                        }
                        continue;
                    }
                    else
                    {
                        int rand = Random.Range(-4, 4);
                        pozit[x,z] = 1;
                        for (int possible = 0; possible<5;) // нельзя выходить за поле
                        {
                            rand = rand > 0 ? rand - possible : rand + possible;
                            if (z + rand < pozit.GetLength(1) && z + rand >= 0)
                            {
                                pozit[x, z + rand] = 2;
                                for (int r = 1; r < Mathf.Abs(rand); r++) //чтобы сразу закрыть поворот клетками
                                {
                                    pozit[x, z + (rand > 0 ? rand - r : rand + r)] = 1;
                                }
                                break;
                            }
                            else
                            {
                                possible++;
                            }
                        }
                    }
                }
            }
        }
    }
    public IEnumerator SpawnWall()
    {
        yield return StartCoroutine(FullPosit());
        for (int x = 0; x < pozit.GetLength(0); x++)
        {
            yield return new WaitForSeconds(1f / 200);
            for (int z = 0; z < pozit.GetLength(1); z++)
            {
                GameObject newWall = Instantiate(wall, new Vector3(x* wideWall, 1f/2, z * wideWall), Quaternion.identity);
                #region
                if (pozit[x, z] == 0)
                {
                    if (x != 0)
                    {
                        newWall.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (z != 0)
                    {
                        newWall.transform.GetChild(1).gameObject.SetActive(false);
                    }
                    if (z != pozit.GetLength(1) - 1)
                    {
                        newWall.transform.GetChild(2).gameObject.SetActive(false);
                    }
                    if (x != pozit.GetLength(0) - 1)
                    {
                        newWall.transform.GetChild(3).gameObject.SetActive(false);
                    }
                    continue;
                }
                else
                {
                    if (x == 0)
                    {
                        newWall.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (x == pozit.GetLength(0) - 1)
                    {
                        newWall.transform.GetChild(3).gameObject.SetActive(false);
                    }
                }
                #endregion
                for (int numberChild = 0, sideX = (x == 0 ? 0 : -1); sideX <= (x == pozit.GetLength(0) - 1 ? 0 : 1); sideX++)
                {
                    for (int sideZ = (z == 0 ? 0 : -1); sideZ <= (z == pozit.GetLength(0) - 1 ? 0 : 1); sideZ++)
                    {
                        if (x == 0 && numberChild == 0) //чтобы дочерние не сбились
                        {
                            numberChild++;
                        }
                        if (sideX == sideZ || sideX + sideZ == 0) { continue; } // нужны только боковые
                        if (pozit[x + sideX, z + sideZ] == 1 || pozit[x + sideX, z + sideZ] == 2)
                        {
                            newWall.transform.GetChild(numberChild).gameObject.SetActive(false);
                        }
                        numberChild++;
                        if((z==0 && numberChild == 1) || (z == pozit.GetLength(0) - 1 && numberChild == 2)) //чтобы дочерние не сбились
                        {
                            numberChild++;
                        }
                    }
                }
            }
        }
    }
    
}
