using System;
using System.Collections.Generic;
using backend.Interface;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Oracle.ManagedDataAccess.Client;
using backend.Models;
namespace backend.Services
{
    public class StudentService : IStudentService
    {
        private readonly string _context;
        public StudentService(IConfiguration _configuration)
        {
            _context = _configuration.GetConnectionString("PostgressConnection");
        }


        public IEnumerable<Student> GetAllStudent()
        {
            List<Student> studentList = new List<Student>();
            using (NpgsqlConnection con = new NpgsqlConnection(_context))
            {
                // using (OracleCommand cmd = new OracleCommand())  //error
                // using (NpgsqlCommand cmd = con.CreateCommand())
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from student", con))
                {
                    con.Open();
                    // cmd.BindByName = true;
                    // cmd.CommandText = "select * from bank";
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Student student = new Student
                        {
                            id = Convert.ToInt32(reader["id"]),
                            name = reader["name"].ToString(),
                            study = reader["study"].ToString(),
                            test = reader["test"].ToString(),
                            grade = reader["grade"].ToString(),
                            finance = reader["finance"].ToString(),
                            lineuserid = reader["lineUserId"].ToString(),


                        };

                        studentList.Add(student);
                    }

                }
            }
            return studentList;
        }

        public Student GetStudentByID(string lineUserId)
        {
            Student student = new Student();
            using (NpgsqlConnection con = new NpgsqlConnection(_context))
            {
                // using (OracleCommand cmd = new OracleCommand())  //error
                // using (NpgsqlCommand cmd = con.CreateCommand())
                using (NpgsqlCommand cmd = new NpgsqlCommand("select * from student where lineUserId = @p", con))
                {
                    con.Open();
                    // cmd.BindByName = true;
                    // cmd.CommandText = "select * from bank";
                    cmd.Parameters.AddWithValue("p", lineUserId);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        student.id = Convert.ToInt32(reader["id"]);
                        student.name = reader["name"].ToString();
                        student.study = reader["study"].ToString();
                        student.test = reader["test"].ToString();
                        student.grade = reader["grade"].ToString();
                        student.finance = reader["finance"].ToString();
                        student.lineuserid = reader["lineuserid"].ToString();
                    }

                }
            }
            return student;
        }
        //



    }
}