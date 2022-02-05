using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroForCamera : MonoBehaviour
{
    public GameObject hero;
    private Wall wallScript;
    public GameObject ownHero;
    public GameObject smallForAnim;
    public List<GameObject> allSpawnSmall = new List<GameObject>();
    private void Start()
    {
        wallScript = Camera.main.GetComponent<Wall>();
        ownHero = Instantiate(hero,new Vector3(-2, hero.transform.localScale.y / 2, wallScript.mainField.transform.localScale.z - 2),Quaternion.identity);
    }
    public void GetAnim()
    {
        StartCoroutine(AnimKillHero());
    }
    public IEnumerator AnimKillHero()
    {
        Vector3 positHero = ownHero.transform.position;
        Destroy(ownHero.gameObject);
        for (int i = 0; i < 20; i++)
        {
            GameObject smallHero = Instantiate(smallForAnim, new Vector3(positHero.x + Random.Range(-1,2)*i/40f, positHero.y + i/40f, positHero.z + Random.Range(-1, 2) * i / 40f), Quaternion.identity);
            allSpawnSmall.Add(smallHero);
        }
        Collider[] colSmallHero = Physics.OverlapSphere(positHero, 5);
        for(int i = 0; i < colSmallHero.Length; i++)
        {
            if (colSmallHero[i].gameObject.name != "Enemy(Clone)")
            {
                Rigidbody rig = colSmallHero[i].attachedRigidbody;
                if (rig != null)
                {
                    rig.AddExplosionForce(250, positHero, 5,1f);
                }
            }
        }
        yield return new WaitForSeconds(3f);
        for(int i = 0; i < allSpawnSmall.Count; i++)
        {
            if (allSpawnSmall[i] != null)
            {
                Destroy(allSpawnSmall[i].gameObject);
            }
        }
        OwnHero.startKill = true;
        ownHero = Instantiate(hero, new Vector3(-2, hero.transform.localScale.y / 2, wallScript.mainField.transform.localScale.z - 2), Quaternion.identity);
    }
}
