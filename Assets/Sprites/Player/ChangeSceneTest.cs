using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneTest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine("Wait");
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);
        Debug.Log(SQLiteManager.Instance.playerDataSource[1301].player_Name) ;

    }

    public void ChangSecene()
    {
        SceneManager.LoadScene("SkillTest");
    }
}
