using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        private Node root = null;

        // Start is called before the first frame update
        protected void Start()
        {
            root = SetUpTree();
        }

        // Update is called once per frame
        private void Update()
        {
            if(root != null)
            {
                root.Evaluate();
            }
        }

        protected abstract Node SetUpTree();
        
    }
}

