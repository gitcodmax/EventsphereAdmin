const express = require('express');
const sql = require('mssql');
const cors = require('cors');

const app = express();
app.use(cors());

const config = {
    user: 'Ehavi',
    password: 'HaviEventsphere',
    server: 'localhost',
    database: "Eventsphere",
    options: {
        encrypt: false, // Set to false for local SQL Server
        trustServerCertificate: true,
        trustedConnection: false,
        enableArithAbort: true,
        instanceName: 'SQLEXPRESS' // Fixed camel case
    },
    port: 1433
  
        //connectionString: "Server=localhost\\SQLEXPRESS,1433;Database=Eventsphere;User Id=Ehavi;Password=HaviEventsphere;Encrypt=false;TrustServerCertificate=true;"
   

}

// Endpoint to delete an event
app.delete("/DeleteEvent", async (req, res) => {
    let { eventid } = req.query;
    if (!eventid) {
        return res.status(400).json({ error: "EventID is required" });
    }

    eventid = parseInt(eventid, 10);
    if (isNaN(eventid)) {
        return res.status(400).json({ error: "Invalid EventID format" });
    }

    try {
        const pool = await sql.connect(config);
        const result = await pool.request()
            .input("EventID", sql.Int, eventid)
            .query("DELETE FROM Events WHERE EventID = @EventID");

        if (result.rowsAffected[0] > 0) {
            res.json({ message: "Event deleted successfully." });
        } else {
            res.status(404).json({ error: "Event not found." });
        }
    } catch (err) {
        console.error(err);
        res.status(500).json({ error: "Internal server error." });
    }
});

// Endpoint to Get Events data
app.get("/GetEvents", async (req, res) => {
    try {
        const pool = await sql.connect(config);
        const data = await pool.request().query('SELECT * FROM Events;');
        res.json(data.recordset);
    } catch (err) {
        console.log(err);
        res.status(500).json({ error: "Internal server error." });
    }
});

// Endpoint to delete a user
app.delete("/DeleteUser", async (req, res) => {
    const { username } = req.query;
    if (!username) {
        return res.status(400).json({ error: "Username is required" });
    }

    try {
        const pool = await sql.connect(config);
        const result = await pool.request()
            .input("Username", sql.VarChar, username)
            .query("DELETE FROM Users WHERE Username = @Username");

        if (result.rowsAffected[0] > 0) {
            res.json({ message: "User deleted successfully." });
        } else {
            res.status(404).json({ error: "User not found." });
        }
    } catch (err) {
        console.error(err);
        res.status(500).json({ error: "Internal server error." });
    }
});

// Endpoint to display users to the Admin 
app.get('/GetUsers', async (req, res) => {
    try {
        const pool = await sql.connect(config);
        const data = await pool.request().query('SELECT UserName, Email, PasswordHash FROM [dbo].[AspNetUsers];');
        res.json(data.recordset);
    } catch (err) {
        console.log(err);
        res.status(500).json({ error: "Internal server error." });
    }
});

app.get('/', (req, res) => {
    return res.json("This is EventSphere Admin Backend");
});

app.listen(3000, () => {
    console.log("The server has started.");
});
