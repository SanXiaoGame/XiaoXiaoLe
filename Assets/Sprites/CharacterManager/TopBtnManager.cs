using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TopBtnManager : MonoBehaviour {

    public int ID;
    public GameObject characterPoprety;
    public GameObject bag;

    private void Awake()
    {
        characterPoprety = GameObject.FindGameObjectWithTag("characterproprety");
        characterPoprety.SetActive(false);
        GameObject playerShow = GameObject.FindGameObjectWithTag("PlayerShow");
       
        GameObject playerShow0 = GameObject.FindGameObjectWithTag("PlayerShow0");
        playerShow0.SetActive(false);
        GameObject player1 = playerShow0.transform.Find("Player0").gameObject;
        GameObject selectbtn = GameObject.FindGameObjectWithTag("SelectBtn");
        GameObject skillHole = GameObject.FindGameObjectWithTag("SkillHole");
        skillHole.SetActive(false);
         bag = GameObject.FindGameObjectWithTag("Bag");
        bag.SetActive(false);
        GameObject equiptHole = GameObject.FindGameObjectWithTag("EquiptHole");
        equiptHole.SetActive(false);
        GameObject playerCreate = GameObject.FindGameObjectWithTag("PlayerCreate");
        
        
        this.transform.Find("Person").GetComponent<Button>().onClick.AddListener(()=> {
            playerShow0.SetActive(true);
            playerShow.SetActive(false);
           
            skillHole.SetActive(true);
            if (skillHole.GetComponent<SkillManager>() == null)
            {
                skillHole.AddComponent<SkillManager>();
            }
            if (ID != 0)
            {
                string path = string.Format("Texture/Icon0/{0}", SQLiteManager.Instance.characterDataSource[ID].character_Class);
                player1.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                skillHole.GetComponent<SkillManager>().RefreshSkill(ID);
            }
            playerCreate.SetActive(false);
           
            selectbtn.SetActive(false);
            equiptHole.SetActive(false);
            bag.SetActive(false);

        });
        this.transform.Find("Team").GetComponent<Button>().onClick.AddListener(() => {
            if (playerCreate.transform.Find("PlayerCreate").GetComponent<CreateCharacter>() == null)
            {
                playerCreate.transform.Find("PlayerCreate").gameObject.AddComponent<CreateCharacter>();
            }
            selectbtn.SetActive(true);
            playerShow0.SetActive(false);
            skillHole.SetActive(false);
            playerCreate.SetActive(true);
            playerShow.SetActive(true);
          
            equiptHole.SetActive(false);
            bag.SetActive(false);



        });
        this.transform.Find("Bag").GetComponent<Button>().onClick.AddListener(() => {

            playerShow0.SetActive(true);
            selectbtn.SetActive(false);
            if (ID != 0)
            {
                string path = string.Format("Texture/Icon0/{0}", SQLiteManager.Instance.characterDataSource[ID].character_Class);
                player1.GetComponent<Image>().sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
            }
            
            skillHole.SetActive(false);
            playerCreate.SetActive(false);
            playerShow.SetActive(false);
            
            equiptHole.SetActive(true);
            bag.SetActive(true);
            if (bag.GetComponent<CreateItem>() == null)
            {
                bag.AddComponent<CreateItem>();
            }


        });
    }
}
