using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Unity.FPS.UI
{
    public class LoadSceneButton : MonoBehaviour
    {
        public string SceneName = "";
        public Button Button;

        void Start()
        {
            if (Button != null)
            {
                Button.onClick.AddListener(OnButtonClicked);
            }
        }

        void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneName);
            }
        }

        public void OnButtonClicked()
        {
            SceneManager.LoadScene(SceneName);
        }

        /*void Update()
        {
            if (EventSystem.current.currentSelectedGameObject == gameObject
                && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
            {
                LoadTargetScene();
            }
        }

        public void LoadTargetScene()
        {
            SceneManager.LoadScene(SceneName);
        }*/
    }
}