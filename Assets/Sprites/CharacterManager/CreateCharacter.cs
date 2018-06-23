using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CreateCharacter : MonoBehaviour {
    

    GameObject target;
    private void Awake()
    {

        foreach (var item in SQLiteManager.Instance.playerDataSource)
        {
           
            AddCharacter(item.Key);//从角色字典中取出角色ID并加载角色
           
        }
    }
    /// <summary>
    /// 创建角色
    /// </summary>
    /// <param name="ID">角色ID</param>
    public void AddCharacter(int ID) {
        target = Resources.Load("Prefabs/PlayerPrefabs/player") as GameObject;
        GameObject character = Instantiate(target);
        character.transform.SetParent(this.transform);
        character.transform.localPosition = Vector3.zero;
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(this.gameObject.GetComponent<RectTransform>().sizeDelta.x, this.gameObject.GetComponent<RectTransform>().sizeDelta.y + this.gameObject.GetComponent<GridLayoutGroup>().cellSize.y + this.gameObject.GetComponent<GridLayoutGroup>().spacing.y);
        transform.position = new Vector3(transform.position.x, transform.position.y - 30, transform.position.z);
        Character _character = character.AddComponent<Character>();
        _character.Name.text = SQLiteManager.Instance.playerDataSource[ID].player_Name;
        _character.Lv.text = SQLiteManager.Instance.playerDataSource[ID].Level.ToString();
        string path = string.Format("Texture/Icon/Class_{0}",SQLiteManager.Instance.playerDataSource[ID].player_Class);
        _character.HeadPortrait.sprite = Resources.Load(path, typeof(Sprite)) as Sprite;
        _character.ID = ID;
        _character.Type = SQLiteManager.Instance.playerDataSource[ID].player_Class;

        SQLiteManager.Instance.characterLists.Add(character);
    }
}
