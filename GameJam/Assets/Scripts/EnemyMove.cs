using UnityEngine;

public class EnemyMove : MonoBehaviour

{
    private Transform player;
    public float speed;
    private Transform _transform;

    public bool moving = false;

    private void Start()
    {
        _transform = transform;
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if(moving)
            MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        _transform.LookAt(player.position);
        _transform.position += Time.deltaTime * speed * _transform.forward;

        if (Vector3.Distance(_transform.position, player.position) < 1f)
        {

        }
    }
}