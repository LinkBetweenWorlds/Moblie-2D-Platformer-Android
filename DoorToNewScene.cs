using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorToNewScene : MonoBehaviour
{
    private Player player;
    [SerializeField]
    private string nextScene;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();
            if(player != null)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }
}
