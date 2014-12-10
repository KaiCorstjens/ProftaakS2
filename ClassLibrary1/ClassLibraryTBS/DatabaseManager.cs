using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace ClassLibrary
{
    public static class DatabaseManager
    {
        private static OracleConnection connection;

        static DatabaseManager()
        {
            connection = new OracleConnection();
            #region Secret

            #region No really!
            connection.ConnectionString = "User Id=dbi297953;Password=hN0VRtdlS0;Data Source=" + " //192.168.15.50:1521/fhictora" + ";";
            #endregion

            #endregion
        }

        public static List<Remise> LaadRemises()
        {
            List<Remise> remises = new List<Remise>();
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                OracleCommand command = new OracleCommand("SELECT * FROM REMISE");
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                OracleDataReader reader = command.ExecuteReader();

                if (!reader.HasRows) return remises;
                else
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        string naam = Convert.ToString(reader["Naam"]);
                        int groteServiceBeurtenPerDag = Convert.ToInt32(reader["GroteServiceBeurtenPerDag"]);
                        int kleineServiceBeurtenPerDag = Convert.ToInt32(reader["KleineServiceBeurtenPerDag"]);
                        int groteSchoonmaakBeurtenPerDag = Convert.ToInt32(reader["GroteSchoonmaakBeurtenPerDag"]);
                        int kleineSchoonmaakBeurtenPerDag = Convert.ToInt32(reader["KleineSchoonmaakBeurtenPerDag"]);

                        remises.Add(new Remise(id,naam,groteServiceBeurtenPerDag,kleineServiceBeurtenPerDag,groteSchoonmaakBeurtenPerDag,kleineSchoonmaakBeurtenPerDag));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return remises;
        }

        public static List<Sector> LaadSectoren()
        {
            List<Sector> sectoren = new List<Sector>();
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                OracleCommand command = new OracleCommand("SELECT * FROM SECTOR");
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                OracleDataReader reader = command.ExecuteReader();

                if (!reader.HasRows) return sectoren;
                else
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        int spoorNummer = Convert.ToInt32(reader["Spoor_ID"]);//Spoor-ID != spoor-nummer
                        Tram tram = TramManager.tramViaId(Convert.ToInt32(reader["Tram_ID"]));
                        int nummer = Convert.ToInt32(reader["Nummer"]);
                        bool beschikbaar = convertBool(Convert.ToString(reader["Beschikbaar"]));
                        bool blokkade = convertBool(Convert.ToString(reader["Blokkade"]));

                        sectoren.Add(new Sector(id, spoorNummer, tram, nummer, beschikbaar, blokkade));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return sectoren;
        }

        public static List<Spoor> LaadSporen()
        {
            List<Spoor> sporen = new List<Spoor>();
            try
            {
                if (connection.State != ConnectionState.Open)
                {
                    connection.Open();
                }
                OracleCommand command = new OracleCommand("SELECT * FROM SPOOR");
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                OracleDataReader reader = command.ExecuteReader();

                if (!reader.HasRows) return sporen;
                else
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        Remise remise = RemiseManager.remiseViaId(Convert.ToInt32(reader["Remise_ID"]));
                        int nummer = Convert.ToInt32(reader["Nummer"]);
                        int lengte = Convert.ToInt32(reader["Lengte"]);
                        bool beschikbaar = convertBool(Convert.ToString(reader["Beschikbaar"]));
                        bool inUitrijSpoor = convertBool(Convert.ToString(reader["InUitRijspoor"]));

                        sporen.Add(new Spoor(id, remise, nummer, lengte, beschikbaar, inUitrijSpoor, RemiseManager.sectorenVanSpoor(id)));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return sporen;
        }

        public static List<Tram> LaadTrams()
        {
            List<Tram> trams = new List<Tram>();
            try
            {
                connection.Open();
                OracleCommand command = new OracleCommand("SELECT * FROM TRAM");
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                OracleDataReader reader = command.ExecuteReader();

                if (!reader.HasRows) return trams;
                else
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        Tramtype tramtype = (Tramtype) Convert.ToInt32(reader["Tramtype_ID"]) -1;
                        int nummer = Convert.ToInt32(reader["Nummer"]);
                        int lengte = Convert.ToInt32(reader["Lengte"]);
                        string status = Convert.ToString(reader["Status"]);
                        Remise remise = RemiseManager.remiseViaId(Convert.ToInt32(reader["Remise_ID_Standplaats"]));
                        bool vervuild = convertBool(Convert.ToString(reader["Vervuild"]));
                        bool defect = convertBool(Convert.ToString(reader["Defect"]));
                        bool conducteurGeschikt = convertBool(Convert.ToString(reader["ConducteurGeschikt"]));
                        bool beschikbaar = convertBool(Convert.ToString(reader["Beschikbaar"]));

                        trams.Add(new Tram(id, tramtype, nummer, lengte, status, remise, vervuild, defect, conducteurGeschikt, beschikbaar));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return trams;
        }

        public static List<Tramonderhoud> LaadTramonderhoud()
        {
            List<Tramonderhoud> onderhoudsBeurten = new List<Tramonderhoud>();
            try
            {
                connection.Open();
                OracleCommand command = new OracleCommand("SELECT * FROM TRAM_ONDERHOUD WHERE Voltooid = 0");
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                OracleDataReader reader = command.ExecuteReader();

                if (!reader.HasRows) return onderhoudsBeurten;
                else
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        Medewerker medewerker = RemiseManager.medewerkerViaId(Convert.ToInt32(reader["Medewerker_ID"]));
                        Tram tram = TramManager.tramViaId(Convert.ToInt32(reader["Tram_ID"]));
                        DateTime beschikbaarDatum = Convert.ToDateTime(reader["BeschikbaarDatum"]);
                        DateTime datumTijd = Convert.ToDateTime(reader["DatumTijdStip"]);
                        TypeOnderhoud typeOnderhoud = (TypeOnderhoud) Convert.ToInt32(reader["TypeOnderhoud"]) - 1;
                        string opmerking = Convert.ToString(reader["Notitie"]);

                        onderhoudsBeurten.Add(new Tramonderhoud(id, medewerker, tram, beschikbaarDatum, datumTijd, typeOnderhoud, opmerking));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return onderhoudsBeurten;
        }

        public static List<Medewerker> LaadMedewerkers()
        {
            List<Medewerker> medewerkers = new List<Medewerker>();
            try
            {
                connection.Open();
                OracleCommand command = new OracleCommand("SELECT * FROM MEDEWERKER");
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                OracleDataReader reader = command.ExecuteReader();

                if (!reader.HasRows) return medewerkers;
                else
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        Functie functie = (Functie) Convert.ToInt32(reader["Functie_ID"]) -1;
                        string naam = Convert.ToString(reader["Naam"]);

                        medewerkers.Add(new Medewerker(id, functie, naam));
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            return medewerkers;
        }

        public static void registreerSectorStatus(Sector sector)
        {
            try
            {
                connection.Open();

                OracleCommand command = new OracleCommand("UPDATE SECTOR SET Tram_ID = :tram_ID, Beschikbaar = :beschikbaar, Blokkade = :blokkade WHERE ID = :sectorid");
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                int tramId = 0;

                if (sector.Tram != null) tramId = sector.Tram.Id;

                command.Parameters.Add(":tram_ID", tramId);
                command.Parameters.Add(":beschikbaar", convertBool(sector.Beschikbaar));
                command.Parameters.Add(":blokkade", convertBool(sector.Blokkade));
                command.Parameters.Add(":sectorid", sector.Id);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        public static void registreerTramStatus(Tram tram)
        {
            try
            {
                connection.Open();

                OracleCommand command = new OracleCommand("UPDATE TRAM SET Status = :status, Beschikbaar = :beschikbaar WHERE ID = :tram_ID");
                command.CommandType = CommandType.Text;
                command.Connection = connection;
                command.Parameters.Add(":tram_ID", tram.Id);
                command.Parameters.Add(":status", tram.status);
                command.Parameters.Add(":beschikbaar", convertBool(tram.beschikbaar));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
        public static void registreerOnderhoud(Tramonderhoud onderhoud)
        {
            try
            {
                connection.Open();

                OracleCommand command = new OracleCommand("INSERT INTO TRAM_ONDERHOUD(Medewerker_ID, Tram_ID, DatumTijdStip, BeschikbaarDatum, TypeOnderhoud, Notitie)" +
                                                            "VALUES (:medewerker_ID, :tram_ID, :datumTijdstip, :datumBeschikbaar, :typeOnderhoud, :notitie)");
                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.Parameters.Add(":medewerker_ID", onderhoud.Medewerker.Id);
                command.Parameters.Add(":tram_ID", onderhoud.Tram.Id);
                command.Parameters.Add(":datumTijdstip", onderhoud.DatumTijdstip);
                command.Parameters.Add(":datumBeschikbaar", onderhoud.BeschikbaarDatum);
                command.Parameters.Add(":typeOnderhoud", (int)onderhoud.TypeOnderhoud +1);
                command.Parameters.Add(":notitie", onderhoud.Opmerking);

                command.ExecuteNonQuery();
            }
            catch (OracleException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool convertBool(string value)
        {
            if (value == "Y") return true;
            return false;
        }

        public static string convertBool(bool value)
        {
            if (value) return "Y";
            return "N";
        }

        public static void VoltooiOnderhoud(Tramonderhoud onderhoud)
        {
            try
            {
                connection.Open();

                OracleCommand command = new OracleCommand("UPDATE TRAM_ONDERHOUD SET Voltooid = 1 WHERE ID = :mID");

                command.CommandType = CommandType.Text;
                command.Connection = connection;

                command.Parameters.Add(":mID", onderhoud.Id);

                command.ExecuteNonQuery();
            }
            catch (OracleException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
