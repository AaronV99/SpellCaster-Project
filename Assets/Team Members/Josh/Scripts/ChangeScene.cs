using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{

    [SerializeField] private string loadLevel;
    [SerializeField] bool isTouching;
    [SerializeField] private float time = 3f;

    public Text counterText;
    public GameObject textCanvas;
    public Animator transitionAnim;
    //public Animator textAnim;


    void Start()
    {
        textCanvas.SetActive(false);
        //objectGrabbed = GetComponent<OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isTouching == true)
        {
            if(time > 0)
            {
                time -= Time.deltaTime;
            }
        }
        else if(isTouching == false)
        {
            time = 3;
        }
    }


    void OnTriggerStay(Collider other)
    {
        isTouching = true;

            
            if (other.gameObject.tag == "Character")
            {
            textCanvas.SetActive(true);
            //textAnim.SetTrigger("change");
            counterText.text = time.ToString("f0");

            if (time <= 0)
                {
                StartCoroutine(LoadScene());
                }
             }
          
        
        
    }

    void OnTriggerExit(Collider other)
    {
        isTouching = false;
        textCanvas.SetActive(false);
    }

    IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(loadLevel);

    }

}
