using System;
using System.Data.SQLite;
using s18085_egz1.Controllers;
using s18085_egz1.Model;

namespace s18085_egz1.Services
{
    public class DbService : IDbService
    {
        private string dbConString = "Data Source=/Users/DamianGoraj/Documents/DBs/sqlite-tools-osx-x86-3310100/s18085.db";

        public DbService() 
        {

        }

        public bool DeletePatient(string id)
        {
            using (var con = new SQLiteConnection(dbConString))
            using (var com = con.CreateCommand())
            {
                com.CommandText = "select * from Patient where idPatient = @id";
                com.Parameters.AddWithValue("id", id);
                con.Open();
                using (var dr = com.ExecuteReader())
                {
                    if (!dr.Read())
                    {
                        return false;
                    }
                }

                com.Reset();
                SQLiteTransaction trans = con.BeginTransaction();
                com.Transaction = trans;
                com.CommandText =   "delete from Prescription_Mediciment where IdPrescription in" +
                                    "(select IdPrescription from Prescription where IdPatient = @id)";
                com.Parameters.AddWithValue("id", id);
                com.ExecuteNonQuery();
                com.CommandText = "delete from Prescription where IdPatient = @id";
                com.Parameters.AddWithValue("id", id);
                com.ExecuteNonQuery();
                trans.Commit();
                return true;
            }
        }

        public Mediciment FindMedicimentsWithPrescr(string id)
        {
            Mediciment mediciment = null;
            using (var con = new SQLiteConnection(dbConString))
            using (var com = con.CreateCommand())
            {
                com.CommandText = "select * from Mediciament where IdMediciment = @id";
                com.Parameters.AddWithValue("id", id);
                con.Open();
                using (var dr = com.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        mediciment = new Mediciment();
                        mediciment.IdMediciment = int.Parse(dr["IdMediciment"].ToString());
                        mediciment.Name = dr["Name"].ToString();
                        mediciment.Decription = dr["Description"].ToString();
                        mediciment.Type = dr["Type"].ToString();

                        com.Reset();
                        com.CommandText =   "Select p.* from Mediciment" +
                                            "join Prescription_Mediciment pm on pm.IdMediciment = @medId" +
                                            "join Prescription p on p.IdPrescription = pm.IdPrescription" ;
                        com.Parameters.AddWithValue("medId", id);
                        using (var dr2 = com.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                var per = new Prescription();
                                per.IdPrescription = int.Parse(dr["IdPrescription"].ToString());
                                per.Date = dr["Date"].ToString();
                                per.DueDate = dr["DueDate"].ToString();
                                mediciment.Prescriptions.Add(per);
                            }
                        }
                    }
                }
            }
            return mediciment;
        }
    }
}
