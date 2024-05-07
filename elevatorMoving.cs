using UnityEngine;

public class ElevatorMoving : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startPoint;
    [SerializeField] private Transform[] points;

    private int currentPointIndex;
    private bool waitingFor6 = false;
    private bool waitingFor4 = false;
    private bool waitingFor1 = false;
    private bool waitingFor2 = false;
    private bool movingTowardsNextPoint = false; // Declare the variable here

    private void Start()
    {
        if (points.Length > 0)//checks whether points are assigned to the elevator
        {
            currentPointIndex = startPoint;//startpoint = 1 here
            transform.position = points[startPoint].position;//places the elevator at point1
        }
        else
        {
            Debug.LogError("No points assigned to the elevator.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            waitingFor6 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && waitingFor6)
        {
            waitingFor4 = true;
            waitingFor6 = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && waitingFor4)
        {
            MoveToNextPoint();
            waitingFor4 = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            waitingFor1 = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && waitingFor1)
        {
            waitingFor2 = true;
            waitingFor1 = false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && waitingFor2)
        {
            MoveToPreviousPoint();
            waitingFor2 = false;
        }

        if (movingTowardsNextPoint)
        {
            MoveTowardsNextPoint();
        }
    }

    private void MoveToNextPoint()
    {
        if (currentPointIndex < points.Length - 1)
        {
            currentPointIndex++;//0->1
            movingTowardsNextPoint = true;
        }
        else
        {
            Debug.Log("Already at the last point.");
        }
    }

    private void MoveToPreviousPoint()
    {
        if (currentPointIndex > 0)//currentpointindex -> 1
        {
            currentPointIndex--;//currentpointindex -> 0
            movingTowardsNextPoint = true;
        }
        else
        {
            Debug.Log("Already at the first point.");
        }
    }

    private void MoveTowardsNextPoint()
    {
        Vector3 targetPosition = points[currentPointIndex].position;
        //case1: when elevator moves up points[1]-> point2
        //holds the position of point2

        //case2: when elevator moves down points[0] -> point1
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        //moves the elevator from point1 to point2
        if (transform.position == targetPosition)
        {
            movingTowardsNextPoint = false;
            Update();
        }
    }
}

