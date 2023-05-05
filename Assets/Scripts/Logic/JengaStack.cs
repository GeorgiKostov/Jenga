using System;
using System.Collections.Generic;
using Assets.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class JengaStack : MonoBehaviour
    {
        [SerializeField] private TextMeshPro stackLabel; // e.g. 8 Grade
        [SerializeField] private GameObject jengaBlockPrefab;
        [SerializeField] private float offset = 0.1f;
        private Vector3 startPos = new Vector3(0, 0, 0);
        private List<JengaBlockData> jengaBlocks = new List<JengaBlockData>();
        public static Action<JengaStack> OnStackCreated;
        
        public void LoadStack(List<JengaBlockData> data, Vector3 startPos)
        {
            this.jengaBlocks.AddRange(data);
            this.stackLabel.text = this.jengaBlocks [0].grade;
            CreateJengaStack(startPos, this.jengaBlocks);
        }
        
        private void CreateJengaStack(Vector3 evenStart, List<JengaBlockData> jengas)
        {
            Vector3 blockSize = this.jengaBlockPrefab.transform.localScale;
            Vector3 oddStartPos = new Vector3(evenStart.x + this.offset + blockSize.x, 0, blockSize.x);
            int blocks = 0; // jenga pieces per row
            int column = 0; // odd or even
            for(int i = 0; i<jengas.Count; i++)
            {
                GameObject newJengaBlock = Instantiate(this.jengaBlockPrefab, Vector3.zero, Quaternion.identity, this.transform);
                newJengaBlock.GetComponent<JengaBlock>().LoadData(jengas[i]);
                newJengaBlock.transform.name = $"{blocks} { column} {jengas[i].mastery}";
                //even
                if (column % 2 == 0)
                {
                    newJengaBlock.transform.localPosition = new Vector3(evenStart.x + blocks * (blockSize.x + this.offset), blockSize.y/2 + blockSize.y * column, evenStart.z - this.offset);
                }
                //odd - rotated 90 degrees
                else
                {
                    newJengaBlock.transform.rotation = Quaternion.Euler(new Vector3(0,90,0));
                    newJengaBlock.transform.localPosition = new Vector3(oddStartPos.x, blockSize.y/2 + blockSize.y * column, oddStartPos.z - blocks * (blockSize.x + this.offset));
                }
                
                // max 3 elements per row, change column
                blocks++;
                if (blocks == 3)
                {
                    blocks = 0;
                    column++;
                }
            }
            
            OnStackCreated?.Invoke(this);
        }
        
    }
}