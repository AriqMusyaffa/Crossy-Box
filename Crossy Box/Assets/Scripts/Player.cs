using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] GameManager GM;
    [SerializeField] TMP_Text stepText;
    [SerializeField, Range(0.01f, 1f)] float moveDuration = 0.2f;
    [SerializeField, Range(0.01f, 1f)] float jumpHeight = 0.5f;
    private float backBoundary;
    private float leftBoundary;
    private float rightBoundary;
    public bool standingOnLog = false;
    public bool standingOnLily = false;
    private bool logIsRight;
    private float logSpeed;
    public bool guaranteeDrown = false;
    private bool soonDead = false;
    private char lookDir;
    [SerializeField] private bool onRail = false;

    [SerializeField] private int maxTravel;
    [SerializeField] GameObject eagleMeter;
    [SerializeField] GameObject stepTextObj;
    [SerializeField] GameObject coinTextObj;
    [SerializeField] GameObject stepNewObj;
    [SerializeField] GameObject coinNewObj;
    [SerializeField] GameObject duckAll;
    [SerializeField] ParticleSystemRenderer splashParticle;
    [SerializeField] Material matLava;

    public int MaxTravel
    {
        get => maxTravel;
    }

    [SerializeField] private int currentTravel;
    public int CurrentTravel
    {
        get => currentTravel;
    }

    public bool IsDie
    {
        get => this.enabled == false;
    }

    public void SetUp(int minZPos, int extent)
    {
        backBoundary = minZPos - 1;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;
    }

    void Start()
    {
        if (GM.areaType == GameManager.AreaType.Dark)
        {
            splashParticle.material = matLava;
        }
    }

    void Update()
    {
        var moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += new Vector3(0, 0, 1);
            lookDir = 'N';
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moveDir += new Vector3(0, 0, 1);
            lookDir = 'N';
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir += new Vector3(0, 0, -1);
            lookDir = 'S';
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDir += new Vector3(0, 0, -1);
            lookDir = 'S';
        }

        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir += new Vector3(-1, 0, 0);
            lookDir = 'W';
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDir += new Vector3(-1, 0, 0);
            lookDir = 'W';
        }

        else if(Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += new Vector3(1, 0, 0);
            lookDir = 'E';
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDir += new Vector3(1, 0, 0);
            lookDir = 'E';
        }

        if (moveDir != Vector3.zero && IsJumping() == false)
        {
            Jump(moveDir, lookDir);
        }

        if (standingOnLog && IsJumping() == false)
        {
            if (logIsRight)
            {
                transform.position += new Vector3(1, 0, 0) * Time.deltaTime * logSpeed;
            }
            else
            {
                transform.position -= new Vector3(1, 0, 0) * Time.deltaTime * logSpeed;
            }
        }

        if (!guaranteeDrown)
        {
            if ((transform.position.x < -4 || transform.position.x > 4) && IsJumping() == false)
            {
                Jump(transform.position, 'X');

                if (!standingOnLog)
                {
                    guaranteeDrown = true;
                }
            }
        }

        if (!soonDead)
        {
            if (guaranteeDrown && IsJumping() == false)
            {
                if (GM.areaType == GameManager.AreaType.Sand)
                {
                    Sink();
                }
                else
                {
                    Drown();
                }

                soonDead = true;
            }
        }

        if (IsJumping() == false)
        {
            if (standingOnLog || standingOnLily)
            {
                transform.position = new Vector3(transform.position.x, 0.05f, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, 0, transform.position.z);
            }
        }
    }

    private void Jump(Vector3 targetDirection, char cardinalRotation)
    {
        // Atur rotasi
        var targetPosition = transform.position + targetDirection;
        targetPosition = new Vector3(Mathf.Round(targetPosition.x), Mathf.Round(targetPosition.y), Mathf.Round(targetPosition.z));
        //Debug.Log(targetPosition);

        //transform.LookAt(targetPosition);

        // Fix rotasi
        switch (cardinalRotation)
        {
            case 'N':
                transform.rotation = Quaternion.Euler(transform.rotation.x, 0, transform.rotation.z);
                break;
            case 'S':
                transform.rotation = Quaternion.Euler(transform.rotation.x, 180, transform.rotation.z);
                break;
            case 'W':
                transform.rotation = Quaternion.Euler(transform.rotation.x, -90, transform.rotation.z);
                break;
            case 'E':
                transform.rotation = Quaternion.Euler(transform.rotation.x, 90, transform.rotation.z);
                break;
        }

        // Loncat ke atas
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration / 2).OnComplete(GM.SFX_Step));

        if (targetPosition.z <= backBoundary || 
            targetPosition.x <= leftBoundary || 
            targetPosition.x >= rightBoundary)
        {
            return;
        }

        if (Tree.AllPositions.Contains(targetPosition) || Rock.AllPositions.Contains(targetPosition))
        {
            if (GM.TerrainType[(int)transform.position.z].GetComponent<Water>() != null)
            {
                standingOnLog = false;
                guaranteeDrown = true;
                return;
            }
            else
            {
                return;
            }
        }

        // Gerak maju/mundur/samping
        transform.DOMoveX(Mathf.Round(targetPosition.x), moveDuration);
        transform.DOMoveZ(targetPosition.z, moveDuration)
            .OnComplete(UpdateTravel);
    }

    private void UpdateTravel()
    {
        currentTravel = (int)this.transform.position.z;

        if (currentTravel > maxTravel)
        {
            maxTravel = currentTravel;
        }

        stepText.text = "STEP : " + maxTravel;

        // Check tipe terrain yang diinjak
        //Debug.Log(GM.TerrainType[(int)transform.position.z]);

        if (GM.TerrainType[(int)transform.position.z].GetComponent<Water>() != null)
        {
            if (GM.TerrainType[(int)transform.position.z].GetComponent<LilyPadSpawner>().enabled &&
                    !GM.TerrainType[(int)transform.position.z].GetComponent<LilyPadSpawner>().safeXPos.Contains((int)transform.position.x))
            {
                if (GM.areaType == GameManager.AreaType.Sand)
                {
                    Sink();
                }
                else
                {
                    Drown();
                }
            }
            else if (GM.TerrainType[(int)transform.position.z].GetComponent<LogSpawner>().enabled && IsJumping() == false)
            {
                if (!standingOnLog)
                {
                    if (GM.areaType == GameManager.AreaType.Sand)
                    {
                        Sink();
                    }
                    else
                    {
                        Drown();
                    }
                }
            }
        }
    }

    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.enabled == false)
        {
            return;
        }

        if (other.tag == "Car")
        {
            Squish();
        }

        if (other.tag == "Log")
        {
            logIsRight = other.GetComponent<Log>().isRight;
            logSpeed = other.GetComponent<Log>().speed;
            standingOnLog = true;
        }

        if (other.tag == "LilyPad")
        {
            standingOnLily = true;
        }

        if (other.tag == "Rail")
        {
            onRail = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Log")
        {
            standingOnLog = false;
            UpdateTravel();
        }

        if (other.tag == "LilyPad")
        {
            standingOnLily = false;
        }

        if (other.tag == "Rail")
        {
            onRail = false;
        }
    }

    private void Squish()
    {
        eagleMeter.SetActive(false);
        stepNewObj.SetActive(false);
        coinNewObj.SetActive(false);
        //stepTextObj.SetActive(false);
        //coinTextObj.SetActive(false);

        if (this.enabled)
        {
            GetComponent<AudioSource>().Play();
        }
        transform.DOScaleY(0.1f, 0.2f);
        transform.DOScaleX(2, 0.2f);
        transform.DOScaleZ(2, 0.2f);

        if (onRail)
        {
            transform.position = new Vector3(transform.position.x, 0.036f, transform.position.z);
        }

        this.enabled = false;
    }

    private void Drown()
    {
        eagleMeter.SetActive(false);
        stepNewObj.SetActive(false);
        coinNewObj.SetActive(false);
        //stepTextObj.SetActive(false);
        //coinTextObj.SetActive(false);

        if (this.enabled)
        {
            GM.SFX_Splash();
        }
        duckAll.SetActive(false);
        if (GM.areaType == GameManager.AreaType.Dark)
        {
            splashParticle.material = matLava;
        }
        splashParticle.gameObject.SetActive(true);
        this.enabled = false;
    }

    private void Sink()
    {
        eagleMeter.SetActive(false);
        stepNewObj.SetActive(false);
        coinNewObj.SetActive(false);
        //stepTextObj.SetActive(false);
        //coinTextObj.SetActive(false);

        if (this.enabled)
        {
            GM.SFX_Sand();
        }
        transform.DOMoveY(-1, 3f);
        this.enabled = false;
    }
}
