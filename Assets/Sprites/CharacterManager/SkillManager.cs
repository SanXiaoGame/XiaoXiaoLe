using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillManager : MonoBehaviour {
    Text _name;//技能名字
    Text _type;//技能类型
    Text _effect;//技能效果
    Text _condition;//释放技能条件
    int skill1;//技能1
    int skill2;//技能2
    int skill3;//技能3
    /// <summary>
    /// 玩家技能信息刷新
    /// </summary>
    /// <param name="ID"></param>
    public void RefreshSkill(int ID) {
        skill1 = SQLiteManager.Instance.playerDataSource[ID].skillOneID;
        skill2 = SQLiteManager.Instance.playerDataSource[ID].skillTwoID;
        skill3 = SQLiteManager.Instance.playerDataSource[ID].skillThreeID;
        string pathSkill1 = string.Format("Texture/Icon/{0}", skill1);
        string pathSkill2 = string.Format("Texture/Icon/{0}", skill2);
        string pathSkill3 = string.Format("Texture/Icon/{0}", skill3);

        this.transform.Find("Skill1").GetComponent<Image>().sprite = Resources.Load(pathSkill1, typeof(Sprite)) as Sprite;
        this.transform.Find("Skill2").GetComponent<Image>().sprite = Resources.Load(pathSkill2, typeof(Sprite)) as Sprite;
        this.transform.Find("Skill3").GetComponent<Image>().sprite = Resources.Load(pathSkill3, typeof(Sprite)) as Sprite;

    }
    private void Awake()
    {
        GameObject skillProproty = this.transform.Find("SkillProprety").gameObject;//技能信息面板
        _name = skillProproty.transform.Find("Name").GetComponent<Text>(); 
        _type = skillProproty.transform.Find("Type").GetComponent<Text>();
        _effect = skillProproty.transform.Find("Effect").GetComponent<Text>();
        _condition = skillProproty.transform.Find("Condition").GetComponent<Text>();
        skillProproty.SetActive(false);

        this.transform.Find("Skill1").GetComponent<Button>().onClick.AddListener(()=> {
            if (skill1 != 0)
            {
                skillProproty.SetActive(true);
                _name.text = SQLiteManager.Instance.skillDataSource[skill1].skill_Name;
                _type.text = SQLiteManager.Instance.skillDataSource[skill1].skill_Type;
                _effect.text = SQLiteManager.Instance.skillDataSource[skill1].skill_Description;
            }
        });
        this.transform.Find("Skill2").GetComponent<Button>().onClick.AddListener(() => {
            if (skill2 != 0)
            {
                skillProproty.SetActive(true);
                _name.text = SQLiteManager.Instance.skillDataSource[skill2].skill_Name;
                _type.text = SQLiteManager.Instance.skillDataSource[skill2].skill_Type;
                _effect.text = SQLiteManager.Instance.skillDataSource[skill2].skill_Description;

            }


        });
        this.transform.Find("Skill3").GetComponent<Button>().onClick.AddListener(() => {
            if (skill3 != 0)
            {
                skillProproty.SetActive(true);
                _name.text = SQLiteManager.Instance.skillDataSource[skill3].skill_Name;
                _type.text = SQLiteManager.Instance.skillDataSource[skill3].skill_Type;
                _effect.text = SQLiteManager.Instance.skillDataSource[skill3].skill_Description;

            }

        });
    }
    
}
