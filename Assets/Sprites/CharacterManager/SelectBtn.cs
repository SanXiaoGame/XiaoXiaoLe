using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SelectBtn : MonoBehaviour {
    [HideInInspector]
    public string characterName;//选择的角色名字
    [HideInInspector]
    public int DelctID;
    [HideInInspector]
    public int ID { get; set; }//角色ID
    Transform[] pos = new Transform[5];//战斗的五个角色位置
    bool isOver = false;
    bool isCome = false;
    GameObject target;
    private void Awake()
    {
        
        GameObject[] gameObject = GameObject.FindGameObjectsWithTag("Charater1");//战斗的位置查找
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = gameObject[i].transform;//添加战斗位置
        }
       //加载角色职位头像
         target = Resources.Load("Prefabs/PlayerPrefabs/HeadPortrait") as GameObject;

        
        //提交选择的玩家
        this.transform.Find("Submit").GetComponent<Button>().onClick.AddListener(()=> {
            for (int i = 0; i < pos.Length; i++)
            {
                if (pos[i].childCount == 0)//查找战斗位置是否有角色
                {
                    isOver = true;
                }
            }
          //判断是否有选择角色 战斗位置是否包含了该角色的职业
            if (ID != 0&&isOver& !SQLiteManager.Instance.team.ContainsKey(ID))
            {

                CharacterSelcet();
            }
           
        });
        //移除战斗位置的玩家
        this.transform.Find("Delet").GetComponent<Button>().onClick.AddListener(() => {
            if (isCome)
            {
                SQLiteManager.Instance.team.Remove(DelctID);
                Destroy(GameObject.Find(characterName));
                isCome = false;
            }
            

        });
        this.transform.Find("Centain").GetComponent<Button>().onClick.AddListener(() => {
         
        });
    }
    /// <summary>
    /// 显示  选上的角色头像在准备战斗的舞台上
    /// </summary>
    void CharacterSelcet() {

        string path = string.Format("Texture/Icon/Cube_{0}", SQLiteManager.Instance.playerDataSource[ID].player_Class);
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
        HeroAdd();
        isCome = true;
    }
    /// <summary>
    /// 存放上战场的角色
    /// </summary>
    void HeroAdd() {
        Hero hero = new Hero();
        PlayerData playerData = SQLiteManager.Instance.playerDataSource[ID];
        hero.playerData = playerData;
        hero.currentAD = playerData.AD;
        hero.currentAP = playerData.AP;
        hero.currentHP = playerData.HP;
        hero.currentDEF = playerData.DEF;
        hero.currentRES = playerData.RES;
        hero.skill1 = SQLiteManager.Instance.skillDataSource[playerData.skillOneID];
        hero.skill2 = SQLiteManager.Instance.skillDataSource[playerData.skillTwoID];
        hero.skill3 = SQLiteManager.Instance.skillDataSource[playerData.skillThreeID];

        SQLiteManager.Instance.team.Add(ID, hero);


    }
}
