using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public Animator Cards, person1, person2, person3, person4, person5, person6;
    public List<int> arr;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

    public void game_start()
    {
        button.interactable = false;
        StartCoroutine(card_init());
        int rand = Random.Range(2, 6);
        for (int i = 0; i < rand;)
        {
            int m = Random.Range(0, 5);
            if (arr.Contains(m) == false)
            {
                arr.Add(m);
                i++;
            }
        }
        arr.Sort();
        StartCoroutine(card_anim_start(arr));
    }

    private IEnumerator card_anim_start(List<int> param)
    {
        Cards.SetBool("flag", true);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < param.Count; i++)
        {
            switch (param[i])
            {
                case 0:
                    person1.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 1:
                    person2.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 2:
                    person3.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 3:
                    person4.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 4:
                    person5.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
                case 5:
                    person6.SetBool("flag", true);
                    yield return new WaitForSeconds(0.8f);
                    break;
            }
        }
        yield return new WaitForSeconds(0.2f);
        Cards.SetBool("flag", false);
        arr.Clear();
        yield return new WaitForSeconds(1f);
        button.interactable = true;
    }

    private IEnumerator card_init()
    {
        person1.SetBool("flag", false);
        person2.SetBool("flag", false);
        person3.SetBool("flag", false);
        person4.SetBool("flag", false);
        person5.SetBool("flag", false);
        person6.SetBool("flag", false);
        Cards.SetBool("flag", false);
        yield return new WaitForSeconds(0.5f);
    }
}