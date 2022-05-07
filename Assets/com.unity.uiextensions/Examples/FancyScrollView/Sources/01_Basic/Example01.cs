﻿/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Linq;

namespace UnityEngine.UI.Extensions.Examples.FancyScrollViewExample01
{
    class Example01 : MonoBehaviour
    {
        [SerializeField] ScrollView scrollView = default;

        public ItemData[] items = Enumerable.Range(0, 20)
               .Select(i => new ItemData($"Cell {i}"))
               .ToArray();
        //void Start()
        //{
        //    scrollView.UpdateData(items);
        //}

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.A))
                scrollView.UpdateData(items);
        }
    }
}
