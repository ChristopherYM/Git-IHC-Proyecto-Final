using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSetup : MonoBehaviour
{
    public static GameSetup GS;
    public int nextPlayersTeam;
    public Text  healthDisplay;
    //public Transform[] spawnPoints;
    public Transform[] spawnPointsTeamOne;
    public Transform[] spawnPointsTeamTwo;
    public Transform[] spawnPointsTeamThree;
    public Transform[] spawnPointsTeamFour;
    public int playersLiving;
    public string[] playerNames; 
    public Text[] playerNameTexts;
    public int[] playerScores;
    public int scoreTotal;
    public Text[] playerScoreText;
    public Transform[] scoreOrder;
    


    public Text[] puntuacion;


    // Start is called before the first frame update
    void Start()
    {
        //UpdatePuntuacion();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        if(GameSetup.GS==null)
        {
            GameSetup.GS=this;
        }
    }

    public void DisconnectPlayer()
    {
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while(PhotonNetwork.IsConnected)
        {
            yield return null;
        }
        SceneManager.LoadScene(MultiplayerSettings.multiplayerSettings.menuScene);
    }
    public void UpdateTeam()
    {
        if(nextPlayersTeam == 1)
        {
            //puntuacion[1].text = PhotonNetwork.NickName;
            nextPlayersTeam= 2;
        }
        else if(nextPlayersTeam == 2)
        {
            //puntuacion[2].text = PhotonNetwork.NickName;
            nextPlayersTeam = 3;
        }
        else if(nextPlayersTeam == 3)
        {
            //puntuacion[3].text = PhotonNetwork.NickName;
            nextPlayersTeam = 4;
        }
    }
    void ScoreBoardUpdate()
    {
        int tempTotal = 0;
        for(int i=0;i<playerScores.Length;i++)
        {
            tempTotal += playerScores[i]; 
        }
        if (tempTotal != scoreTotal)
        {
            OrderUpdate();
            scoreTotal = tempTotal;
            for(int i=0;i<playerScores.Length;i++)
            {
                playerScoreText[i].text = playerScores[i].ToString(); 
            }
        }
    }
    public void OrderUpdate()
    {
        Transform[] order = scoreOrder;
        int[] scores  = playerScores;
        int[] places = {0,0,0};
        for(int i=0; i<scores.Length;i++)
        {
            for(int j=0;j<scores.Length;j++)
            {
                if(scores[i]<scores[j])
                {
                    places[i]++;
                }
            }
        }
        for (int i = 0; i < order.Length; i++)
        {
            order[i].SetSiblingIndex(places[i]);
        }
    }

    public void UpdatePuntuacion()
    {
        
        for(int i=0; i<4;i++)
        {
            string named;
            named = PhotonNetwork.NickName;
            puntuacion[i].text = string.Concat(named , ": 20 ");
        }
        
    }

}


