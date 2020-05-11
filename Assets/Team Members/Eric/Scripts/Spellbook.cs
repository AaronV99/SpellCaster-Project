using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spellbook : MonoBehaviour
{
    public GameObject[] pages, arrows;
    public int pageOrder;
    public Material[] pageMaterials;
    // Start is called before the first frame update
    void Start()
    {
        UpdatePages(pageOrder);
    }


    public void UpdatePages(int order)
    {
        pageOrder += order;

        if(pageOrder< 0)
        {
            pageOrder = 0;
        }else if(pageOrder > 2)
        {
            pageOrder = 2;
        }
        
        //if ((pageOrder += order) > 0 && (pageOrder += order) < 2)
        //{
        //    pageOrder += order;
        //}
        //else if ((pageOrder += order) > 2)
        //{ 
        //    pageOrder = 2; 
        //}
        //else if ((pageOrder += order) < 0)
        //{
        //    pageOrder = 0;
        //}

        switch (pageOrder)
        {
            case 0:
                if(pages[0].gameObject.activeSelf == false || pages[1].gameObject.activeSelf == false)
                {
                    pages[0].gameObject.SetActive(true);
                    pages[1].gameObject.SetActive(true);
                }
                pages[0].GetComponent<MeshRenderer>().material = pageMaterials[0];
                pages[1].GetComponent<MeshRenderer>().material = pageMaterials[1];
                arrows[0].gameObject.SetActive(false);
                arrows[1].gameObject.SetActive(true);


                break;

            case 1:
                if (pages[0].gameObject.activeSelf == false || pages[1].gameObject.activeSelf == false)
                {
                    pages[0].gameObject.SetActive(true);
                    pages[1].gameObject.SetActive(true);
                }
                pages[0].GetComponent<MeshRenderer>().material = pageMaterials[2];
                pages[1].GetComponent<MeshRenderer>().material = pageMaterials[3];
                arrows[0].gameObject.SetActive(true);
                arrows[1].gameObject.SetActive(true);

                break;

            case 2:
                if (pages[0].gameObject.activeSelf == false || pages[1].gameObject.activeSelf == false)
                {
                    pages[0].gameObject.SetActive(true);
                    pages[1].gameObject.SetActive(true);
                }
                pages[0].GetComponent<MeshRenderer>().material = pageMaterials[4];
                pages[1].gameObject.SetActive(false);
                arrows[0].gameObject.SetActive(true);
                arrows[1].gameObject.SetActive(false);

                break;
        }
    }
}
