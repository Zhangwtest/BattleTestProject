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
		/// <summary>
		///加速扩建消耗钻石基数
		/// </summary>
		public double[] speedUpCostDiamondBase;
		/// <summary>
		///初始值（itemID Value）
		/// </summary>
		public double[][] initItemData;
		/// <summary>
		///修建队列上限
		/// </summary>
		public int buildingQueueMax;
		/// <summary>
		///最近制造显示上限
		/// </summary>
		public int latestEquipMakeShowMax;
		/// <summary>
		///订单奖励装备对应钻石数量
		/// </summary>
		public int[] orderDiamondArray;
		/// <summary>
		///订单所需普通装备品质
		/// </summary>
		public int[][] orderQuality;
		/// <summary>
		///订单所需普通装备数量
		/// </summary>
		public int[][] orderNumData;
		/// <summary>
		///订单高级装备爆率
		/// </summary>
		public int[] orderBlastArray;
		/// <summary>
		///重置订单消耗钻石数
		/// </summary>
		public int orderResetCost;
		/// <summary>
		///装备材料不足钻石基础消耗
		/// </summary>
		public int[] EquipMakeBaseCost;
		/// <summary>
		///材料不足时，材料或装备加值
		/// </summary>
		public int[] EquipMakeAdd;
		/// <summary>
		///信任值加减
		/// </summary>
		public int[] trustParam;
		/// <summary>
		///打折和涨价系数
		/// </summary>
		public float[] priceChange;
		/// <summary>
		///顾客求购界面特殊表现时间间隔
		/// </summary>
		public float cusBuyingInterval;
		/// <summary>
		///顾客行为权重
		/// </summary>
		public int[] cusBehaviourPro;
		/// <summary>
		///顾客求购权重
		/// </summary>
		public int[] cusBuyingPro;
		/// <summary>
		///顾客从桥派遣间隔时间
		/// </summary>
		public float[] cusFromBridge;
		/// <summary>
		///顾客从码头派遣时间间隔
		/// </summary>
		public float[] cusFormMatou;
		/// <summary>
		///顾客在部落停留时间
		/// </summary>
		public float[] cusStayInTribe;
		/// <summary>
		///顾客求购间隔时间
		/// </summary>
		public float[] cusBuyInterval;
		/// <summary>
		///初始顾客上限
		/// </summary>
		public int cusOriginNum;
		/// <summary>
		///特殊顾客求购时间
		/// </summary>
		public float cusWaitMinite;
		/// <summary>
		///大地图点位刷新间隔（分钟）
		/// </summary>
		public float mapRefreshTimeMin;
		/// <summary>
		///大地图占领点敌人名字
		/// </summary>
		public string[] mapEnemyNames;
		/// <summary>
		///体力参数
		/// </summary>
		public float[] energyParam;
		/// <summary>
		///玩家名字长度限制
		/// </summary>
		public int[] cusNameLimit;
		/// <summary>
		///人物采集动作切换随机时间
		/// </summary>
		public float[] playerRandomTime;
		/// <summary>
		///属性换算战斗力系数
		/// </summary>
		public float[][] propToFightingValParam;
		/// <summary>
		///战斗力换算属性系数
		/// </summary>
		public float[][] fightingValToPropParam;
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

					m_tempData.speedUpCostDiamondBase=CSVConverter.ConvertToArray<double>(m_datas[i][1].Trim());
					m_tempData.initItemData=CSVConverter.ConvertToMultiArray<double>(m_datas[i][2].Trim());
					
				if (!int.TryParse(m_datas[i][3].Trim(),out m_tempData.buildingQueueMax))
				{
					m_tempData.buildingQueueMax=0;
				}

					
				if (!int.TryParse(m_datas[i][4].Trim(),out m_tempData.latestEquipMakeShowMax))
				{
					m_tempData.latestEquipMakeShowMax=0;
				}

					m_tempData.orderDiamondArray=CSVConverter.ConvertToArray<int>(m_datas[i][5].Trim());
					m_tempData.orderQuality=CSVConverter.ConvertToMultiArray<int>(m_datas[i][6].Trim());
					m_tempData.orderNumData=CSVConverter.ConvertToMultiArray<int>(m_datas[i][7].Trim());
					m_tempData.orderBlastArray=CSVConverter.ConvertToArray<int>(m_datas[i][8].Trim());
					
				if (!int.TryParse(m_datas[i][9].Trim(),out m_tempData.orderResetCost))
				{
					m_tempData.orderResetCost=0;
				}

					m_tempData.EquipMakeBaseCost=CSVConverter.ConvertToArray<int>(m_datas[i][10].Trim());
					m_tempData.EquipMakeAdd=CSVConverter.ConvertToArray<int>(m_datas[i][11].Trim());
					m_tempData.trustParam=CSVConverter.ConvertToArray<int>(m_datas[i][12].Trim());
					m_tempData.priceChange=CSVConverter.ConvertToArray<float>(m_datas[i][13].Trim());
					
				if (!float.TryParse(m_datas[i][14].Trim(),out m_tempData.cusBuyingInterval))
				{
					m_tempData.cusBuyingInterval=0.0f;
				}

					m_tempData.cusBehaviourPro=CSVConverter.ConvertToArray<int>(m_datas[i][15].Trim());
					m_tempData.cusBuyingPro=CSVConverter.ConvertToArray<int>(m_datas[i][16].Trim());
					m_tempData.cusFromBridge=CSVConverter.ConvertToArray<float>(m_datas[i][17].Trim());
					m_tempData.cusFormMatou=CSVConverter.ConvertToArray<float>(m_datas[i][18].Trim());
					m_tempData.cusStayInTribe=CSVConverter.ConvertToArray<float>(m_datas[i][19].Trim());
					m_tempData.cusBuyInterval=CSVConverter.ConvertToArray<float>(m_datas[i][20].Trim());
					
				if (!int.TryParse(m_datas[i][21].Trim(),out m_tempData.cusOriginNum))
				{
					m_tempData.cusOriginNum=0;
				}

					
				if (!float.TryParse(m_datas[i][22].Trim(),out m_tempData.cusWaitMinite))
				{
					m_tempData.cusWaitMinite=0.0f;
				}

					
				if (!float.TryParse(m_datas[i][23].Trim(),out m_tempData.mapRefreshTimeMin))
				{
					m_tempData.mapRefreshTimeMin=0.0f;
				}

					m_tempData.mapEnemyNames=CSVConverter.ConvertToArray<string>(m_datas[i][24].Trim());
					m_tempData.energyParam=CSVConverter.ConvertToArray<float>(m_datas[i][25].Trim());
					m_tempData.cusNameLimit=CSVConverter.ConvertToArray<int>(m_datas[i][26].Trim());
					m_tempData.playerRandomTime=CSVConverter.ConvertToArray<float>(m_datas[i][27].Trim());
					m_tempData.propToFightingValParam=CSVConverter.ConvertToMultiArray<float>(m_datas[i][28].Trim());
					m_tempData.fightingValToPropParam=CSVConverter.ConvertToMultiArray<float>(m_datas[i][29].Trim());
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

