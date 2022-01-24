using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tool.Database
{
	public class ConfigDataMgr : Singleton<ConfigDataMgr>
	{
		public Dictionary<uint, IDatabase> m_Databases;

		public ConfigDataMgr()
		{
			m_Databases = new Dictionary<uint, IDatabase>();
			RegisterDataType(new GlobalConfigDatabase());
			RegisterDataType(new HeroConfigDatabase());
			RegisterDataType(new MonsterConfigDatabase());

			Load();
		}

		public void Load()
		{
			foreach (KeyValuePair<uint, IDatabase> data in m_Databases)
			{
				data.Value.Load();
			}
		}

		private T GetDatabase<T>() where T : IDatabase
		{
            foreach (var item in m_Databases.Values)
            {
                if (item is T database)
                {
					return database;
                }
            }
			return default;
		}

		private void RegisterDataType(IDatabase database)
		{
			m_Databases[database.TypeID()] = database;
		}

		public GlobalConfigData getGlobalConfigData(int nId, bool bEmptyLog = true)
		{
			var data = GetDatabase<GlobalConfigDatabase>().GetDataByKey(nId);
			if (bEmptyLog && data == null && nId != int.MaxValue)
				Debuger.Log("GlobalConfig Is Null! ID:" + nId);
			return data;
		}
		public List<GlobalConfigData> getAllGlobalConfigData()
		{
			return GetDatabase<GlobalConfigDatabase>().FindAll();
		}
		public HeroConfigData getHeroConfigData(int nId, bool bEmptyLog = true)
		{
			var data = GetDatabase<HeroConfigDatabase>().GetDataByKey(nId);
			if (bEmptyLog && data == null && nId != int.MaxValue)
				Debuger.Log("HeroConfig Is Null! ID:" + nId);
			return data;
		}
		public List<HeroConfigData> getAllHeroConfigData()
		{
			return GetDatabase<HeroConfigDatabase>().FindAll();
		}
		public MonsterConfigData getMonsterConfigData(int nId, bool bEmptyLog = true)
		{
			var data = GetDatabase<MonsterConfigDatabase>().GetDataByKey(nId);
			if (bEmptyLog && data == null && nId != int.MaxValue)
				Debuger.Log("MonsterConfig Is Null! ID:" + nId);
			return data;
		}
		public List<MonsterConfigData> getAllMonsterConfigData()
		{
			return GetDatabase<MonsterConfigDatabase>().FindAll();
		}

		private static GlobalConfigData mGlobalData = null;
        public static GlobalConfigData GlobalData
        {
            get
            {
                if (mGlobalData == null)
                {
                    var globalList = Ins.getAllGlobalConfigData();
                    if (globalList.Count != 1)
                    {
                        Debuger.LogError("GlobalConfigDatabase is not 1 line");
                        return null;
                    }
                    mGlobalData = globalList[0];
                }
                return mGlobalData;
            }
        }
	}
}
