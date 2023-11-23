using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private float lookDistance = 5f;
    [SerializeField] private Transform transformToLook;

    private GameObject player;

    private Vector3 startPositon;

    private void Start()
    {
        startPositon = transformToLook.position;
        player = GameManager.instance.playerObject;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < lookDistance)
        {
            transformToLook.LookAt(player.transform);
        }
        else
        {
            //transformToLook.position = startPositon;
        }
    }
}
