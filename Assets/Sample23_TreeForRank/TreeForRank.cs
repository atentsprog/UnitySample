using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sample23_TreeForRank
{
    public class TreeForRank : MonoBehaviour
    {
        public NodeGO baseNode;
        public int[] arr = { 5, 1, 4, 4, 5, 9, 7, 13, 3 };
        public int targetNum;

        public Node root;
        void Start()
        {
            int n = arr.Length;
            int x = 4;
            root = null;
            for (int i = 0; i < n; i++)
                root = insert(root, arr[i]);

            MakeNode();
        }

        private void MakeNode()
        {
            baseNode.gameObject.SetActive(true);
            MakeNode(root, baseNode.transform, baseNode.transform.parent);
            baseNode.gameObject.SetActive(false);

            int maxDepth = GetMaxDepth(root, 0);
            print($"maxDepth:{maxDepth}");

            ChangeXPosition(root, maxDepth, 0);
        }
        public float baseWidth = 20;

        private void ChangeXPosition(Node node, int depth, int direction)
        {
            if (node == null)
                return;

            depth--;

            float depthWidth = Mathf.Pow(2, depth) * baseWidth;
            node.tr.localPosition = new Vector3(depthWidth * direction, 0, 0);

            ChangeXPosition(node.low, depth, -1);
            ChangeXPosition(node.high, depth, 1);
        }

        private int GetMaxDepth(Node node, int currentDepth)
        {
            if (node == null)
                return currentDepth;

            currentDepth++;

            return Mathf.Max(GetMaxDepth(node.low, currentDepth), GetMaxDepth(node.high, currentDepth));
        }

        private void MakeNode(Node node, Transform positionTr, Transform parent)
        {
            if (node == null)
            {
                positionTr.GetComponent<Text>().text = "";
                return;
            }

            NodeGO newNode = Instantiate(baseNode, parent);
            newNode.transform.position = positionTr.position;
            newNode.Init(node);

            MakeNode(node.low, newNode.lowTr, newNode.lowTr);
            MakeNode(node.high, newNode.highTr, newNode.highTr);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                PrintRank(targetNum);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                var sortedList = arr.OrderBy(x => x);
                foreach (var item in sortedList)
                    PrintRank(item);
            }
        }

        private void PrintRank(int findNum)
        {
            print("Rank of " + findNum +
                                            " in stream is : " +
                                            getRank(root, findNum));
        }

        [System.Serializable]
        public class Node
        {
            public int data;
            public Node low;
            public Node high;
            public int leftSize;
            public Transform tr;

            public override string ToString() => $"{data}({leftSize})";
        }

        static Node newNode(int data)
        {
            Node temp = new Node();
            temp.data = data;
            temp.low = null;
            temp.high = null;
            temp.leftSize = 0;
            return temp;
        }

        // Inserting a new Node.
        static Node insert(Node node, int data)
        {
            if (node == null)
                return newNode(data);

            // Updating size of left subtree.
            if (data <= node.data)
            {
                node.low = insert(node.low, data);
                node.leftSize++;
            }
            else
                node.high = insert(node.high, data);

            return node;
        }

        // Function to get Rank of a Node x.
        static int getRank(Node root, int x)
        {
            // Step 1.
            if (root.data == x)
                return root.leftSize;

            // Step 2.
            if (x < root.data)
            {
                if (root.low == null)
                    return -1;
                else
                    return getRank(root.low, x);
            }

            // Step 3.
            else
            {
                if (root.high == null)
                    return -1;
                else
                {
                    int rightSize = getRank(root.high, x);
                    if (rightSize == -1) return -1;
                    return root.leftSize + 1 + rightSize;
                }
            }
        }
    }
}