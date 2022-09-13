using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Lvl")
        {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        }
    }
}
 


  