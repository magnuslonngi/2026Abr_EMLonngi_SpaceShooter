using UnityEngine;

[RequireComponent(typeof(EnemyMovement))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyBounce : MonoBehaviour
{
    [SerializeField] private bool _bounceHorizontal;
    [SerializeField] private bool _bounceVertical;

    [SerializeField] private float _upLimit;
    [SerializeField] private float _downLimit;
    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;

    private EnemyMovement _enemyMovement;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (_bounceHorizontal) HorizontalBounce();

        if (_bounceVertical) VerticalBounce();
    }

    // TODO: I rotate the parent game object so the axis are inverted...
    // Maybe change the sprite renderer to a child game object later
    private void HorizontalBounce()
    {
        if (_enemyMovement.GetCurrentDirection().x < 0 && transform.position.x < _leftLimit)
        {
            _enemyMovement.FlipHorizontalDirection();
            _spriteRenderer.flipY = !_spriteRenderer.flipY;
        }

        if (_enemyMovement.GetCurrentDirection().x > 0 && transform.position.x > _rightLimit)
        {
            _enemyMovement.FlipHorizontalDirection();
            _spriteRenderer.flipY = !_spriteRenderer.flipY;
        }
    }
    
    private void VerticalBounce()
    {
        if (_enemyMovement.GetCurrentDirection().y < 0 && transform.position.y < _downLimit)
        {
            _enemyMovement.FlipVerticalDirection();
        }

        if (_enemyMovement.GetCurrentDirection().y > 0 && transform.position.y > _upLimit)
        {
            _enemyMovement.FlipVerticalDirection();
        }
    }
}
