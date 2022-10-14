using System;
using Npgsql;
using System.Diagnostics;

public class DataConfigure
{
    // Obtain connection string information from the portal
    //
    private static string Host = "35.243.89.97";
    private static string User = "postgres";
    private static string DBname = "postgres";
    private static string Password = "Thangchiba123#";
    private static string Port = "5432";

    public static void ConnectToDatabase()
    {
        // Build connection string using parameters from portal
        //
        string connString =
            String.Format(
                "Host={0};Username={1};Database={2};Port={3};Password={4};",
                Host,
                User,
                DBname,
                Port,
                Password);

        try
        {
            var conn = new NpgsqlConnection(connString);
            conn.Open();
        }
        catch (Exception e)
        {
            Console.WriteLine("Loi ket noi db");
            Console.WriteLine(e.ToString());
        }
        //using (var conn = new NpgsqlConnection(connString))

        //{
        //    Console.Out.WriteLine("Opening connection");
        //    conn.Open();

        //    using (var command = new NpgsqlCommand("DROP TABLE IF EXISTS inventory", conn))
        //    {
        //        command.ExecuteNonQuery();
        //        Console.Out.WriteLine("Finished dropping table (if existed)");

        //    }

        //    using (var command = new NpgsqlCommand("CREATE TABLE inventory(id serial PRIMARY KEY, name VARCHAR(50), quantity INTEGER)", conn))
        //    {
        //        command.ExecuteNonQuery();
        //        Console.Out.WriteLine("Finished creating table");
        //    }

        //    using (var command = new NpgsqlCommand("INSERT INTO inventory (name, quantity) VALUES (@n1, @q1), (@n2, @q2), (@n3, @q3)", conn))
        //    {
        //        command.Parameters.AddWithValue("n1", "banana");
        //        command.Parameters.AddWithValue("q1", 150);
        //        command.Parameters.AddWithValue("n2", "orange");
        //        command.Parameters.AddWithValue("q2", 154);
        //        command.Parameters.AddWithValue("n3", "apple");
        //        command.Parameters.AddWithValue("q3", 100);

        //        int nRows = command.ExecuteNonQuery();
        //        Console.Out.WriteLine(String.Format("Number of rows inserted={0}", nRows));
        //    }
        //}

        Console.WriteLine("Connected To Database");
        Console.ReadLine();
    }
}

