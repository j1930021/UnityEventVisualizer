﻿using System.Collections.Generic;
using UnityEngine;

namespace EventVisualizer.Base
{
    public class NodeData
    {
        public Component Entity { get; private set; }

        public string Name
        {
            get
            {
                return Entity != null ? Entity.name : "<Missing>";
            }
        }


        public List<EventCall> Outputs { get; private set; }
        public List<EventCall> Inputs { get; private set; }

        private static Dictionary<Component, NodeData> nodes = new Dictionary<Component, NodeData>();

        public static ICollection<NodeData> Nodes
        {
            get
            {
                return nodes != null ? nodes.Values : null;
            }
        }

        public static void Clear() 
        {
            nodes.Clear();
        }
        
        public static void RegisterEvent(EventCall eventCall)
        {
            CreateNode(eventCall.Sender);
            CreateNode(eventCall.Receiver);

            nodes[eventCall.Sender].Outputs.Add(eventCall);
            nodes[eventCall.Receiver].Inputs.Add(eventCall);
        }

        private static void CreateNode(Component entity)
        {
            if(!nodes.ContainsKey(entity))
            {
                nodes.Add(entity, new NodeData(entity));
            }
        }
        
        public NodeData(Component entity)
        {
            Entity = entity;
            Outputs = new List<EventCall>();
            Inputs = new List<EventCall>();
        }
    }

}