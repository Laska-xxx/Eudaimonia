using Core;
using UnityEngine;

namespace Source.Core
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance { get; private set; }
        public GameInput GameInput;
        public ActionMapType CurentActionMapType;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

        public static void Init()
        {
            if (Instance != null)
                return;

            GameObject obj = new GameObject("InputManager");
            Instance = obj.AddComponent<InputManager>();
            DontDestroyOnLoad(obj);
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            GameInput = new GameInput();
            GameInput.Enable();
        }

        private void OnDisable()
        {
            if (Instance == this)
            {
                DisableAllActionMaps();
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                GameInput?.Dispose();
                GameInput = null;
                Instance = null;
            }
        }

        private void DisableAllActionMaps()
        {
            GameInput?.Player.Disable();
            GameInput?.UI.Disable();
        }

        public void SwitchActionMapType(ActionMapType mapType)
        {
            DisableAllActionMaps();
            switch (mapType)
            {
                case ActionMapType.Game:
                    GameInput.Player.Enable();
                    break;
                case ActionMapType.UI:
                    GameInput.UI.Enable();
                    break;
            }

            CurentActionMapType = mapType;
        }
    }

    public enum ActionMapType
    {
        Game,
        UI
    }
}