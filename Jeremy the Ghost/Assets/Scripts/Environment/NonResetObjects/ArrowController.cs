using UnityEngine;

public class ArrowController : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera1;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera camera2;
    
    [SerializeField] private GameObject noGoingBackCollider;

    [SerializeField] private bool hideMoves = false;
    [SerializeField] private bool setNewMax = false;
    [SerializeField] private int newMovesMax = 0;

    [SerializeField] private GameObject newRespawn;

    private void OnTriggerEnter2D(Collider2D other)
    {
        camera1.Priority = 0;
        camera2.Priority = 100;
        noGoingBackCollider.SetActive(true);
        if (hideMoves)
        {
            GameObject.Find("MoveController").GetComponent<MoveController>().DisableMoveCounter();
        }
        else if (setNewMax)
        {
            GameObject.Find("MoveController").GetComponent<MoveController>().SetMax(newMovesMax);
        }
        if (newRespawn is not null)
        {
            GameObject.Find("JeremyParent").GetComponentInChildren<JeremyController>().puzzleRespawn = newRespawn.transform;
        }
    }
}
