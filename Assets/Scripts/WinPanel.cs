using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WinPanel : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI _text;

  public void ChangeTetx(string text)
  {
    _text.text = text;
  }
}
