using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace Sample15_Thread
{
    public class ThreadTest : MonoBehaviour
    {
        public int number = 0;
        public int calculateCount = 100;

        [Header("락 사용 여부")]
        public bool useLock;
        [Header("Unity Namespace에 접근(Text 컴포넌트)")]
        public bool printUIText;
        [Header("메인 쓰레드에서 실행")]
        public bool useMainThread;
        [Header("메인 쓰레드에서 캡쳐한 값으로 실행")]
        public bool useMainThreadValueCapture;

        protected Text resultText;
        private int initNumber = 1000;
        protected StringBuilder sb = new StringBuilder();

        private void Awake()
        {
            initNumber = number;
            resultText = transform.Find("Result").GetComponent<Text>();
            GetComponent<Button>().onClick.AddListener(Run);
        }

        public void Run()
        {
            number = initNumber;
            sb.Clear();
            // 10개의 쓰레드가 동일 메서드 실행
            for (int i = 0; i < 10; i++)
            {
                new Thread(ThreadFn).Start();
            }
        }

        protected Queue<Action> mainThreadFn = new Queue<Action>();
        private void Update()
        {
            while (mainThreadFn.Count > 0)
            {
                //mainThreadFn.Dequeue().Invoke();
                var ac = mainThreadFn.Dequeue();
                if(ac == null)
                {
                    print("ac == null");
                    continue;
                }    
                ac.Invoke();
            }
        }

        //// lock문에 사용될 객체
        private object lockObject = new object();
        private void ThreadFn()
        {
            // 한번에 한 쓰레드만 lock블럭 실행
            if (useLock)
            {
                lock (lockObject)
                {
                    CalculateFn();
                }
            }
            else
            {
                CalculateFn();
            }
        }

        protected void CalculateFn()
        {
            // 필드값 변경
            number++;

            // 가정 : 다른 복잡한 일을 한다
            for (int i = 0; i < number; i++)
                for (int j = 0; j < number; j++) ;

            // 필드값 읽기
            if (printUIText)
            {
                if (useMainThread)
                {
                    if (useMainThreadValueCapture)
                    {
                        int captureNumber = number;
                        mainThreadFn.Enqueue(() =>
                        {
                            sb.AppendLine(captureNumber.ToString());
                            resultText.text = sb.ToString();
                        });
                    }
                    else
                    {
                        mainThreadFn.Enqueue(() =>
                        {
                            sb.AppendLine(number.ToString());
                            resultText.text = sb.ToString();
                        });
                    }
                }
                else
                {
                    sb.AppendLine(number.ToString());
                    resultText.text = sb.ToString();
                }
            }
            else
                Debug.Log(number);
        }
    }
}
