using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.Maps.Unity;
using Microsoft.Geospatial;
using UnityEngine.UI;

public class BackupBarSelector : MonoBehaviour
{
    public GameObject addButton;
    public GameObject removeButton;
    public GameObject nextBarButton;
    public GameObject resultater;
    public GameObject ui;
    public List<GameObject> barer = new List<GameObject>();
    private string[] drinks = new string[] { "Opvarming", "Flaske �l med sidevogn", "Fad�l", "Flaske �l", "Flaske/Lille special �l", "Mojito", "Bl� Thor", "Shot", "Giraf �l", "Lika a Lady/Sex on the beach" };
    private int[] antalT�rer = new int[] { 200, 5, 5, 4, 5, 5, 5, 1, 6, 6 };
    public List<int> personligeT�rer = new List<int> { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
    //public int[] personligeT�rer = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    [SerializeField]
    private int barNr = 0;
    private int nuv�rendeT�re;

    public TMP_Text currentLocationText;
    public TMP_Text currentDrinkText;
    public TMP_Text ParText;
    public TMP_Text barText;
    public TMP_Text drinkText;

    private string savedDrink;

    //Table
    public TMP_Text par1, par2, par3, par4, par5, par6, par7, par8, par9;
    public TMP_Text sip1, sip2, sip3, sip4, sip5, sip6, sip7, sip8, sip9;
    public TMP_Text parSum;
    public TMP_Text sipSum;

    private bool showResults = false;

    // Start is called before the first frame update
    void Start()
    {
        //Updates text first time
        barText.text = "Next bar: " + barer[barNr].name;
        //savedDrink = drinks[Random.Range(0, drinks.Length)];
        savedDrink = drinks[barNr];
        drinkText.text = "Next drink: " + savedDrink;
        Debug.Log(savedDrink);
        //Disables every bar at start
        for (int i = 0; i < barer.Count; i++)
        {
            barer[i].GetComponent<MapPin>().enabled = false;
            barer[i].SetActive(false);
        }
        //Activates first bar
        barer[0].GetComponent<MapPin>().enabled = true;
        barer[0].SetActive(true);


        //Set table par values
        par1.text = antalT�rer[1].ToString();
        par2.text = antalT�rer[2].ToString();
        par3.text = antalT�rer[3].ToString();
        par4.text = antalT�rer[4].ToString();
        par5.text = antalT�rer[5].ToString();
        par6.text = antalT�rer[6].ToString();
        par7.text = antalT�rer[7].ToString();
        par8.text = antalT�rer[8].ToString();
        par9.text = antalT�rer[9].ToString();
        parSum.text = (antalT�rer[1] + antalT�rer[2] + antalT�rer[3] + antalT�rer[4] + antalT�rer[5] + antalT�rer[6] + antalT�rer[7] + antalT�rer[8] + antalT�rer[9]).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (barNr > barer.Count+1)
        {
            nextBarButton.GetComponent<Button>().interactable = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            NextBar();
        }
    }

    public void NextBar()
    {
        barer[barNr].GetComponent<MapPin>().enabled = false;
        barer[barNr].SetActive(false);
        barNr++;
        if (barNr <= barer.Count)
        {
            addButton.SetActive(true);
            removeButton.SetActive(true);
            //Adds current bars sips to list and sets them to 0 again
            if (barNr >= 1)
            {
                personligeT�rer.Add(nuv�rendeT�re);
            }
            nuv�rendeT�re = 0;
            currentLocationText.text = barer[barNr-1].name;
            currentDrinkText.text = savedDrink;
                ParText.text = "T�rer/Par: " + nuv�rendeT�re + "/" + antalT�rer[barNr-1];
                barText.text = "Next bar: " + barer[barNr ].name;
                //savedDrink = drinks[Random.Range(0, drinks.Length)];
                savedDrink = drinks[barNr];
                drinkText.text = "Next drink: " + savedDrink;
                //barNr++;
                barer[barNr].GetComponent<MapPin>().enabled = true;
                barer[barNr].SetActive(true);
                Debug.Log(savedDrink);
                Debug.Log("Next");
        }
        else
        {
            Debug.Log("DONE!!!");
            currentLocationText.text = "Done!!!";
            currentDrinkText.text = "";
            ParText.text = "";
            /*for (int i = 0; i < personligeT�rer.Count; i++)
            {
                personligeT�rer[i]++;
            }*/
            foreach (int nuv�rendeT�re in personligeT�rer)
            {
                Debug.Log(nuv�rendeT�re.ToString());
            }

            ui.SetActive(false);
            resultater.SetActive(true);
            //Set table sip values
            sip1.text = personligeT�rer[1].ToString();
            sip2.text = personligeT�rer[2].ToString();
            sip3.text = personligeT�rer[3].ToString();
            sip4.text = personligeT�rer[4].ToString();
            sip5.text = personligeT�rer[5].ToString();
            sip6.text = personligeT�rer[6].ToString();
            sip7.text = personligeT�rer[7].ToString();
            sip8.text = personligeT�rer[8].ToString();
            sip9.text = personligeT�rer[9].ToString();
            sipSum.text = (personligeT�rer[1] + personligeT�rer[2] + personligeT�rer[3] + personligeT�rer[4] + personligeT�rer[5] + personligeT�rer[6] + personligeT�rer[7] + personligeT�rer[8] + personligeT�rer[9]).ToString();

        }
    }

    public void AddSip()
    {
        nuv�rendeT�re++;
        ParText.text = "T�rer/Par: " + nuv�rendeT�re + "/" + antalT�rer[barNr-1];
    }

    public void RemoveSip()
    {
        nuv�rendeT�re--;
        ParText.text = "T�rer/Par: " + nuv�rendeT�re + "/" + antalT�rer[barNr-1];
    }

    public void ToggleResults()
    {
        //Set table sip values
        sip1.text = personligeT�rer[1].ToString();
        sip2.text = personligeT�rer[2].ToString();
        sip3.text = personligeT�rer[3].ToString();
        sip4.text = personligeT�rer[4].ToString();
        sip5.text = personligeT�rer[5].ToString();
        sip6.text = personligeT�rer[6].ToString();
        sip7.text = personligeT�rer[7].ToString();
        sip8.text = personligeT�rer[8].ToString();
        sip9.text = personligeT�rer[9].ToString();
        sipSum.text = (personligeT�rer[1] + personligeT�rer[2] + personligeT�rer[3] + personligeT�rer[4] + personligeT�rer[5] + personligeT�rer[6] + personligeT�rer[7] + personligeT�rer[8] + personligeT�rer[9]).ToString();
        showResults = !showResults;
        ui.SetActive(showResults);
        resultater.SetActive(!showResults);
    }
}
