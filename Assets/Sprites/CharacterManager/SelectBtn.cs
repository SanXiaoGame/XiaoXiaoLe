using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectBtn : MonoBehaviour {
    public string characterName;
    public int ID { get; set; }
    Transform[] pos = new Transform[5];
    private void Awake()
    {
        
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("Charater1");
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = gameObject[i].transform;
        }
       
        GameObject target = Resources.Load("Prefabs/PlayerPrefabs/HeadPortrait") as GameObject;

        bool isOver = false;
        bool isCome = false;
        this.transform.Find("Submit").GetComponent<Button>().onClick.AddListener(()=> {
            for (int i = 0; i < pos.Length; i++)
            {
                if (pos[i].childCount == 0)
                {
                    isOver = true;
                }
            }
           // Debug.Log(!SQLiteManager.Instance.character.ContainsKey(SQLiteManager.Instance.characterDataSource[ID].character_Class));
            if (ID != 0&&isOver& !SQLiteManager.Instance.character.ContainsKey(SQLiteManager.Instance.characterDataSource[ID].character_Class))
            {
                
                string path = string.Format("Texture/Icon0/{0}", SQLiteManager.Instance.characterDataSource[ID].character_Class);
                Sprite sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
                GameObject @object = Instantiate(target);
                @object.GetComponent<Image>().sprite = sprite;
                @object.name = sprite.name;
                HeadPortrait headPortrait = @object.AddComponent<HeadPortrait>();
                headPortrait.ID = ID;
                for (int i = 0; i < pos.Length; i++)
                {
                    if (pos[i].childCount == 0)
                    {
                        @object.transform.parent = pos[i];
                        @object.transform.localPosition = Vector3.zero;
                    }
                }
                SQLiteManager.Instance.character.Add(SQLiteManager.Instance.characterDataSource[ID].character_Class, ID);
                isCome = true;
            }
           
        });
        this.transform.Find("Delet").GetComponent<Button>().onClick.AddListener(() => {
            if (isCome)
            {
                SQLiteManager.Instance.character.Remove(GameObject.Find(characterName).GetComponent<Image>().sprite.name);
                Destroy(GameObject.Find(characterName));
                isCome = false;
            }
            

        });
        this.transform.Find("Centain").GetComponent<Button>().onClick.AddListener(() => {
         
        });
    }
}
