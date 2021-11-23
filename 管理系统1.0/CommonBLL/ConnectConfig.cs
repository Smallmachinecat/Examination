using CommonDAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonBLL
{
    [Serializable]
    public class ConnectConfig
    {
        public string DBIPEncrypt = Common.EncryptCode.EncryptStr("127.0.0.1");
        [System.Xml.Serialization.XmlIgnore]
        public string DBIP
        {
            get
            {
                return Common.EncryptCode.DecryptStr(DBIPEncrypt);
            }
            set
            {
                DBIPEncrypt = Common.EncryptCode.EncryptStr(value);
            }
        }

        public string DBUserEncrypt = Common.EncryptCode.EncryptStr("sa");
        [System.Xml.Serialization.XmlIgnore]
        public string DBUser
        {
            get
            {
                return Common.EncryptCode.DecryptStr(DBUserEncrypt);
            }
            set
            {
                DBUserEncrypt = Common.EncryptCode.EncryptStr(value);
            }
        }

        public string DBPasswordEncrypt = Common.EncryptCode.EncryptStr("Admin123");
        [System.Xml.Serialization.XmlIgnore]
        public string DBPassword
        {
            get
            {
                return Common.EncryptCode.DecryptStr(DBPasswordEncrypt);
            }
            set
            {
                DBPasswordEncrypt = Common.EncryptCode.EncryptStr(value);
            }
        }

        public string DBNameEncrypt = Common.EncryptCode.EncryptStr("studata_DDB");
        [System.Xml.Serialization.XmlIgnore]
        public string DBName
        {
            get
            {
                return Common.EncryptCode.DecryptStr(DBNameEncrypt);
            }
            set
            {
                DBNameEncrypt = Common.EncryptCode.EncryptStr(value);
            }
        }

        public string DBPortEncrypt = Common.EncryptCode.EncryptStr("1433");
        [System.Xml.Serialization.XmlIgnore]
        public int DBPort
        {
            get
            {
                try
                {
                    return int.Parse(Common.EncryptCode.DecryptStr(DBPortEncrypt));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            set
            {
                DBPortEncrypt = Common.EncryptCode.EncryptStr(value.ToString());
            }
        }

        /// <summary>
        /// 心跳间隔时间
        /// </summary>
        public int HeartbeatSpanSecond = 10;

        [System.Xml.Serialization.XmlIgnore]
        public static string Caption = "江苏中职机电大类学业水平测试";

        [System.Xml.Serialization.XmlIgnore]
        public SqlConnection DBConnection
        {
            get
            {
                return Common.ConnectionTools.getSqlConnection(DBUser, DBPassword, DBIP, DBName, DBPort);
            }
            //set
            //{
            //    value.Container.
            //}
        }
    }
}
