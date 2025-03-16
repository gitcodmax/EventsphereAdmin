const sql = require('mssql');

const config = {
    user: 'Ehavi',
    password: 'HaviEventsphere',
    server: 'localhost', // OR '127.0.0.1'
    database: "Eventsphere",
    options: {
        encrypt: false,
        trustServerCertificate: true,
    },
    port: 1433
};

async function testConnection() {
    try {
        console.log("Connecting to SQL Server...");
        let pool = await sql.connect(config);
        console.log("✅ Connected successfully!");
        let result = await pool.request().query("SELECT 1 AS test");
        console.log("Test Query Result:", result.recordset);
    } catch (err) {
        console.error("❌ Connection failed:", err);
    } finally {
        sql.close();
    }
}

testConnection();
