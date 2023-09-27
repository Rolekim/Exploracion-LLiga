using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    FollowPoint[] charactersFollow;
    [SerializeField]
    GameObject[] characters;

    [SerializeField]
    float speed = 3;

    [SerializeField]
    Animator characterAnimator;
    [SerializeField]
    SpriteRenderer characterSprite;
    [SerializeField]
    Animator exclamationSign;

    [SerializeField]
    GameObject dustTrail;
    [SerializeField]
    Transform dustTrailPoint;
    [SerializeField]
    float timeToSpawn = 0.3f;
    bool canSpawnDust = true;

    float lastYAxis;
    float lastXAxis;

    [SerializeField]
    float minSteps = 10;
    [SerializeField]
    float maxSteps = 30;
    [SerializeField]
    float stepsForFight;
    [SerializeField]
    float dist;
    Vector3 initialPos;

    private RaycastHit2D shot;
    public Vector3 raycastDir;

    [SerializeField]
    Transform[] followPositions;

    SceneManager sceneManager;

    void Awake()
    {     
        
        characterSprite = GetComponentInChildren<SpriteRenderer>();
        stepsForFight = Random.Range(minSteps, maxSteps);
        sceneManager = FindObjectOfType<SceneManager>();
        SceneSave.sceneSave.RegisterInitialPositions(this.gameObject, characters[0], characters[1], characters[2]);
        SceneSave.sceneSave.PlayerIsComingBack(this.gameObject, characters[0], characters[1], characters[2]);
        Debug.Log("Transform Conglo: " + transform.position + " | Transform guardado: " + SceneSave.sceneSave.ReturnPlayerPosition());
        initialPos = transform.position;
    }

    void Start()
    {
        if (!SceneSave.sceneSave.firstDialogue)
        {
            SceneSave.sceneSave.ChangeCanMove(true);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (SceneSave.sceneSave.ReturnCanMove())
        {
            float xAxis = Input.GetAxisRaw("Horizontal");

            float yAxis = Input.GetAxisRaw("Vertical");

            if (xAxis != 0)
            {
                lastXAxis = xAxis;
                lastYAxis = 0;
                SpawnDust();        
            }
            if (yAxis != 0)
            {
                lastYAxis = yAxis;
                lastXAxis = 0;
                SpawnDust();
                
            }

            characterAnimator.SetFloat("yinput", yAxis);
            characterAnimator.SetFloat("xinput", xAxis);
            characterAnimator.SetFloat("lastYInput", lastYAxis);
            characterAnimator.SetFloat("lastXInput", lastXAxis);

            dist = Vector3.Distance(initialPos, transform.position);
            if (dist >= stepsForFight)
            {
                stepsForFight = Random.Range(minSteps, maxSteps);
                initialPos = transform.position;
                SceneSave.sceneSave.PlayerIsSwitchingScene(this.gameObject, characters[0], characters[1], characters[2]);

                Debug.Log(SceneSave.sceneSave.ReturnPlayerPosition());
                StartCoroutine(ChangeToBattle());
                Debug.Log("Batalla");
            }

            transform.Translate(new Vector3(xAxis, yAxis, 0f).normalized * (speed * Time.deltaTime));

            foreach (FollowPoint f in charactersFollow)
            {
                f.UpdateAnimatorAxis(xAxis, yAxis, lastXAxis, lastYAxis);
            }
        }

    }

    void Update()
    {
        int layer_mask = LayerMask.GetMask("NPCs");

        if (lastYAxis == -1)
        {
            shot = Physics2D.Raycast(transform.position, Vector2.down, 100f, layer_mask);
            raycastDir = Vector2.down;
        }
        else if(lastYAxis == 1)
        {
            shot = Physics2D.Raycast(transform.position, Vector2.up, 100f, layer_mask);
            raycastDir = Vector2.up;
        }
        if (lastXAxis == -1)
        {
            shot = Physics2D.Raycast(transform.position, Vector2.left, 100f, layer_mask);
            raycastDir = Vector2.left;
        }
        else if(lastXAxis == 1)
        {
            shot = Physics2D.Raycast(transform.position, Vector2.right, 100f, layer_mask);
            raycastDir = Vector2.right;

        }

        Debug.DrawRay(transform.position, raycastDir, Color.green);

        if (shot.collider != null)
        {
            // Calculate the distance from the surface and the "error" relative
            // to the floating height.
            float distance = Vector3.Distance(shot.point, transform.position);

            if (distance <= 1)
            {
                if (shot.collider.tag == "NPC")
                {
                    if (Input.GetKeyDown(KeyCode.E) && SceneSave.sceneSave.ReturnCanMove())
                    {
                        shot.collider.GetComponent<DialogueTrigger>().TriggerDialogue();
                    }
                }
            }
        }
    }

    public void ReturnPositions()
    {
        SceneSave.sceneSave.PlayerIsSwitchingScene(this.gameObject, characters[0], characters[1], characters[2]);
    }

    public Transform ReturnPos(int pos)
    {
        return followPositions[pos];
    }

     public IEnumerator ChangeToBattle()
    {
        SoundManager.soundManager.StopExplorationMusic();
        SoundManager.soundManager.PlayStingerBattleFound();
        SceneSave.sceneSave.ChangeCanMove(false);
        exclamationSign.SetTrigger("Show");
        yield return new WaitForSeconds(0.5f);
        SceneSave.sceneSave.ShowTransition();
        SoundManager.soundManager.PlayBattleStarts();
        yield return new WaitForSeconds(0.4f);
        SceneSave.sceneSave.ChangeCanMove(true);
        sceneManager.ChangeSceneToBattle();
    }

    public IEnumerator ChangeToBossBattle()
    {
        SoundManager.soundManager.StopExplorationMusic();
        SoundManager.soundManager.PlayStingerBattleFound();
        SceneSave.sceneSave.ChangeCanMove(false);
        exclamationSign.SetTrigger("Show");
        yield return new WaitForSeconds(0.5f);
        SceneSave.sceneSave.ShowTransition();
        SoundManager.soundManager.PlayBattleStarts();
        yield return new WaitForSeconds(0.4f);
        SceneSave.sceneSave.ChangeCanMove(true);
        sceneManager.ChangeSceneToBoss();
    }

    void SpawnDust()
    {
        if (canSpawnDust)
        {
            Instantiate(dustTrail, dustTrailPoint.position, Quaternion.identity);
            canSpawnDust = false;
            StartCoroutine(TimeToSpawnDust());
        }
    }

    IEnumerator TimeToSpawnDust()
    {
        yield return new WaitForSeconds(timeToSpawn);
        canSpawnDust = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "BossTrigger")
        {   
            col.GetComponent<DialogueTrigger>().TriggerDialogue();
            StartCoroutine(TimeToFinalBattle());
        }

    }

    IEnumerator TimeToFinalBattle()
    {
        yield return new WaitForSeconds(1f);
        SceneSave.sceneSave.finalBattle = true;
    }

}
