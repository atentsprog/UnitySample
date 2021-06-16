using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;
using System.Text;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class LinqSample : MonoBehaviour
{
    const string LEVEL = "LEVEL";
    const string NAME = "NAME";
    const string ATTRIBUTE = "ATTRIBUTE";


    enum EnumName
    {
        슬라임,
        가고일,
        골렘,
        코볼트,
        고블린,
        고스트,
        언데드,
        노움,
        드래곤,
        웜,
        서큐버스,
        데빌,
        만티코어,
        바실리스트,
    }

    enum EnumAttribute
    {
        불,
        물,
        바람,
        번개,
        어둠,
        빛,
        땅,
        나무,
    }

    Text tableText;
    Button attributeSortButton;
    Button nameSortButton;
    Button levelSortButton;
    Button filterButton;
    Button cancelButton;
    Dropdown attributeDropdown;
    InputField minLevel;
    InputField maxLevel;

    private void Awake()
    {
        tableText = transform.Find("TableText").GetComponent<Text>();
        attributeSortButton = transform.Find("AttributeSortButton").GetComponent<Button>();
        nameSortButton = transform.Find("NameSortButton").GetComponent<Button>();
        levelSortButton = transform.Find("LevelSortButton").GetComponent<Button>();

        minLevel = transform.Find("MinLevel").GetComponent<InputField>();
        maxLevel = transform.Find("MaxLevel").GetComponent<InputField>();

        attributeDropdown = transform.Find("AttributeDropdown").GetComponent<Dropdown>();
        filterButton = transform.Find("FilterButton").GetComponent<Button>();
        cancelButton = transform.Find("CancelButton").GetComponent<Button>();

        attributeSortButton.AddListener(this, () => { SortFn(SortType.Attribute); });
        nameSortButton.AddListener(this, () => { SortFn(SortType.Name); });
        levelSortButton.AddListener(this, () => { SortFn(SortType.Level); });

        filterButton.AddListener(this, FilterFn);
        cancelButton.AddListener(this, CancelFn);

        CreateData();  // 정보 생성
    }

    private void CancelFn()
    {
        filteredList = null;
        UpdateText();
    }

    List<Row> filteredList;
    private void FilterFn()
    {
        int minLevel = int.Parse(this.minLevel.text);
        int maxLevel = int.Parse(this.maxLevel.text);
        filteredList = list.Where(x => x.level >= minLevel && x.level <= maxLevel
        && x.attribute.ToString() == attributeDropdown.captionText.text).ToList();

        UpdateText();
    }

    private void SortFn(SortType sortType)
    {
        StringBuilder sb = new StringBuilder();
        switch (sortType)
        {
            case SortType.Level:
                list = list.OrderBy(x => x.level).ToList();
                break;
            case SortType.Name:
                list = list.OrderBy(x => x.name).ToList();
                break;
            case SortType.Attribute:
                list = list.OrderBy(x => x.attribute).ToList();
                break;
        }
        UpdateText();
    }

    private void UpdateText()
    {
        var showList = filteredList ?? list;
        StringBuilder sb = new StringBuilder();
        showList.ForEach(x => sb.AppendLine(x.ToString()));
        tableText.text = sb.ToString();
    }

    enum SortType
    {
        Attribute,
        Level,
        Name,
    }
    class Row
    {
        public int level;
        public string name;
        public EnumAttribute attribute;
        public override string ToString()
        {
            return $"{level,-3}{name,-8}{attribute,6}";
        }
    }
    List<Row> list = new List<Row>();
    /// <summary>
    /// Data Table에 자료를 입력
    /// </summary>
    private void CreateData()
    {
        attributeDropdown.options.Clear();
        foreach (var item in Enum.GetNames(typeof(EnumAttribute)))
        {
            attributeDropdown.options.Add(new Dropdown.OptionData(item));
        }
        minLevel.text = "1";
        maxLevel.text = "10";

        Random rd = new Random();
        list.Clear();
        foreach (EnumName oName in Enum.GetValues(typeof(EnumName)))   // ※15강 캡슐화에서 사용
        {
            Row dr = new Row();
            dr.level = rd.Next(1, 11);  // 1 ~ 10 중에서 Random
            dr.name = oName.ToString();  // 이름을 넣어 줌

            int iEnumLength = Enum.GetValues(typeof(EnumAttribute)).Length;  // Enum 의 개수를 가져옴
            int iAttribute = rd.Next(iEnumLength);
            dr.attribute = (EnumAttribute)iAttribute;

            list.Add(dr);
        }

        UpdateText();
    }
}
