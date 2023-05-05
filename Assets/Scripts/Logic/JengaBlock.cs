using Assets.Scripts.Behaviour;
using Assets.Scripts.Data;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class JengaBlock:MonoBehaviour
    {
        private JengaBlockData jengaData;
        private Rigidbody rigidbody;
        private MeshRenderer renderer;
        [SerializeField] private Material glassMaterial;
        [SerializeField] private Material woodMaterial;
        [SerializeField] private Material stoneMaterial;

        public JengaBlockData JengaData => this.jengaData;

        private void Awake()
        {
            this.renderer = GetComponent<MeshRenderer>();
            this.rigidbody = GetComponent<Rigidbody>();
            this.rigidbody.useGravity = false;
            EventBroadcaster.OnTestMyStack+= OnTestMyStack;
        }

        private void OnDestroy()
        {
            EventBroadcaster.OnTestMyStack-= OnTestMyStack;
        }

        private void OnTestMyStack()
        {
            this.rigidbody.useGravity = true;
            this.rigidbody.isKinematic = false;
            if (this.JengaData.mastery == "0")
            {
                Destroy(this.gameObject);
            }
        }

        public void LoadData(JengaBlockData data)
        {
            this.jengaData = data;

            switch (data.mastery)
            {
                case "0":
                   this.renderer.material = this.glassMaterial;
                    break;
                case "1":
                   this.renderer.material = this.woodMaterial;
                    break;
                case "2":
                  this.renderer.material = this.stoneMaterial;
                    break;
            }
        }

        public JengaBlockData OnClickGetData()
        {
            return this.JengaData;
        }
    }
}