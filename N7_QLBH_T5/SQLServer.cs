﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.IO;

namespace N7_QLBH_T5
{
    public class SQLServer
    {
        
    

        public string Server { get; set; }
        public string Database { get; set; }

        public string strConnectString = "";// chuỗi kết nối dến CSDL của máy chủ
        public SqlConnection myConnectSQL;// Đối tượng dùng để kết nối đến máy chủ

        public SQLServer()
        {
            strConnectString = "Server = " + Connect_Server.TenMayChu + ";" +
                "Database = " + Connect_Server.TenCSDL + ";" +
                "Integrated Security = true;" +
                "Connection Timeout = 30";

            // Tạo đối tượng kết nối đến CSDL

            myConnectSQL = new SqlConnection();
            myConnectSQL.ConnectionString = strConnectString;

        }

        // Mở kết nối

        public bool Open_KetNoiCSDL()
        {
            try
            {
                if (myConnectSQL.State == System.Data.ConnectionState.Closed)// kiểm tra nếu đóng thì mở 
                {
                    myConnectSQL.Open();
                }
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        // Đóng kết nối

        public bool Close_KetNoiCSDL()
        {
            try
            {
                if (myConnectSQL.State == System.Data.ConnectionState.Open)
                    myConnectSQL.Close();
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }

        // Câu lệnh Select
        public DataTable LayDuLieuBang(string strQuery)
        {
            try
            {
                Open_KetNoiCSDL();
                SqlCommand myCommand = new SqlCommand();
                myCommand.CommandType = CommandType.Text;
                myCommand.CommandText = strQuery;
                myCommand.Connection = myConnectSQL;

                DataTable dataTable = new DataTable();
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = myCommand;
                dataAdapter.Fill(dataTable);// đổ dữ liệu
                Close_KetNoiCSDL();
                return dataTable;


            }
            catch (SqlException ex)
            {
                return null;
            }
        }


        public bool sql_KiemTra(string Query)
        {
            bool kt;
            Open_KetNoiCSDL();
            SqlCommand command = new SqlCommand(Query, myConnectSQL);
            SqlDataReader data = command.ExecuteReader();
            kt = data.Read();
            Close_KetNoiCSDL();
            return kt;


        }
        public string QueryString(string Query)
        {
            string between;

            Open_KetNoiCSDL();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(Query, myConnectSQL);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();

            between = reader[0].ToString();
            Close_KetNoiCSDL();
            return between;
        }


    
}
}
