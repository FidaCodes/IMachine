using UnityEngine;

namespace Unity.FPS.UI
{
    public class ExitConfirmationUI : MonoBehaviour
    {
        public GameObject MainMenuPanel; // Assign this to your MainMenuPanel object in the Inspector

        //a function to quit the application when the users click on yes button
        public void OnYesClicked()
        {
            //closing the application
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }

        //a function to cancel and go back to the main menu, when the user clicks on no button
        public void OnNoClicked()
        {
            gameObject.SetActive(false);
            if (MainMenuPanel != null)
            {
                MainMenuPanel.SetActive(true);
            }
        }
    }
}
