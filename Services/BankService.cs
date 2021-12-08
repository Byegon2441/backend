using System;
using System.Collections.Generic;
using backend.Interface;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using backend.Models;
namespace backend.Services
{
    public class BankService : IBankService
    {
        private readonly string _context;
        public BankService(IConfiguration _configuration)
        {
            _context = _configuration.GetConnectionString("PostgressConnection");
        }

        public void AddBank(Bank bank)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_context))
            {
                con.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO bank (BANKCODE,BANKNAME,BANKACCOUNT) VALUES (@BANKCODE,@BANKNAME,@BANKACCOUNT)", con))
                {
                    cmd.Parameters.AddWithValue("BANKCODE", bank.BANKCODE);
                    cmd.Parameters.AddWithValue("BANKNAME", bank.BANKNAME);
                    cmd.Parameters.AddWithValue("BANKACCOUNT", bank.BANKACCOUNT);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DelBank(string bankcode)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_context))
            {
                con.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("DELETE FROM bank WHERE BANKCODE = @BANKCODE", con))
                {
                    cmd.Parameters.AddWithValue("BANKCODE", bankcode);
                    cmd.ExecuteNonQuery();

                }
            }
        }

        public IEnumerable<Bank> GetAllBank()
        {
            List<Bank> bankList = new List<Bank>();
            using (NpgsqlConnection con = new NpgsqlConnection(_context))
            {
                // using (OracleCommand cmd = new OracleCommand())  //error
                // using (NpgsqlCommand cmd = con.CreateCommand())
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from bank", con))
                {
                    con.Open();
                    // cmd.BindByName = true;
                    // cmd.CommandText = "select * from bank";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Bank bank = new Bank
                        {
                            BANKCODE = reader["BANKCODE"].ToString(),
                            BANKNAME = reader["BANKNAME"].ToString(),
                            BANKACCOUNT = reader["BANKACCOUNT"].ToString(),
                            TRANSACTIONFEE = reader["TRANSACTIONFEE"] == DBNull.Value ? null : Convert.ToInt32(reader["TRANSACTIONFEE"]),
                            VOUCHERTYPECODE = reader["VOUCHERTYPECODE"].ToString(),
                            SERVICECODE = reader["SERVICECODE"].ToString(),
                            BANKFEEID = reader["BANKFEEID"] == DBNull.Value ? null : Convert.ToInt32(reader["BANKFEEID"]),
                            BANKNAMEABB = reader["BANKNAMEABB"].ToString(),
                            BANKNAMEABBENG = reader["BANKNAMEABBENG"].ToString(),
                            BANKDESCRIPTION = reader["BANKDESCRIPTION"].ToString(),
                            BANKDESCRIPTIONENG = reader["BANKDESCRIPTIONENG"].ToString(),
                            BANKFILETYPE = reader["BANKFILETYPE"].ToString(),
                            BANKFILECODE = reader["BANKFILECODE"].ToString(),
                            ORDERNO = reader["ORDERNO"] == DBNull.Value ? null : Convert.ToInt32(reader["ORDERNO"]),
                            IMAGEURL = reader["IMAGEURL"].ToString()

                        };

                        bankList.Add(bank);
                    }

                }
            }
            return bankList;
        }

        public Bank GetBankByID()
        {
            Bank bank = new Bank();
            //
            return bank;
        }

        public void UpdateBank(Bank bank)
        {
            using (NpgsqlConnection con = new NpgsqlConnection(_context))
            {
                con.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand("UPDATE bank SET BANKNAME = @BANKNAME,BANKACCOUNT = @BANKACCOUNT WHERE BANKCODE = @BANKCODE", con))
                {
                    cmd.Parameters.AddWithValue("BANKCODE", bank.BANKCODE);
                    cmd.Parameters.AddWithValue("BANKNAME", bank.BANKNAME);
                    cmd.Parameters.AddWithValue("BANKACCOUNT", bank.BANKACCOUNT);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}