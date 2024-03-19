using Infrastructure.Level.EventsBus;
using Infrastructure.Level.EventsBus.Signals;
using UI.UIPanels;
using UnityEngine;
using Zenject;

namespace UI
{
    public class UIControl : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private StartMenu _panelMenu;
        [SerializeField] private GamePanel _panelInGame;
        [SerializeField] private WinPanel _panelWin;  
        [SerializeField] private LostPanel _panelLost;
        private IEventBus _eventBus;

        [Inject]
        public void Construct(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        private void Start()
        {
            _eventBus.Subscribe<LevelStart>(OnLevelStart);
            _eventBus.Subscribe<LevelWin>(OnLevelWin);
            _eventBus.Subscribe<LevelLost>(OnLevelLost);

            _panelMenu.ClickedPanel += OnPlayGame;
            _panelLost.ClickedPanel += RestartGame;
            _panelInGame.ClickedPanel += OnPauseGame;
            _panelWin.ClickedPanel += LoadNextLevel;
            StartLevel();
        }

        private void StartLevel()
        {
            HideAllPanels();
            _panelMenu.Show();
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<LevelStart>(OnLevelStart);
            _eventBus.Unsubscribe<LevelWin>(OnLevelWin);
            _eventBus.Unsubscribe<LevelLost>(OnLevelLost);

            _panelMenu.ClickedPanel -= OnPlayGame;
            _panelLost.ClickedPanel -= RestartGame;
            _panelInGame.ClickedPanel -= OnPauseGame;
            _panelWin.ClickedPanel -= LoadNextLevel;
        }

        private void OnLevelLost(LevelLost obj)
        {
            Debug.Log("Level Lost");  
            HideAllPanels();
            _panelLost.Show();
        }

        private void OnLevelWin(LevelWin obj)
        {
            Debug.Log("Level Win"); 
            HideAllPanels();
            _panelWin.Show(); 
        }

        private void OnLevelStart(LevelStart obj)
        {
           StartLevel();
        }

        private void OnPauseGame()
        {
            _eventBus.Invoke(new PauseGame());
        }

        private void OnPlayGame()
        { 
           _eventBus.Invoke(new PlayGame());
            HideAllPanels(); 
            _panelInGame.Show();         
        }
        private void LoadNextLevel()
        {
          _eventBus.Invoke(new LoadNextLevel());
        }

        private void RestartGame()
        {
            _eventBus.Invoke(new RestartScene());
        }

        private void HideAllPanels()
        {
            _panelMenu.Hide();
            _panelLost.Hide();
            _panelWin.Hide();
            _panelInGame.Hide();
        }
    
    }
}
