using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Sample14_page
{
    public class PageUISample : MonoBehaviour
    {
        public List<string> testItems;
        PageUISampleItem baseItem;
        public int currentPage = 1;
        public int countPerPage = 12;
        void Start()
        {
            baseItem = GetComponentInChildren<PageUISampleItem>();

            CreatePageItems(currentPage);
        }

        List<GameObject> items = new List<GameObject>();
        private void CreatePageItems(int _currentPage)
        {
            var pageItems = testItems.Skip((_currentPage -1) * countPerPage).Take(countPerPage).ToList();

            items.ForEach(x => Destroy(x));
            items.Clear();
            baseItem.gameObject.SetActive(true);
            foreach (var item in pageItems)
            {
                PageUISampleItem newItem = Instantiate(baseItem, baseItem.transform.parent);
                newItem.Init(item);
                items.Add(newItem.gameObject);
            }
            baseItem.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                PageMove(-1); // 이전페이지로 이동

            if (Input.GetKeyDown(KeyCode.RightArrow))
                PageMove(1); // 다음 페이지로 이동
        }

        private void PageMove(int offset)
        {
            currentPage += offset;
            CreatePageItems(currentPage);
        }
    }
}
