using System.Collections.Generic;
using System;
using UnityEngine;

namespace Tool.Database
{
    public class GlobalConfigData
    {
        /// <summary>
		///广告公共cd
		/// </summary>
		public int adCommonCD;
    }

    public class GlobalConfigDatabase : IDatabase
    {
        public uint TYPE_ID =1;
        public const string DATA_PATH ="Config/GlobalConfig";

        private List<GlobalConfigData> m_datas;

        public  GlobalConfigDatabase() { }

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

		private List<GlobalConfigData> GetAllData(string[][] m_datas)
		{
			List<GlobalConfigData> m_tempList = new List<GlobalConfigData>();
			for (int i = 0; i < m_datas.Length; i++)
            {
				GlobalConfigData m_tempData = new GlobalConfigData();
                
				if (!int.TryParse(m_datas[i][0].Trim(),out m_tempData.adCommonCD))
				{
					m_tempData.adCommonCD=0;
				}

				m_tempList.Add(m_tempData);
            }
            return m_tempList;
		}

        public GlobalConfigData GetDataByKey(int key)
        {
            foreach (var data in m_datas)
            {
                if (data.adCommonCD == key)
                    return data;
            }

            return default;
        }

		public List<GlobalConfigData> FindAll(Predicate<GlobalConfigData> handler = null)
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

