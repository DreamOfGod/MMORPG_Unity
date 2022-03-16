using UnityEngine;

public class FingerEvent :  MonoBehaviour
{
	public static FingerEvent Instance;

	/// <summary>
	/// 手指方向枚举
	/// </summary>
	public enum FingerDir
    {
		//上、下、左、右
		Up, Down, Left, Right
    }

	/// <summary>
	/// 滑动委托
	/// </summary>
	public System.Action<FingerDir> OnFingerDrag;

	/// <summary>
	/// 手指点击后未滑动，然后抬起的委托
	/// </summary>
	public System.Action<Vector2> OnFingerUpWithoutDrag;

	/// <summary>
	/// 手指点击后，抬起前是否滑动
	/// </summary>
	private bool dragAfterFingerDown = false;

	/// <summary>
	/// 缩放类型
	/// </summary>
	public enum ZoomType
    {
		//放大、缩小
		In, Out
    }

	/// <summary>
	/// 缩放委托
	/// </summary>
	public System.Action<ZoomType> OnZoom;

	private Vector2 m_OldFinger1Pos;
	private Vector2 m_OldFinger2Pos;

    private void Awake()
    {
		Instance = this;
    }

    void OnEnable()
    {
        FingerGestures.OnFingerDown += OnFingerDown;
        FingerGestures.OnFingerUp += OnFingerUp;
        FingerGestures.OnFingerDragBegin += OnFingerDragBegin;
        FingerGestures.OnFingerDragMove += OnFingerDragMove;
    }

    void OnDisable()
    {
        FingerGestures.OnFingerDown -= OnFingerDown;
        FingerGestures.OnFingerUp -= OnFingerUp;
        FingerGestures.OnFingerDragBegin -= OnFingerDragBegin;
        FingerGestures.OnFingerDragMove -= OnFingerDragMove;
    }

    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN//编辑器和PC平台
		float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
		if (scrollWheel > 0)
        {
			//放大
			if(OnZoom != null)
            {
				OnZoom(ZoomType.In);
            }
        }
		else if(scrollWheel < 0)
        {
			//缩小
			if (OnZoom != null)
			{
				OnZoom(ZoomType.Out);
			}
		}
#elif UNITY_ANDROID || UNITY_IPHONE//安卓和苹果平台
		if(Input.touchCount > 1)
        {
			if(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
				float nowDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(0).position);
				float oldDistance = Vector2.Distance(m_OldFinger1Pos, m_OldFinger2Pos);

				if(nowDistance > oldDistance)
                {
					//放大
					if (OnZoom != null)
					{
						OnZoom(ZoomType.In);
					}
				}
				else if(nowDistance < oldDistance)
                {
					//缩小
					if (OnZoom != null)
					{
						OnZoom(ZoomType.Out);
					}
				}
			}

			m_OldFinger1Pos = Input.GetTouch(0).position;
			m_OldFinger2Pos = Input.GetTouch(1).position;
		}
#endif
	}

	//按下时调用
	void OnFingerDown( int fingerIndex, Vector2 fingerPos )
    {
		dragAfterFingerDown = false;
	}
	
	//抬起时调用
	void OnFingerUp( int fingerIndex, Vector2 fingerPos, float timeHeldDown )
	{
		if(!dragAfterFingerDown)
        {
			if(OnFingerUpWithoutDrag != null)
            {
				OnFingerUpWithoutDrag(fingerPos);
            }
        }
	}
	
	//开始滑动
	void OnFingerDragBegin( int fingerIndex, Vector2 fingerPos, Vector2 startPos )
    {
		dragAfterFingerDown = true;
	}

	//滑动中
    void OnFingerDragMove( int fingerIndex, Vector2 fingerPos, Vector2 delta )
    {
		//判断滑动方向：想象直角坐标系中的两条直线y=x和y=-x，向左就是delta位于y=x的下方和y=-x的上方区域
		if(delta.y > delta.x && delta.y > -delta.x)
        {
			//向上
			if(OnFingerDrag != null)
            {
				OnFingerDrag(FingerDir.Up);
			}
        }
		else if(delta.y < delta.x && delta.y < -delta.x)
        {
			//向下
			if (OnFingerDrag != null)
			{
				OnFingerDrag(FingerDir.Down);
			}
		}
		else if(delta.y >= delta.x && delta.y <= -delta.x)
        {
			//向左
			if (OnFingerDrag != null)
			{
				OnFingerDrag(FingerDir.Left);
			}
		}
		else if(delta.y <= delta.x && delta.y >= -delta.x)
        {
			//向右
			if (OnFingerDrag != null)
			{
				OnFingerDrag(FingerDir.Right);
			}
		}
    }
}