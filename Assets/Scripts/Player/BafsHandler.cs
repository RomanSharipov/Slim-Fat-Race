
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BafsHandler : MonoBehaviour
{
   [SerializeField] private Player _player;
   [SerializeField] private BafIcon _skiIcon;
   [SerializeField] private int _bafDuration;
   [SerializeField] private List<GameObject> _ski = new List<GameObject>();
   [SerializeField] private InputOnPlane _inputOnPlane;
   [SerializeField] private InputOnBridge _inputOnBridge;
   [SerializeField] private InputOnElevator _inputOnElevator;
   [SerializeField] private float _speedOnSki;

   private Coroutine _skiCoroutine;


   private bool _skiUsed;
   private bool _superFastRunUsed;


   public void UseSki()
   {
       if ( _skiUsed)
       {
          StopCoroutine(_skiCoroutine);
       }
        _skiUsed = true;
        _skiCoroutine = StartCoroutine(StopSki());
        _skiIcon.StartReference(_bafDuration);
        TurnSki(true);
        ChangeSpeed(_speedOnSki);
   }

  

   private IEnumerator StopSki()
   {
       yield return new  WaitForSeconds(_bafDuration);
       _skiUsed = false;
       _skiIcon.StopReference();
       TurnSki(false);
       ChangeSpeed();
   }

   private void TurnSki(bool activeSelf)
   {
       for (int i = 0; i < _ski.Count; i++)
       {
           _ski[i].SetActive(activeSelf);
       }
   }

   private void ChangeSpeed(float speed=0)
   {
       if (speed==0)
       {
           _inputOnBridge.ReturnStartSpeed();
           _inputOnElevator.ReturnStartSpeed();
           _inputOnPlane.ReturnStartSpeed();
       }
       else
       {
           _inputOnBridge.ChangeSpeed(speed);
           _inputOnElevator.ChangeSpeed(speed);
           _inputOnPlane.ChangeSpeed(speed);
       }
   }
}
