using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//显示酒馆可以招募的三个角色
public class DrunkeryCharacterShow : MonoBehaviour {
    Image _character1;
    Image _character2;
    Image _character3;
    int _id1;
    int _id2;
    int _id3;
    GameObject characterMessage;
    bool isOver = false;
    private void Awake()
    {

       
        _character1 = this.transform.Find("Character1").GetComponent<Image>();
        _character2 = this.transform.Find("Character2").GetComponent<Image>();
        _character3 = this.transform.Find("Character3").GetComponent<Image>();

        this.transform.Find("Character1").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Character2").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();
        this.transform.Find("Character3").GetComponent<Toggle>().group = this.GetComponent<ToggleGroup>();

        GameObject character1 = this.transform.Find("Character1/character1").gameObject;
        character1.SetActive(false);
        GameObject character2 = this.transform.Find("Character2/character2").gameObject;
        character2.SetActive(false);
        GameObject character3 = this.transform.Find("Character3/character3").gameObject;
        character3.SetActive(false);


        this.transform.Find("Character1").GetComponent<Toggle>().onValueChanged.AddListener((isOn)=> {
        characterMessage = GameObject.FindGameObjectWithTag("TopBtn").GetComponent<DrunkeryTopBtn>().characterMessage;

            characterMessage.SetActive(true);
            string type;
            if (isOver)
            {
                type = "1级稀有";
            }
            else
            {
                type = "1级普通";
            }
            characterMessage.GetComponent<DrunkeryCharacterMessage>().ShowCharacterMesaage(_id1,type);
            character1.SetActive(isOn);
            

        });
        this.transform.Find("Character2").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            characterMessage = GameObject.FindGameObjectWithTag("TopBtn").GetComponent<DrunkeryTopBtn>().characterMessage;

            characterMessage.SetActive(true);
            characterMessage.GetComponent<DrunkeryCharacterMessage>().ShowCharacterMesaage(_id2,"2级普通");
            character2.SetActive(isOn);
        });
        this.transform.Find("Character3").GetComponent<Toggle>().onValueChanged.AddListener((isOn) => {
            characterMessage = GameObject.FindGameObjectWithTag("TopBtn").GetComponent<DrunkeryTopBtn>().characterMessage;

            characterMessage.SetActive(true);
            characterMessage.GetComponent<DrunkeryCharacterMessage>().ShowCharacterMesaage(_id3,"2级稀有");
            character3.SetActive(isOn);
        });


    }
    /// <summary>
    /// 显示角色
    /// </summary>
    /// <param name="id1">1级角色id</param>
    /// <param name="id2">2级普通角色id</param>
    /// <param name="id3">2级稀有角色id</param>
    public void ShowCharacter(int id1,int id2,int id3,bool isRare) {
        _id1 = id1;
        _id2 = id2;
        _id3 = id3;
        isOver = isRare;
        string path1 = string.Format("Texture/Icon/Cube_{0}",SQLiteManager.Instance.characterDataSource[id1].character_Class);
        string path2 = string.Format("Texture/Icon/Cube_{0}", SQLiteManager.Instance.characterDataSource[id2].character_Class);
        string path3 = string.Format("Texture/Icon/Cube_{0}", SQLiteManager.Instance.characterDataSource[id3].character_Class);
        _character1.sprite = Resources.Load(path1, typeof(Sprite)) as Sprite;
        _character2.sprite = Resources.Load(path2, typeof(Sprite)) as Sprite;
        _character3.sprite = Resources.Load(path3, typeof(Sprite)) as Sprite;

    }

}
