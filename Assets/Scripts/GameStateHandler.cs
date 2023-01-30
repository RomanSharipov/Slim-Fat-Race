using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateHandler : MonoBehaviour
{
   [SerializeField] private Snowball _snowball;
   [SerializeField] private List<ScoreMultiplayer> _scoreMultiplayers;
   [SerializeField] private WinPanel _winPanel;

   private int _scoreMultiplayer=1;

   private const float WinPanelDellay = 1.5f;
   private const int ScoreForCompliteLevel=10;
   
   private void OnEnable()
   {
      _snowball.Stoped += OnSnowballStoped;
      
      for (int i = 0; i < _scoreMultiplayers.Count; i++)
      {
         _scoreMultiplayers[i].SnowballEntered += OnSnowballEntered;
      }
   }

   private void OnDisable()
   {
      _snowball.Stoped -= OnSnowballStoped;
      
      for (int i = 0; i < _scoreMultiplayers.Count; i++)
      {
         _scoreMultiplayers[i].SnowballEntered -= OnSnowballEntered;
      }
   }

   private void OnSnowballStoped()
   {
      Invoke(nameof(OpenWinPanel),WinPanelDellay);
   }

   private void OpenWinPanel()
   {
      _winPanel.gameObject.SetActive(true);
      _winPanel.ChangeTetx((ScoreForCompliteLevel*_scoreMultiplayer).ToString());
   }

   private void OnSnowballEntered(int scoreMultiplayer)
   {
      if (scoreMultiplayer>_scoreMultiplayer)
      {
         _scoreMultiplayer = scoreMultiplayer;
      }
     
   }
}
