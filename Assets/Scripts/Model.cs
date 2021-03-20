using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{
    public GameObject[] Characters;
    public GameObject[] Heads;
    public GameObject[] BackPacks;
    int Which;
    // Start is called before the first frame update
    void Start()
    {

        Refresh();
      //  UnlockRandPlayer();
    }
    public void Refresh()
    {
        PlayerPrefs.SetInt("Character" + 0.ToString(), 1);
        if (PlayerPrefs.GetInt("AllCharacters") == 1)
        {
            for (int i = 0; i < Characters.Length; i++)
            {
                if (!Characters[i].GetComponent<Char>().IsZombie)
                {
                    PlayerPrefs.SetInt("Character" + i.ToString(), 1);
                }
            }
        }
        if (PlayerPrefs.GetInt("AllZombies") == 1)
        {
            for (int i = 0; i < Characters.Length; i++)
            {
                if (Characters[i].GetComponent<Char>().IsZombie)
                {
                    PlayerPrefs.SetInt("Character" + i.ToString(), 1);
                }
            }
        }
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);
            if (PlayerPrefs.GetInt("Character" + i.ToString()) == 1)
            {
                Characters[i].GetComponent<Char>().Unlocked = true;
            }
            else
            {
                Characters[i].GetComponent<Char>().Unlocked = false;
            }

        }
        Which = PlayerPrefs.GetInt("SelectedCharacter");
        ChangeChar(Which);
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("Character" + Which.ToString()) == 1)
        {
            GetComponentInParent<Player>().TheMan.LockedObj.SetActive(false);
        }
        else
        {
            GetComponentInParent<Player>().TheMan.LockedObj.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("Character" + Which.ToString(), 1);
        }
        if (Heads[Which].activeSelf == false)
        {
            for(int i = 0; i < Heads.Length; i++)
            {
                Heads[i].SetActive(false);
            }
            Heads[Which].SetActive(true);
        }
        if (BackPacks[Which].activeSelf == false)
        {
            for (int i = 0; i < BackPacks.Length; i++)
            {
               BackPacks[i].SetActive(false);
            }
           BackPacks[Which].SetActive(true);
        }
    }
    public void UnlockRandPlayer(bool IsZombie)
    {
        int rand = 0;
        int count=0;
        for(int i = 0; i < Characters.Length; i++)
        {
            if (IsZombie)
            {
                if (PlayerPrefs.GetInt("Character" + i.ToString()) == 0&&
                    Characters[i].GetComponent<Char>().IsZombie)
                {
                    count += 1;
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("Character" + i.ToString()) == 0 &&
                   !Characters[i].GetComponent<Char>().IsZombie)
                {
                    count += 1;
                }
            }
        }
        if (count !=0)
        {
            if (IsZombie)
            {
                Debug.Log("IsZombie");
                while (PlayerPrefs.GetInt("Character" + rand.ToString()) == 1 ||
                   Characters[rand].GetComponent<Char>().IsZombie==false)
                {
                    rand = Random.Range(0, Characters.Length);
                }
            }
            else
            {
                Debug.Log("NotZombie");
                Debug.Log("Count"+count.ToString());
                while (PlayerPrefs.GetInt("Character" + rand.ToString()) == 1 ||
                  Characters[rand].GetComponent<Char>().IsZombie==true)
                {
                    rand = Random.Range(0, Characters.Length);
                }
            }
            
        }
        if(PlayerPrefs.GetInt("Character" + rand.ToString()) == 0)
        {
            ChangeChar(rand);
            GetComponentInParent<Player>().TheMan.UnlockRandomPlayer();
        }
    }
    public void ChangeChar(int TWhich)
    {
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);
        }
        PlayerPrefs.SetInt("Character" + TWhich.ToString(), 1);
        PlayerPrefs.SetInt("SelectedCharacter", TWhich);
        Characters[TWhich].SetActive(true);
        Which = TWhich;
    }
    public void Next()
    {
        /* int Prev = Which;
         Which += 1;
         while (PlayerPrefs.GetInt("Character" + Which.ToString()) == 0)
         {
             Which += 1;
             if (Which > Characters.Length - 1)
             {
                 Which = 0;
             }
         }

         for (int i = 0; i < Characters.Length; i++)
         {
             Characters[i].SetActive(false);

         }
         if (Which > Characters.Length-1)
         {
             Which = 0;
         }
         Characters[Which].SetActive(true);
         PlayerPrefs.SetInt("SelectedCharacter", Which);*/
        Which += 1;
        if (Which > Characters.Length - 1)
        {
            Which = 0;
        }
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);

        }
        Characters[Which].SetActive(true);
        if(PlayerPrefs.GetInt("Character" + Which.ToString()) == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", Which); 
        }

    }
    public void Prev()
    {

        /* Which -= 1;
         while (PlayerPrefs.GetInt("Character" + Which.ToString()) == 0)
         {
             Which -= 1;
             if (Which < 0)
             {
                 Which = Characters.Length - 1;
             }
         }
         for (int i = 0; i < Characters.Length; i++)
         {
             Characters[i].SetActive(false);

         }
         if (Which <0)
         {
           Which= Characters.Length - 1;
         }
         Characters[Which].SetActive(true);
         PlayerPrefs.SetInt("SelectedCharacter", Which);*/
        Which -= 1;
        if (Which < 0)
        {
            Which = Characters.Length - 1;
        }
        for (int i = 0; i < Characters.Length; i++)
        {
            Characters[i].SetActive(false);

        }
        Characters[Which].SetActive(true);
        if (PlayerPrefs.GetInt("Character" + Which.ToString()) == 1)
        {
            PlayerPrefs.SetInt("SelectedCharacter", Which);
        }

    }

}
