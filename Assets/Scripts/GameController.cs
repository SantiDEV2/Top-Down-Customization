using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool isGamePaused = false;

    [Header("References")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject houseGrid;
    [SerializeField] private GameObject environmentGrid;
    [SerializeField] private Transform[] spawnPos;

    private void OnEnable()
    {
        EventManager.StartListening("SceneChange", ChangeEnvironment);
    }

    private void OnDisable()
    {
        EventManager.StopListening("SceneChange", ChangeEnvironment);
    }

    private void ChangeEnvironment(object param)
    {
        if (houseGrid.activeInHierarchy == true && environmentGrid.activeInHierarchy == false)
        {
            houseGrid.SetActive(false);
            environmentGrid.SetActive(true);
            player.transform.position = spawnPos[0].transform.position;
        }
        else if (houseGrid.activeInHierarchy == false && environmentGrid.activeInHierarchy == true)
        {
            environmentGrid.SetActive(false);
            houseGrid.SetActive(true);
            player.transform.position = spawnPos[1].transform.position;
        }
    }
}
