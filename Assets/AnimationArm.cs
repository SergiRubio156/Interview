using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimationArm : MonoBehaviour
{
    public Animator animator;
    private Animation animationComponent;

    ObjectManager objectManager;
    Objects obj;

    public List<GameObject> robots = new List<GameObject>();
    public List<GameObject> arms = new List<GameObject>();
    public List<AnimationClip> animationClips = new List<AnimationClip>();

    float cuerpoRobot,brazoRobot;
    Transform child;
    Vector3 position;
    Quaternion rotation;
    GameObject _obj;

    public bool finishAnimation;

    private void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
        animator = gameObject.GetComponent<Animator>();
        objectManager = FindObjectOfType<ObjectManager>();

    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    public GameObject GetRobotPosition(float _num)
    {
        switch(_num)
        {
            case 1:
                return robots[0];              
            case 2:
                return robots[1];
            case 3:
                return robots[2];
        }
        return null;
    }


    private void HandleGameStateChanged(GameState newState) 
    {
        switch (newState)
        {
            case GameState.Playing:
                break;
            case GameState.Lasers:

                break;
            case GameState.Settings:
                break;
            case GameState.Menu:

                break;
            case GameState.Wire:
                finishAnimation = false;
                animator.SetInteger("animatorChoose", 0);
                break;
            case GameState.Topping:

                break;
            case GameState.Color:

                break;
            case GameState.RobotPanel:
                animator.SetInteger("animatorChoose", 0);
                break;
            case GameState.ArmPanel:
                obj = objectManager.FindStateOfObject(ObjectState.Arm);
                cuerpoRobot = obj.robotUp;
                obj.LerpRotation(obj.robotUp);
                break;
        }
    }
    
    void InstantiateObj(int _num)
    {
        switch(_num)
        {
            case 1:
                
                 _obj = Instantiate(arms[0], position, rotation);

                _obj.transform.SetParent(transform);
                _obj.name = "Arm " + 1;
                break;
            case 2:
                 
                 _obj = Instantiate(arms[1], position, rotation);

                _obj.transform.SetParent(transform);

                _obj.name = "Arm " + 2;

                break;
            case 3:
                
                _obj = Instantiate(arms[2], position, rotation);

                _obj.transform.SetParent(transform);

                _obj.name = "Arm " + 3;

                break;
        }
    }
    public void AnimationRun(int _numDown)
    {
        switch(cuerpoRobot)
        {
            case 1:
                if(_numDown == 1)
                {
                    position = arms[0].transform.position;
                    rotation = arms[0].transform.rotation;

                    brazoRobot = 0.1f;

                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[0].length;

                    animator.SetInteger("animatorChoose", 1);

                    arms[0].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);

                }
                else if(_numDown == 2)
                {
                    brazoRobot = 0.2f;
                    position = arms[1].transform.position;
                    rotation = arms[1].transform.rotation;
                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[1].length;

                    animator.SetInteger("animatorChoose", 2);

                    arms[1].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);
                }
                else if(_numDown == 3)
                {
                    brazoRobot = 0.3f;
                    position = arms[2].transform.position;
                    rotation = arms[2].transform.rotation;

                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[2].length;

                    animator.SetInteger("animatorChoose", 3);

                    arms[2].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);
                }
                break;
            case 2:
                if (_numDown == 1)
                {
                    position = arms[0].transform.position;
                    rotation = arms[0].transform.rotation;

                    brazoRobot = 0.1f;

                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[3].length;

                    animator.SetInteger("animatorChoose", 4);

                    arms[0].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);
                }
                else if (_numDown == 2)
                {
                    brazoRobot = 0.2f;
                    position = arms[1].transform.position;
                    rotation = arms[1].transform.rotation;
                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[4].length;

                    animator.SetInteger("animatorChoose", 5);

                    arms[1].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);
                }
                else if (_numDown == 3)
                {
                    brazoRobot = 0.3f;
                    position = arms[2].transform.position;
                    rotation = arms[2].transform.rotation;

                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[5].length;

                    animator.SetInteger("animatorChoose", 6);

                    arms[2].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);
                }
                break;
            case 3:
                if (_numDown == 1)
                {
                    position = arms[0].transform.position;
                    rotation = arms[0].transform.rotation;

                    brazoRobot = 0.1f;

                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[6].length;

                    animator.SetInteger("animatorChoose", 7);

                    arms[0].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);
                }
                else if (_numDown == 2)
                {
                    brazoRobot = 0.2f;
                    position = arms[1].transform.position;
                    rotation = arms[1].transform.rotation;
                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[7].length;

                    animator.SetInteger("animatorChoose", 8);

                    arms[1].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);
                }
                else if (_numDown == 3)
                {
                    brazoRobot = 0.3f;
                    position = arms[2].transform.position;
                    rotation = arms[2].transform.rotation;

                    obj.robotDown = brazoRobot;

                    float animationDuration = animationClips[8].length;

                    animator.SetInteger("animatorChoose", 9);

                    arms[2].SetActive(true);

                    Invoke("OnAnimationEnd", animationDuration);
                }
                break;
        }
    }

    private void OnAnimationEnd()
    {
        switch(brazoRobot)
        {
            case 0.1f:
                InstantiateObj(1);

                _obj.transform.SetParent(obj.obj.transform);
                arms[0].SetActive(false);

                finishAnimation = true;
                break;
            case 0.2f:
                InstantiateObj(2);

                child = arms[1].transform;
                arms[1].SetActive(false);

                _obj.transform.SetParent(obj.obj.transform);


                finishAnimation = true;

                break;
            case 0.3f:
                InstantiateObj(3);

                child = arms[2].transform;
                arms[2].SetActive(false);

                _obj.transform.SetParent(obj.obj.transform);

                finishAnimation = true;
                break;
        }
    }
}
