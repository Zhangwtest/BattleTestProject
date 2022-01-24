using System.Collections.Generic;
using System;
using UnityEngine;

namespace Tool.Database
{
    public class MonsterConfigData
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
		///默认品质
		/// </summary>
		public int defaultQuality;
		/// <summary>
		///预制体名称
		/// </summary>
		public string prefabName;
		/// <summary>
		///怪物头像
		/// </summary>
		public string heroHead;
		/// <summary>
		///移动速度
		/// </summary>
		public float baseMoveSpeed;
		/// <summary>
		///生命值
		/// </summary>
		public int baseHp;
		/// <summary>
		///攻击力
		/// </summary>
		public int baseAttack;
		/// <summary>
		///防御力
		/// </summary>
		public int baseDefence;
    }

    public class MonsterConfigDatabase : IDatabase
    {
        public uint TYPE_ID =3;
        public const string DATA_PATH ="Config/MonsterConfig";

        private List<MonsterConfigData> m_datas;

        public  MonsterConfigDatabase() { }

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

		private List<MonsterConfigData> GetAllData(string[][] m_datas)
		{
			List<MonsterConfigData> m_tempList = new List<MonsterConfigData>();
			for (int i = 0; i < m_datas.Length; i++)
            {
				MonsterConfigData m_tempData = new MonsterConfigData();
                
				if (!int.TryParse(m_datas[i][0].Trim(),out m_tempData.ID))
				{
					m_tempData.ID=0;
				}

					m_tempData.name=m_datas[i][1];
					m_tempData.name = m_tempData.name.Replace("\\n", "\n");
					m_tempData.beizhu=m_datas[i][2];
					m_tempData.beizhu = m_tempData.beizhu.Replace("\\n", "\n");
					
				if (!int.TryParse(m_datas[i][3].Trim(),out m_tempData.defaultQuality))
				{
					m_tempData.defaultQuality=0;
				}

					m_tempData.prefabName=m_datas[i][4];
					m_tempData.prefabName = m_tempData.prefabName.Replace("\\n", "\n");
					m_tempData.heroHead=m_datas[i][5];
					m_tempData.heroHead = m_tempData.heroHead.Replace("\\n", "\n");
					
				if (!float.TryParse(m_datas[i][6].Trim(),out m_tempData.baseMoveSpeed))
				{
					m_tempData.baseMoveSpeed=0.0f;
				}

					
				if (!int.TryParse(m_datas[i][7].Trim(),out m_tempData.baseHp))
				{
					m_tempData.baseHp=0;
				}

					
				if (!int.TryParse(m_datas[i][8].Trim(),out m_tempData.baseAttack))
				{
					m_tempData.baseAttack=0;
				}

					
				if (!int.TryParse(m_datas[i][9].Trim(),out m_tempData.baseDefence))
				{
					m_tempData.baseDefence=0;
				}

				m_tempList.Add(m_tempData);
            }
            return m_tempList;
		}

        public MonsterConfigData GetDataByKey(int key)
        {
            foreach (var data in m_datas)
            {
                if (data.ID == key)
                    return data;
            }

            return default;
        }

		public List<MonsterConfigData> FindAll(Predicate<MonsterConfigData> handler = null)
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

