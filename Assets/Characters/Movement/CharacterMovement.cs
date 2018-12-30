using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
	public Vector2Int[] Coordinates { get; set; }
	private LinkedList<Vector3> path;
	private LinkedList<Vector2Int> pathCoords;

	public Action<Vector2Int[], int> ApplyDamage;
	public Action UpdateAttackArea;
	public Func<bool> IsSelected;
	public Action EndTurn;

    private CharacterAttributes attributes;

	private float attackTimer = 0;

    private int MovementSpeed
    {
        get
        {
            if (attributes == null)
                attributes = GetComponent<CharacterAttributes>();

            return attributes.movementSpeed;
        }
    }
    private int Damage
    {
        get
        {
            if (attributes == null)
                attributes = GetComponent<CharacterAttributes>();

            return attributes.damage;
        }
    }

    public GridRotation Rotation { get; set; }

	public Func<Vector2Int[], bool> CanMove;
    public bool IsMoving
    {
        get
        {
            return path == null || path.Count == 0 ? false : true; 
        }
    }

   
    protected bool IsAttacking = false;

    private Animator anim;
    private int attackHash;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        attackHash = Animator.StringToHash("StartAttack");
    }


    void Update()
    {
		Move();
		Rotate();
        if (Input.GetKeyDown(KeyCode.Space))
            InitializeAttack();

		
		if(attackTimer > 0)
		{
			attackTimer -= Time.deltaTime;
			if(attackTimer <= 0)
			{
				attackTimer = 0;
				EndTurn();
			}
		}
    }	

	private void Rotate()
	{
		if (IsMoving)
			return;

		if(IsSelected() && Input.GetMouseButton(1))
		{
			Plane playerPlane = new Plane(Vector3.up, transform.position);
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float hitdist = 0.0f;
			if (playerPlane.Raycast(ray, out hitdist))
			{
				Vector3 targetPoint = ray.GetPoint(hitdist);
				Quaternion futureRotation = Quaternion.LookRotation(targetPoint - transform.position);

                futureRotation = RoundRotation(futureRotation);

                var futureRotationEnum = GetRoation(futureRotation);

				if(Rotation != futureRotationEnum)
                {
                    var previousRotation = Rotation;
                    Rotation = futureRotationEnum;
                    if (!CanMove(GetFutureCoordinates(GetCenterCoord())))
                    {
                        Rotation = previousRotation;
                        return;
                    }
                    SetCoorinates(GetCenterCoord());

                    transform.rotation = futureRotation;
					UpdateAttackArea();
                }
			}
		}
	}

    private Vector2Int GetCenterCoord()
    {
        if (Coordinates.Length == 3)
            return Coordinates[1];
        return Coordinates[0];
    }

   	private void Move()
	{
		if (path != null && path.Count != 0)
		{
			if (Vector3.Distance(path.First.Value, transform.position) < 0.15)
			{
				path.RemoveFirst();
				SetCoorinates(pathCoords.First.Value);
				pathCoords.RemoveFirst();
				UpdateAttackArea();

				if (path.Count == 0)
				{
					return;
				}

				if (!CanMove(GetFutureCoordinates(pathCoords.First.Value)))
				{
					path = null;
					pathCoords = null;
					return;
				}
			}

			var vector = path.First.Value - transform.position;

			transform.position += vector.normalized * MovementSpeed * Time.deltaTime;
		}
	}
    protected virtual Quaternion RoundRotation(Quaternion targetRotation)
    {
        targetRotation.x = Mathf.Round(targetRotation.x);
        targetRotation.y = Mathf.Round(targetRotation.y);
        targetRotation.z = Mathf.Round(targetRotation.z);
        targetRotation.w = Mathf.Round(targetRotation.w);
        return targetRotation;
    }

    protected virtual GridRotation GetRoation(Quaternion rot)
	{
		if (rot.y == 1.0 && rot.w == 1.0)
		{
			return GridRotation.Right;
		}
		if (rot.y == -1.0 && rot.w == 1.0)
		{
			return GridRotation.Left;
		}
		if (rot.y == 0 && rot.w == 1.0)
		{
			return GridRotation.Up;
		}
		if (rot.y == 1.0 && rot.w == 0)
		{
			return GridRotation.Down;
		}
		return GridRotation.Down;
	}

    public void SetRotation(GridRotation r)
    {
        switch (r)
        {
            case GridRotation.Down:
                transform.rotation = new Quaternion(0, 1, 0, 0);
                break;
            case GridRotation.Up:
                transform.rotation = new Quaternion(0, 0, 0, 1);
                break;
            case GridRotation.Left:
                transform.rotation = new Quaternion(0, -1, 0, 1);
                break;
            case GridRotation.Right:
                transform.rotation = new Quaternion(0, 1, 0, 1);
                break;
        }
        Rotation = r;
    }

	public void InitializeMovement(LinkedList<Vector3> path, LinkedList<Vector2Int> pathCoords)
    {
        if (!CanMove(GetFutureCoordinates(pathCoords.First.Value)))
        {
            return;
        }
        this.path = path;
        this.pathCoords = pathCoords;
    }

    public bool InitializeAttack()
    {
        if (IsMoving || !IsSelected())
            return false;

        ApplyDamage(GetAttackArea(), Damage);

        anim.SetTrigger(attackHash);
		attackTimer = 1;
        return true;
    } 

    public virtual void SetCoorinates(Vector2Int centerCoord)
    {
    }

    public virtual Vector2Int[] GetFutureCoordinates(Vector2Int futureCoords)
    {
        return null;
    }

	public virtual Vector2Int[] GetAttackArea()
	{
		return null;
	}
}
