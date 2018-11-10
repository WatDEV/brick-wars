using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    public Vector2Int[] Coordinates { get; set; }
    private LinkedList<Vector3> path;
    private LinkedList<Vector2Int> pathCoords;
	public Action UpdateAttackArea;
	public Func<bool> IsSelected;
    private int movementSpeed = 20;
	public GridRotation Rotation { get; set; }

	public Func<Vector2Int[], bool> CanMove;
    public bool IsMoving
    {
        get
        {
            return path == null || path.Count == 0 ? false : true; 
        }
    }

    // Update is called once per frame
    void Update()
    {
		Move();
		Rotate();
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
				Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
				
				targetRotation.x = Mathf.Round(targetRotation.x);
				targetRotation.y = Mathf.Round(targetRotation.y);
				targetRotation.z = Mathf.Round(targetRotation.z);
				targetRotation.w = Mathf.Round(targetRotation.w);

				var rot = GetRoation(targetRotation);
				if(Rotation != rot)
				{
					Rotation = rot;
					transform.rotation = targetRotation;
					UpdateAttackArea();
				}
			}
		}
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

			transform.position += vector.normalized * movementSpeed * Time.deltaTime;
		}
	}

	static GridRotation GetRoation(Quaternion rot)
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
	public void InitializeMovement(LinkedList<Vector3> path, LinkedList<Vector2Int> pathCoords)
    {
        this.path = path;
        this.pathCoords = pathCoords;
    }

    public virtual void SetCoorinates(Vector2Int centerCoord)
    {
        //rotationnot yet implemented
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
