using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    #region Singleton
    //instance
    private static GameAssets _instance;

    public static GameAssets Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Instantiate(Resources.Load("GameAssets") as GameObject).GetComponent<GameAssets>();
            }
            return _instance;
        }
    }
    #endregion

    #region References
    public GameObject PFb_PlayerProjectile;
    public GameObject PFb_EnemyProjectile;
    public GameObject PFb_TopEnemy;
    public GameObject PFb_MiddleEnemy;
    public GameObject PFb_BottomEnemy;
    public GameObject PFb_Ufo;
    #endregion

}
