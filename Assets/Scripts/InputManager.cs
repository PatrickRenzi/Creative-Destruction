using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    public Transform GestureStartPosition;

    public float GestureTimeElapsed;

    public bool applyForceOnFixedUpdate;

    public Rigidbody2D fixedUpdateRigidBody2D;

    public Vector2 fixedUpdateForceToAdd;

    private GameObject _currentFocus;

    public ContractPanel contractPanel;

    public int Profit = 0;
    public bool HasFocus
    {
        get
        {
            return CurrentFocus != null;
        }
    }

    public GameObject CurrentFocus
    {
        get
        {
            return _currentFocus;
        }
        set
        {
            if(_currentFocus != null)
            {
                var oldFocusable = _currentFocus.GetComponent<Focusable>();
                if (oldFocusable)
                    oldFocusable.SetFocus(false);
            }

            _currentFocus = value;

            if (_currentFocus != null)
            {
                var newFocusable = _currentFocus.GetComponent<Focusable>();
                if (newFocusable)
                    newFocusable.SetFocus(true);
            }
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        if (AnyScreenInput())
        {
            var worldPosition = GetWorldPosition();

            if (StartGesture())
            {
                if (AquireATarget(worldPosition))
                {
                    var TargetCandidate = GetTarget(worldPosition);
                    {
                        if (TargetCandidate.tag == "Asteriod")
                            StartFlingGesture(TargetCandidate);
                        else if (TargetCandidate.tag == "Planet")
                        {
                            CurrentFocus = null;
                            Planet.CurrentPlanetFocus = TargetCandidate.GetComponent<Planet>();
                        }
                        else if (TargetCandidate.tag == "Resource")
                        {
                            if(Planet.CurrentPlanetFocus != null)
                                TargetCandidate.GetComponent<TransportToPlanet>().SetDestination(Planet.CurrentPlanetFocus.gameObject);

                        }
                    }
                }
            }
            else
            {
                if (HasFocus)
                {
                    if (CurrentFocus.tag == "Asteriod")
                        EndFlingGesture(worldPosition);
                    else if (CurrentFocus.tag == "Planet")
                        CurrentFocus = null;
                    
                }
            }
        }
    }

    private bool AnyScreenInput()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
            return true;
        else
            return false;
    }

    private Vector2 GetWorldPosition()
    {
        if (Input.touchCount > 0)
            return Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        else
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private bool StartGesture()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            return true;
        else if (Input.GetMouseButtonDown(0))
            return true;
        else
            return false;
    }

    private void FixedUpdate()
    {
        if (HasFocus)
        {
            if (CurrentFocus.tag == "Asteriod")
            {
                GestureTimeElapsed += Time.deltaTime;
            }
            else if(CurrentFocus.tag == "Planet")
            {
                var target = GetWorldPosition();

                target = Vector2.Lerp((Vector2)CurrentFocus.transform.position, (Vector2)target, Time.fixedDeltaTime * 4);

                CurrentFocus.transform.position = new Vector3(target.x, CurrentFocus.transform.position.y, CurrentFocus.transform.position.z);
            }

        }
        else if(fixedUpdateRigidBody2D != null)
        {
            fixedUpdateRigidBody2D.AddForce(fixedUpdateForceToAdd, ForceMode2D.Impulse);
            fixedUpdateRigidBody2D = null;
        }

    }

    private bool AquireATarget(Vector2 worldPosition)
    {
        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);

        if (hitData)
            return true;
        else
            return false;
    }

    private GameObject GetTarget(Vector2 worldPosition)
    {

        RaycastHit2D hitData = Physics2D.Raycast(new Vector2(worldPosition.x, worldPosition.y), Vector2.zero, 0);

        if (hitData)
        {
            return hitData.collider.gameObject;
        }

        return null;
    }

    private void StartFlingGesture(GameObject GestureTarget)
    {
        SetInitialMovement initialMovement = GestureTarget.GetComponent<SetInitialMovement>();
        Rigidbody2D rigidbody2D = GestureTarget.GetComponent<Rigidbody2D>();
        rigidbody2D.isKinematic = true;
        rigidbody2D.velocity = new Vector2(0f, 0f);
        CurrentFocus = GestureTarget;
    }

    private void EndFlingGesture(Vector2 gestureEndPosition)
    {
        var heading = gestureEndPosition - (Vector2)CurrentFocus.transform.position;

        var distance = heading.magnitude;
        var direction = heading / distance;

        if (ShouldFling(distance, GestureTimeElapsed))
        {
            applyForceOnFixedUpdate = true;
            Rigidbody2D rigidbody2D = CurrentFocus.GetComponent<Rigidbody2D>();
            fixedUpdateRigidBody2D = rigidbody2D;
            rigidbody2D.isKinematic = false;
            fixedUpdateForceToAdd = heading;
            CurrentFocus.GetComponent<Fling>().FlingTrail.gameObject.SetActive(true);
        }
        else
        {
            SetInitialMovement initialMovement = CurrentFocus.GetComponent<SetInitialMovement>();
            Rigidbody2D rigidbody2D = CurrentFocus.GetComponent<Rigidbody2D>();
            rigidbody2D.isKinematic = false;
            rigidbody2D.velocity = initialMovement.InitialMovement;
            
        }
        GestureTimeElapsed = 0f;
        InputManager.instance.CurrentFocus = null;
    }

    private bool ShouldFling(float GestureDistance, float GestureTimeElapsed)
    {
        if (GestureDistance / GestureTimeElapsed > 3)
            return true;
        else
            return false;
    }

    public void UpdateProfit(int newIncome)
    {
        Profit += newIncome;
        ContractPanel.instance.ProfitValue.text = "$" + Profit.ToString();
    }

}
