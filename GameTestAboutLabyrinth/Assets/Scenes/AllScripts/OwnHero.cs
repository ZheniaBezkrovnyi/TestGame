using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OwnHero : MonoBehaviour
{
    public static bool startKill; // много статик потомучто с префабом все
    private NavMeshAgent navMeshAg;
    private static bool clickButton;
    public GameObject animWin;
    private bool animWinBool;
    private void Start()
    {
        animWinBool = true;
        clickButton = false;
        gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color(1, 1, 0);
        startKill = true;
        navMeshAg = GetComponent<NavMeshAgent>();
        Invoke("Way", 2f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Enemy(Clone)" && startKill)
        {
            startKill = false;
            Camera.main.GetComponent<HeroForCamera>().GetAnim();
        }
        if(collision.gameObject.name == "GreenField" && animWinBool)
        {
            animWinBool = false;
            StartCoroutine(animationWin());
        }
    }
    private void Way()
    {
        navMeshAg.SetDestination(Camera.main.GetComponent<Wall>().greenField.transform.position);
    }

    public void OnClick()
    {
        if (!clickButton)
        {
            clickButton = true;
            if (Camera.main.GetComponent<HeroForCamera>().ownHero != null)
            {
                Camera.main.GetComponent<HeroForCamera>().ownHero.GetComponent<Renderer>().sharedMaterial.color = new Color32(179, 255, 47, 255);
                startKill = false;
            }
            Invoke("clButtonForInvoke",2f);
        }
    }
    public void clButtonForInvoke()
    {
        if (Camera.main.GetComponent<HeroForCamera>().ownHero != null)
        {
            Camera.main.GetComponent<HeroForCamera>().ownHero.GetComponent<Renderer>().sharedMaterial.color = new Color(1, 1, 0);
            startKill = true;
        }
    }
    public void OnUp()
    {
        CancelInvoke("clButtonForInvoke");
        gameObject.GetComponent<Renderer>().sharedMaterial.color = new Color(1,1,0);
        startKill = true;
        clickButton = false;
    }
    public IEnumerator animationWin()
    {
        yield return new WaitForSeconds(1f);
        for (int i=0; i < 3; i++)
        {
            Instantiate(animWin, gameObject.transform.position, animWin.transform.rotation);
            yield return new WaitForSeconds(4f/3);
        }
        GameObject panel = Camera.main.GetComponent<UI>().panel;
        panel.GetComponent<Image>().color += new Color32(0, 0, 0, 80);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
