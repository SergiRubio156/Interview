using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StarterAssets
{
    public class LaserManager : MonoBehaviour
    {
        //private StarterAssetsInputs controls;
        public Camera mainCamera;
        public GameObject ObjectMove;

        //Raycast
        float maxRayDistance = 100.0f;

        //Mouse
        bool sceneSettings;
        Vector3 worldPosition;
        bool objectSelect;
        private float xRotation = 30;

        private Vector3 screenPoint;
        private Vector3 offset;
        void Awake()
        {
            GameManager.OnGameStateChanged += GameManager_OnGameStateChanged;   //Esto es el evento del script GameManager
            mainCamera = Camera.main;
            //controls = new StarterAssetsInputs();
        }

        private void OnDestroy()
        {
            GameManager.OnGameStateChanged -= GameManager_OnGameStateChanged;   //La funcion "OnDestroy" se activa cuando destruimos el objeto, una vez destruido se activa el evento,
        }
        private void Start()
        {
            objectSelect = false;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
        }

        private void GameManager_OnGameStateChanged(GameState state)        //Esta funcion depende del Awake del evento, Como he explicado antes nso permite comparar entre Script y GameObjects
        {
            Cursor.visible = (state == GameState.Lasers && state == GameState.Settings);
            sceneSettings = (state == GameState.Settings);
        }


        // Update is called once per frame
        void Update()
        {
            MoveMouse();
            if (Input.GetKeyDown(KeyCode.Escape)) //Cuando le damos click al Escape entra a esta funcion
            {
                if (sceneSettings) GameManager.Instance.UpdateGameState(GameState.Lasers);
                else GameManager.Instance.UpdateGameState(GameState.Settings);
            }
            if (objectSelect)
                MoveObject();
        }



        void MoveMouse()
        {
            worldPosition = Input.mousePosition;
            worldPosition.z = 10.0f;
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, worldPosition.z);
            curScreenPoint = Camera.main.ScreenToWorldPoint(curScreenPoint);

            //if (curScreenPoint.y <= 0)
            //curScreenPoint.y = 0.02f;
            transform.position = curScreenPoint;
            if (Input.GetMouseButtonDown(0))
                RayObject();
        }


        void RayObject()
        { 
                Debug.Log("1");
                RaycastHit hit;
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, maxRayDistance) && !objectSelect)
                {
                    Debug.Log("2");
                    Debug.DrawRay(worldPosition, Vector3.forward, Color.red, maxRayDistance);
                    if (hit.collider.CompareTag("Interactable"))
                    {
                        Debug.Log("3");
                        ObjectMove = hit.collider.gameObject;
                        objectSelect = true;
                    }
                }
                else if(objectSelect)
                {
                    ObjectMove = null;
                    objectSelect = false;
                }
        }

        void MoveObject()
        {
            ObjectMove.transform.position = transform.position;
        }

    }
}
