using UnityEngine;

public class EnemyMove : MonoBehaviour

{
    private Transform player;
    public float speed;
    private Transform _transform;

    private void Start()
    {
        _transform = transform;
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        Vector3 target = new Vector3(player.position.x, _transform.position.y, player.position.z);
        _transform.LookAt(target);
        _transform.position += Time.deltaTime * speed * _transform.forward;
    }
}