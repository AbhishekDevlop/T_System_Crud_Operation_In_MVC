using System.Data.SqlClient;
using System.Collections;
using System.Linq;
using CRUD_Operation_In_MVC.Controllers;
using CRUD_Operation_In_MVC.Models;
using System.Collections.Generic;
using System;

namespace CRUD_Operation_In_MVC.Models
{
    public class ProductDAL
    {
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

        public ProductDAL()
        {
           con = new SqlConnection(Startup.ConnectionString);
            //con = new SqlConnection(@"Server = DESKTOP-INLRLPK\SQLEXPRESS;DataBase = T_System_Training;Integrated Security = True");
        }

        public List<Products> GetAllProducts()
        {
            List<Products> list = new List<Products>();
            cmd = new SqlCommand("select * from Products", con);
            con.Open();
            dr = cmd.ExecuteReader();
            list = ArrageList(dr);
            con.Close();
            return list;
        }
        public int Save(Products p)
        {
            cmd = new SqlCommand("insert into Products values(@name,@price)", con);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@price", p.Price);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public List<Products> ArrageList(SqlDataReader dr)
        {
            List<Products> list = new List<Products>();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    Products p = new Products();
                    p.Id = Convert.ToInt32(dr["ProductId"]);
                    p.Name = dr["ProductName"].ToString();
                    p.Price = Convert.ToDecimal(dr["ProductPrice"]);
                    list.Add(p);
                }
                return list;
            }
            else
            {
                return null;
            }
        }
        public Products GetProductById(int id)
        {
            Products prod = new Products();
            cmd = new SqlCommand("select * from Products where ProductId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    prod.Id = Convert.ToInt32(dr["ProductId"]);
                    prod.Name = dr["ProductName"].ToString();
                    prod.Price = Convert.ToDecimal(dr["ProductPrice"]);
                }

            }
            con.Close();
            return prod;
        }

        public int Upate(Products p)
        {
            cmd = new SqlCommand("update Products set ProductName=@name,ProductPrice=@price where ProductId=@id", con);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@id", p.Id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
        public int Delete(int id)
        {

            cmd = new SqlCommand("delete from Products where ProductId=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }



    }
}
