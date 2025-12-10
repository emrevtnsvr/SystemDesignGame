using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
  public void ToGameplay()
  {
        SceneManager.LoadScene("GameplayScene");
  }
}
