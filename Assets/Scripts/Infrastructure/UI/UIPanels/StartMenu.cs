using System;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.UIPanels
{
    public class StartMenu : PanelBase
    {
        [SerializeField] private Text _timer;
        [SerializeField] private int _timerCount;

        private int _time;
        public override event Action ClickedPanel;

        private void OnEnable()
        {
            _timer.DOCounter(_timerCount,0,_timerCount)
                  .OnComplete(() =>
                  {
                      ClickedPanel?.Invoke();
                  });
        }
    }
}