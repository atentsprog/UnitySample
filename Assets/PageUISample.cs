using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Sample14_page
{
    public class PageUISample : MonoBehaviour
    {
        public List<string> testItems;
        PageUISampleItem baseItem;
        public int currentPage = 1;
        public int countPerPage = 12;
        int MaxPage => (testItems.Count / countPerPage) + 1;

        Text pageText;
        void Start()
        {
            pageText = transform.Find("PageText").GetComponent<Text>();

            transform.Find("PreviousButton").GetComponent<Button>().onClick.AddListener(() => PageMove(-1));
            transform.Find("NextButton").GetComponent<Button>().onClick.AddListener(() => PageMove(1));

            baseItem = GetComponentInChildren<PageUISampleItem>();

            CreatePageItems(currentPage);
        }


        List<GameObject> items = new List<GameObject>();
        private void CreatePageItems(int _currentPage)
        {
            var pageItems = testItems
                .Skip((_currentPage -1) * countPerPage)
                .Take(countPerPage);

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

            pageText.text = $"{_currentPage}/{MaxPage}";
        }

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

            currentPage = Math.Max(1, currentPage);
            currentPage = Math.Min(MaxPage, currentPage);

            CreatePageItems(currentPage);
        }
    }
}
