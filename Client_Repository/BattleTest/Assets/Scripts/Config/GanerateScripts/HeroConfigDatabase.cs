using System.Collections.Generic;
using System;
using UnityEngine;

namespace Tool.Database
{
    public class HeroConfigData
    {
        /// <summary>
		///ID
		/// </summary>
		public int ID;
		/// <summary>
		///名称
		/// </summary>
		public string name;
		/// <summary>
		///备注
		/// </summary>
		public string beizhu;
		/// <summary>
		///兵种
		/// </summary>
		public int armyType;
		/// <summary>
		///默认品质
		/// </summary>
		public int defaultQuality;
		/// <summary>
		///默认携带士兵ID
		/// </summary>
		public int defaultOwnSoldierID;
		/// <summary>
		///预制体名称
		/// </summary>
		public string prefabName;
		/// <summary>
		///英雄头像
		/// </summary>
		public string heroHead;
		/// <summary>
		///基础移动速度
		/// </summary>
		public float baseMoveSpeed;
		/// <summary>
		///普通攻击技能ID
		/// </summary>
		public int normalAttackID;
		/// <summary>
		///星级对应技能ID
		/// </summary>
		public int[] starUpSkillIDs;
		/// <summary>
		///碎片ID
		/// </summary>
		public int fragID;
		/// <summary>
		///获得相同英雄后转化碎片数量
		/// </summary>
		public int fragChangeCount;
		/// <summary>
		///升星消耗碎片数量
		/// </summary>
		public int[] starUpFragCost;
		/// <summary>
		///升星资质系数
		/// </summary>
		public float[] starupParam;
		/// <summary>
		///对应等级表
		/// </summary>
		public int[] levelExcel;
    }

    public class HeroConfigDatabase : IDatabase
    {
        public uint TYPE_ID =2;
        public const string DATA_PATH ="Config/HeroConfig";

        private List<HeroConfigData> m_datas;

        public  HeroConfigDatabase() { }

        public uint TypeID()
        {
            return TYPE_ID;
        }

        public string DataPath()
        {
            return DATA_PATH;
        }

        public void Load()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(DataPath());
            m_datas = GetAllData(CSVConverter.SerializeCSVData(textAsset));
        }

		private List<HeroConfigData> GetAllData(string[][] m_datas)
		{
			List<HeroConfigData> m_tempList = new List<HeroConfigData>();
			for (int i = 0; i < m_datas.Length; i++)
            {
				HeroConfigData m_tempData = new HeroConfigData();
                
				if (!int.TryParse(m_datas[i][0].Trim(),out m_tempData.ID))
				{
					m_tempData.ID=0;
				}

					m_tempData.name=m_datas[i][1];
					m_tempData.name = m_tempData.name.Replace("\\n", "\n");
					m_tempData.beizhu=m_datas[i][2];
					m_tempData.beizhu = m_tempData.beizhu.Replace("\\n", "\n");
					
				if (!int.TryParse(m_datas[i][3].Trim(),out m_tempData.armyType))
				{
					m_tempData.armyType=0;
				}

					
				if (!int.TryParse(m_datas[i][4].Trim(),out m_tempData.defaultQuality))
				{
					m_tempData.defaultQuality=0;
				}

					
				if (!int.TryParse(m_datas[i][5].Trim(),out m_tempData.defaultOwnSoldierID))
				{
					m_tempData.defaultOwnSoldierID=0;
				}

					m_tempData.prefabName=m_datas[i][6];
					m_tempData.prefabName = m_tempData.prefabName.Replace("\\n", "\n");
					m_tempData.heroHead=m_datas[i][7];
					m_tempData.heroHead = m_tempData.heroHead.Replace("\\n", "\n");
					
				if (!float.TryParse(m_datas[i][8].Trim(),out m_tempData.baseMoveSpeed))
				{
					m_tempData.baseMoveSpeed=0.0f;
				}

					
				if (!int.TryParse(m_datas[i][9].Trim(),out m_tempData.normalAttackID))
				{
					m_tempData.normalAttackID=0;
				}

					m_tempData.starUpSkillIDs=CSVConverter.ConvertToArray<int>(m_datas[i][10].Trim());
					
				if (!int.TryParse(m_datas[i][11].Trim(),out m_tempData.fragID))
				{
					m_tempData.fragID=0;
				}

					
				if (!int.TryParse(m_datas[i][12].Trim(),out m_tempData.fragChangeCount))
				{
					m_tempData.fragChangeCount=0;
				}

					m_tempData.starUpFragCost=CSVConverter.ConvertToArray<int>(m_datas[i][13].Trim());
					m_tempData.starupParam=CSVConverter.ConvertToArray<float>(m_datas[i][14].Trim());
					m_tempData.levelExcel=CSVConverter.ConvertToArray<int>(m_datas[i][15].Trim());
				m_tempList.Add(m_tempData);
            }
            return m_tempList;
		}

        public HeroConfigData GetDataByKey(int key)
        {
            foreach (var data in m_datas)
            {
                if (data.ID == key)
                    return data;
            }

            return default;
        }

		public List<HeroConfigData> FindAll(Predicate<HeroConfigData> handler = null)
		{
			if (handler == null)
            {
                return m_datas;
            }
            else
            {
                return m_datas.FindAll(handler);
            }
		}

        public int GetCount()
        {
			return m_datas.Count;
        }
    }
}

