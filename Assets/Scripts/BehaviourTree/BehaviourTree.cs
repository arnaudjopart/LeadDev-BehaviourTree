
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ajc.BehaviourTree
{
    public class BehaviourTree
    {
        public BehaviourTree()
        {
            m_nodes = new List<Node>();
        }
        public List<Node> m_nodes;
        private int m_index;
        
        public void Tick(float _deltaTime)
        {
            var status = m_nodes[m_index].Process(_deltaTime);
            if(status != Node.STATUS.Running)
            {
                m_index = (m_index+1)%m_nodes.Count;
            }
        }

        public void AddNode(Node _node)
        {
            m_nodes.Add(_node);
        }
    }
}