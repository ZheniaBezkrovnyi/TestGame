using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject enemy;
    private Wall wallScript;
    private void Start()
    {
        wallScript = Camera.main.GetComponent<Wall>();
        StartCoroutine(SpawnEnemy());
    }
    public IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(3f);

        for (; ; )
        {
            yield return new WaitForSeconds(1f);
            Vector3 positEnemy = new Vector3();
            for (; ; ) {
                int randX = Random.Range(0, Wall.pozit.GetLength(0));
                int randZ = Random.Range(0, Wall.pozit.GetLength(1));
                if (Wall.pozit[randX, randZ] == 1 ||
                    Wall.pozit[randX, randZ] == 2)
                {

                    positEnemy = new Vector3(randX* wallScript.wideWall, enemy.transform.localScale.y/2,randZ * wallScript.wideWall);
                    break;
                }
            }
            Instantiate(enemy,positEnemy,Quaternion.identity);
        }
    }
}
