using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _creditsUI;

    public void GoToGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowCredits()
    {
        _creditsUI.SetActive(true);
    }

    public void HideCredits()
    {
        _creditsUI.SetActive(false);
    }
}
