using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Data;
using Assets.Scripts.Logic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Controls
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField]
        private List<JengaStack> jengaStacks = new List<JengaStack>();
        [SerializeField] private TextMeshProUGUI blockText;
        [SerializeField] private Vector3 offset = new Vector3(0,0,-5);
        public float sensitivity = 5.0f; // The speed at which the camera rotates

        private float _mouseX = 0.0f;
        private float _mouseY = 0.0f;
        
        private Camera camera;
        private int currentStack;
        private GameObject currenStackSelected;

        private void Awake()
        {
            JengaStack.OnStackCreated+= OnStackCreated;
            this.camera = GetComponent<Camera>();
        }

        private void OnDestroy()
        {
            JengaStack.OnStackCreated-= OnStackCreated;
        }

        private void OnStackCreated(JengaStack obj)
        {
            this.jengaStacks.Add(obj);
            FocusOnStack(0);
        }

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                _mouseX += Input.GetAxis("Mouse X") * sensitivity;
                _mouseY -= Input.GetAxis("Mouse Y") * sensitivity;

                transform.position = this.currenStackSelected.transform.position + Quaternion.Euler(_mouseY, _mouseX, 0) * this.offset;
                transform.LookAt(this.currenStackSelected.transform.position);
            }

            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    JengaBlock block = hit.transform.GetComponent<JengaBlock>();
                    if (block != null)
                    {
                        JengaBlockData data = block.JengaData;
                        this.blockText.text =
                            $"{data.grade}:{data.domain}\n{data.cluster}\n{data.standardid}:{data.standarddescription}";
                    }
                }
            }
        }

        public void OnNextStack()
        {
            this.currentStack++;
            if (this.currentStack > jengaStacks.Count - 1)
            {
                this.currentStack = 0;
            }
            FocusOnStack(this.currentStack);
        }
        
        public void OnPreviousStack()
        {
            this.currentStack--;
            if (this.currentStack < 0)
            {
                this.currentStack = jengaStacks.Count - 1;
            }
            FocusOnStack(this.currentStack);
        }

        private void FocusOnStack(int stack)
        {
            this.currentStack = stack;
            this.currenStackSelected = this.jengaStacks[stack].gameObject;
            transform.position = this.currenStackSelected.transform.position + Quaternion.Euler(_mouseY, _mouseX, 0) * this.offset;
            transform.LookAt(this.currenStackSelected.transform.position);
        }
    }
}