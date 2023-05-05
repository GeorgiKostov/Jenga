using System.Collections.Generic;
using System.Linq;
using Assets._3rdParty;
using Assets.Scripts.Data;
using Assets.Scripts.Logic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class JengaGenerator : AManager<JengaGenerator>
    {
        [SerializeField]
        private GameObject jengaStackPrefab;

        [SerializeField] private Vector3 stack1Pos;
        [SerializeField] private Vector3 stack2Pos;
        [SerializeField] private Vector3 stack3Pos;
        [SerializeField]
        List<JengaBlockData> stack1 = new List<JengaBlockData>();
        [SerializeField]
        List<JengaBlockData> stack2 = new List<JengaBlockData>();
        [SerializeField]
        List<JengaBlockData> stack3 = new List<JengaBlockData>();
        
        public void GenerateStacks(List<JengaBlockData> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i].grade.Contains("6"))
                {
                    stack1.Add(data[i]);
                    this.stack1.OrderBy(x => x.domain)
                        .ThenBy(x => x.cluster)
                        .ThenBy(x => x.standardid);
                }
                if (data[i].grade.Contains("7"))
                {
                    stack2.Add(data[i]);
                    this.stack1.OrderBy(x => x.domain)
                        .ThenBy(x => x.cluster)
                        .ThenBy(x => x.standardid);
                }
                if (data[i].grade.Contains("8"))
                {
                    stack3.Add(data[i]);
                    this.stack1.OrderBy(x => x.domain)
                        .ThenBy(x => x.cluster)
                        .ThenBy(x => x.standardid);
                }
                
            }
            
            GameObject newStack = Instantiate(jengaStackPrefab, this.stack1Pos, Quaternion.identity);
            newStack.GetComponent<JengaStack>().LoadStack(this.stack1, Vector3.zero);
            GameObject newStack2 = Instantiate(jengaStackPrefab, this.stack2Pos, Quaternion.identity);
            newStack2.GetComponent<JengaStack>().LoadStack(this.stack2, Vector3.zero);
            GameObject newStack3 = Instantiate(jengaStackPrefab, this.stack3Pos, Quaternion.identity);
            newStack3.GetComponent<JengaStack>().LoadStack(this.stack3, Vector3.zero);
        }
    }
}
