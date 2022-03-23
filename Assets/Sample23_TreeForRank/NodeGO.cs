using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sample23_TreeForRank
{
    public class NodeGO : MonoBehaviour
    {
        public TreeForRank.Node node;
        public Transform lowTr;
        public Transform highTr;

        internal void Init(TreeForRank.Node node)
        {
            this.node = node;
            node.tr = transform;

            string text = $"{node.data}({node.leftSize})";// node.data.ToString();
            transform.Find("Data").GetComponent<Text>().text = text;
            name = text;
        }
    }
}