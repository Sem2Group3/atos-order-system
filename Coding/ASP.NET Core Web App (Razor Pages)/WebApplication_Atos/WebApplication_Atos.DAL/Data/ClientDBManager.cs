using MySqlConnector;
using System;
using System.Data;
using WebApplication_Atos.Core.Interfaces;
using WebApplication_Atos.Core.Models;

namespace WebApplication_Atos.Data;

public class ClientDBManager : IClientDBManager
{
    private readonly MySqlDataSource database;

    public ClientDBManager(MySqlDataSource database)
    {
        this.database = database;
    }

    public List<Client> GetClients()
    {
        var clients = new List<Client>();

        using (var connection = database.CreateConnection())
        {
            connection.Open();

            using (var command = new MySqlCommand("SELECT * FROM klant", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var client = new Client
                        {
                            Klantid = reader.GetInt32("Klantid"),
                            Naam = reader["Naam"] as string,
                            Adres = reader["Adres"] as string,
                            Woonplaats = reader["Woonplaats"] as string,
                            Postcode = reader["Postcode"] as string,
                            Telefoon = reader["Telefoon"] as string,
                            Fax = reader["Fax"] as string,
                            Email = reader["E-mail adres"] as string,
                            Contactpersoon = reader["Contactpersoon"] as string,
                            BTWnummer = reader["BTWnummer"] as string,
                            Notities = reader["Notities"] as string,
                            Debiteurnummer = reader["Debiteurnummer"] as int?,
                            FactuurNaam = reader["FactuurNaam"] as string,
                            FactuurAdres = reader["FactuurAdres"] as string,
                            FactuurWoonplaats = reader["FactuurWoonplaats"] as string,
                            FactuurPostcode = reader["FactuurPostcode"] as string,
                            FactuurContactpersoon = reader["FactuurContactpersoon"] as string,
                            NotitiesContactpersoon = reader["NotitiesContactpersoon"] as string,
                            GevondenIn = reader["GevondenIn"] as string,
                            FactuurEmail = reader["FactuurEmail"] as string,
                            Geblokkeerd = reader["Geblokkeerd"] as string

                        };

                        clients.Add(client);
                    }
                }
            }
            connection.Close();
        }

        return clients;
    }

    public Client? GetClientFromID(int clientID)
    {
        using (var connection = database.CreateConnection())
        {
            connection.Open();

            var query = "SELECT * FROM klant WHERE Klantid = @Klantid";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Klantid", clientID);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Client
                        {
                            Klantid = reader.GetInt32("Klantid"),
                            Naam = reader["Naam"] as string,
                            Adres = reader["Adres"] as string,
                            Woonplaats = reader["Woonplaats"] as string,
                            Postcode = reader["Postcode"] as string,
                            Telefoon = reader["Telefoon"] as string,
                            Fax = reader["Fax"] as string,
                            Email = reader["E-mail adres"] as string,
                            Contactpersoon = reader["Contactpersoon"] as string,
                            BTWnummer = reader["BTWnummer"] as string,
                            Notities = reader["Notities"] as string,
                            Debiteurnummer = reader["Debiteurnummer"] as int?,
                            FactuurNaam = reader["FactuurNaam"] as string,
                            FactuurAdres = reader["FactuurAdres"] as string,
                            FactuurWoonplaats = reader["FactuurWoonplaats"] as string,
                            FactuurPostcode = reader["FactuurPostcode"] as string,
                            FactuurContactpersoon = reader["FactuurContactpersoon"] as string,
                            NotitiesContactpersoon = reader["NotitiesContactpersoon"] as string,
                            GevondenIn = reader["GevondenIn"] as string,
                            FactuurEmail = reader["FactuurEmail"] as string,
                            Geblokkeerd = reader["Geblokkeerd"] as string
                        };
                    }
                }
            }

            connection.Close();
        }

        return null;
    }


    public async Task<List<Client>> GetClientsAsync()
    {
        var clients = new List<Client>();

        using (var connection = database.CreateConnection())
        {
            await connection.OpenAsync().ConfigureAwait(false);

            var query = @"SELECT 
                Klantid, Naam, Adres, Woonplaats, Postcode, Telefoon, Fax, `E-mail adres`,
                Contactpersoon, BTWnummer, Notities, Debiteurnummer, FactuurNaam, FactuurAdres,
                FactuurWoonplaats, FactuurPostcode, FactuurContactpersoon, NotitiesContactpersoon,
                GevondenIn, FactuurEmail, Geblokkeerd
                FROM klant";

            using (var command = new MySqlCommand(query, connection))
            {
                try { command.Prepare(); } catch { /* ignore */ }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    int oKlantid = reader.GetOrdinal("Klantid");
                    int oNaam = reader.GetOrdinal("Naam");
                    int oAdres = reader.GetOrdinal("Adres");
                    int oWoonplaats = reader.GetOrdinal("Woonplaats");
                    int oPostcode = reader.GetOrdinal("Postcode");
                    int oTelefoon = reader.GetOrdinal("Telefoon");
                    int oFax = reader.GetOrdinal("Fax");
                    int oEmail = reader.GetOrdinal("E-mail adres");
                    int oContactpersoon = reader.GetOrdinal("Contactpersoon");
                    int oBTWnummer = reader.GetOrdinal("BTWnummer");
                    int oNotities = reader.GetOrdinal("Notities");
                    int oDebiteurnummer = reader.GetOrdinal("Debiteurnummer");
                    int oFactuurNaam = reader.GetOrdinal("FactuurNaam");
                    int oFactuurAdres = reader.GetOrdinal("FactuurAdres");
                    int oFactuurWoonplaats = reader.GetOrdinal("FactuurWoonplaats");
                    int oFactuurPostcode = reader.GetOrdinal("FactuurPostcode");
                    int oFactuurContactpersoon = reader.GetOrdinal("FactuurContactpersoon");
                    int oNotitiesContactpersoon = reader.GetOrdinal("NotitiesContactpersoon");
                    int oGevondenIn = reader.GetOrdinal("GevondenIn");
                    int oFactuurEmail = reader.GetOrdinal("FactuurEmail");
                    int oGeblokkeerd = reader.GetOrdinal("Geblokkeerd");

                    while (await reader.ReadAsync().ConfigureAwait(false))
                    {
                        var client = new Client
                        {
                            Klantid = reader.IsDBNull(oKlantid) ? 0 : reader.GetInt32(oKlantid),
                            Naam = reader.IsDBNull(oNaam) ? null : reader.GetString(oNaam),
                            Adres = reader.IsDBNull(oAdres) ? null : reader.GetString(oAdres),
                            Woonplaats = reader.IsDBNull(oWoonplaats) ? null : reader.GetString(oWoonplaats),
                            Postcode = reader.IsDBNull(oPostcode) ? null : reader.GetString(oPostcode),
                            Telefoon = reader.IsDBNull(oTelefoon) ? null : reader.GetString(oTelefoon),
                            Fax = reader.IsDBNull(oFax) ? null : reader.GetString(oFax),
                            Email = reader.IsDBNull(oEmail) ? null : reader.GetString(oEmail),
                            Contactpersoon = reader.IsDBNull(oContactpersoon) ? null : reader.GetString(oContactpersoon),
                            BTWnummer = reader.IsDBNull(oBTWnummer) ? null : reader.GetString(oBTWnummer),
                            Notities = reader.IsDBNull(oNotities) ? null : reader.GetString(oNotities),
                            Debiteurnummer = reader.IsDBNull(oDebiteurnummer) ? null : reader.GetInt32(oDebiteurnummer),
                            FactuurNaam = reader.IsDBNull(oFactuurNaam) ? null : reader.GetString(oFactuurNaam),
                            FactuurAdres = reader.IsDBNull(oFactuurAdres) ? null : reader.GetString(oFactuurAdres),
                            FactuurWoonplaats = reader.IsDBNull(oFactuurWoonplaats) ? null : reader.GetString(oFactuurWoonplaats),
                            FactuurPostcode = reader.IsDBNull(oFactuurPostcode) ? null : reader.GetString(oFactuurPostcode),
                            FactuurContactpersoon = reader.IsDBNull(oFactuurContactpersoon) ? null : reader.GetString(oFactuurContactpersoon),
                            NotitiesContactpersoon = reader.IsDBNull(oNotitiesContactpersoon) ? null : reader.GetString(oNotitiesContactpersoon),
                            GevondenIn = reader.IsDBNull(oGevondenIn) ? null : reader.GetString(oGevondenIn),
                            FactuurEmail = reader.IsDBNull(oFactuurEmail) ? null : reader.GetString(oFactuurEmail),
                            Geblokkeerd = reader.IsDBNull(oGeblokkeerd) ? null : reader.GetString(oGeblokkeerd)
                        };

                        clients.Add(client);
                    }
                }
            }
        }

        return clients;
    }

    public void AddClient(Client newClient)
    {
        using (var connection = database.CreateConnection())
        {
            connection.Open();

            var query = @"INSERT INTO klant 
        (Naam, Adres, Woonplaats, Postcode, Telefoon, Fax, `E-mail adres`, Contactpersoon, BTWnummer, Notities, Debiteurnummer, FactuurNaam, FactuurAdres, FactuurWoonplaats, FactuurPostcode, FactuurContactpersoon, NotitiesContactpersoon, GevondenIn, FactuurEmail, Geblokkeerd)
        VALUES 
        (@Naam, @Adres, @Woonplaats, @Postcode, @Telefoon, @Fax, @Email, @Contactpersoon, @BTWnummer, @Notities, @Debiteurnummer, @FactuurNaam, @FactuurAdres, @FactuurWoonplaats, @FactuurPostcode, @FactuurContactpersoon, @NotitiesContactpersoon, @GevondenIn, @FactuurEmail, @Geblokkeerd);";

            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Naam", newClient.Naam);
                command.Parameters.AddWithValue("@Adres", newClient.Adres);
                command.Parameters.AddWithValue("@Woonplaats", newClient.Woonplaats);
                command.Parameters.AddWithValue("@Postcode", newClient.Postcode);
                command.Parameters.AddWithValue("@Telefoon", newClient.Telefoon);
                command.Parameters.AddWithValue("@Fax", newClient.Fax);
                command.Parameters.AddWithValue("@Email", newClient.Email);
                command.Parameters.AddWithValue("@Contactpersoon", newClient.Contactpersoon);
                command.Parameters.AddWithValue("@BTWnummer", newClient.BTWnummer);
                command.Parameters.AddWithValue("@Notities", newClient.Notities);
                command.Parameters.AddWithValue("@Debiteurnummer", newClient.Debiteurnummer);
                command.Parameters.AddWithValue("@FactuurNaam", newClient.FactuurNaam);
                command.Parameters.AddWithValue("@FactuurAdres", newClient.FactuurAdres);
                command.Parameters.AddWithValue("@FactuurWoonplaats", newClient.FactuurWoonplaats);
                command.Parameters.AddWithValue("@FactuurPostcode", newClient.FactuurPostcode);
                command.Parameters.AddWithValue("@FactuurContactpersoon", newClient.FactuurContactpersoon);
                command.Parameters.AddWithValue("@NotitiesContactpersoon", newClient.NotitiesContactpersoon);
                command.Parameters.AddWithValue("@GevondenIn", newClient.GevondenIn);
                command.Parameters.AddWithValue("@FactuurEmail", newClient.FactuurEmail);
                command.Parameters.AddWithValue("@Geblokkeerd", newClient.Geblokkeerd);

                command.ExecuteNonQuery();
            }

            connection.Close();
        }
    }
    public void UpdateClient(Client client)
{
    using (var connection = database.CreateConnection())
    {
        connection.Open();

        var query = @"UPDATE klant SET 
                        Naam = @Naam,
                        Adres = @Adres,
                        Woonplaats = @Woonplaats,
                        Postcode = @Postcode,
                        Telefoon = @Telefoon,
                        Fax = @Fax,
                        `E-mail adres` = @Email,
                        Contactpersoon = @Contactpersoon,
                        BTWnummer = @BTWnummer,
                        Notities = @Notities,
                        FactuurNaam = @FactuurNaam,
                        FactuurAdres = @FactuurAdres,
                        FactuurWoonplaats = @FactuurWoonplaats,
                        FactuurPostcode = @FactuurPostcode,
                        FactuurContactpersoon = @FactuurContactpersoon,
                        NotitiesContactpersoon = @NotitiesContactpersoon,
                        GevondenIn = @GevondenIn,
                        FactuurEmail = @FactuurEmail,
                        Geblokkeerd = @Geblokkeerd
                    WHERE Klantid = @Klantid";

        using (var command = new MySqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@Naam", client.Naam);
            command.Parameters.AddWithValue("@Adres", client.Adres);
            command.Parameters.AddWithValue("@Woonplaats", client.Woonplaats);
            command.Parameters.AddWithValue("@Postcode", client.Postcode);
            command.Parameters.AddWithValue("@Telefoon", client.Telefoon);
            command.Parameters.AddWithValue("@Fax", client.Fax);
            command.Parameters.AddWithValue("@Email", client.Email);
            command.Parameters.AddWithValue("@Contactpersoon", client.Contactpersoon);
            command.Parameters.AddWithValue("@BTWnummer", client.BTWnummer);
            command.Parameters.AddWithValue("@Notities", client.Notities);
            command.Parameters.AddWithValue("@FactuurNaam", client.FactuurNaam);
            command.Parameters.AddWithValue("@FactuurAdres", client.FactuurAdres);
            command.Parameters.AddWithValue("@FactuurWoonplaats", client.FactuurWoonplaats);
            command.Parameters.AddWithValue("@FactuurPostcode", client.FactuurPostcode);
            command.Parameters.AddWithValue("@FactuurContactpersoon", client.FactuurContactpersoon);
            command.Parameters.AddWithValue("@NotitiesContactpersoon", client.NotitiesContactpersoon);
            command.Parameters.AddWithValue("@GevondenIn", client.GevondenIn);
            command.Parameters.AddWithValue("@FactuurEmail", client.FactuurEmail);
            command.Parameters.AddWithValue("@Geblokkeerd", client.Geblokkeerd);
            command.Parameters.AddWithValue("@Klantid", client.Klantid);

            command.ExecuteNonQuery();
        }

        connection.Close();
    }
}


}
