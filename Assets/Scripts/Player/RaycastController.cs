using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    public LayerMask collisionMask;
    [HideInInspector] public int horizontalRayCount;
    [HideInInspector] public int verticalRayCount;
    [HideInInspector] public float horizontalRaySpacing;
    [HideInInspector] public float verticalRaySpacing;
    [HideInInspector] public BoxCollider2D m_Collider;
    
    protected RaycastOrigins _raycastOrigins;
    protected const float _skinWidth = .015f;
    
    private const float _dstBetweenRays = .25f;
    
    public virtual void Awake()
    {
        m_Collider = GetComponent<BoxCollider2D>();
    }

    public virtual void Start()
    {
        CalculateRaySpacing();
    }

    protected void UpdateRaycastOrigins()
    {
        Bounds bounds = m_Collider.bounds;
        bounds.Expand(_skinWidth * -2);

        _raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        _raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        _raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        _raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = m_Collider.bounds;
        bounds.Expand(_skinWidth * -2);

        float boundsWidth = bounds.size.x;
        float boundsHeight = bounds.size.y;

        horizontalRayCount = Mathf.RoundToInt(boundsHeight / _dstBetweenRays);
        verticalRayCount = Mathf.RoundToInt(boundsWidth / _dstBetweenRays);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    protected struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}