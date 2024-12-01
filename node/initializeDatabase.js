const { database, dbConfig } = require("./databaseConnection");

async function createDatabaseAndTables() {
  try {
    const connection = await database.getConnection();

    await connection.query(
      `CREATE DATABASE IF NOT EXISTS \`${dbConfig.database}\``
    );
    await connection.query(`USE \`${dbConfig.database}\``);

    await connection.query(`
      CREATE TABLE IF NOT EXISTS users (
        id INT AUTO_INCREMENT PRIMARY KEY,
        email VARCHAR(100) NOT NULL UNIQUE,
        username VARCHAR(50) NOT NULL UNIQUE,
        password VARCHAR(255) NOT NULL,
        created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
        updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
      )
    `);

    connection.release();
    console.log("Database and tables are ready.");
  } catch (err) {
    console.error("Error creating database and tables:", err.message);
    throw err;
  }
}

module.exports = createDatabaseAndTables;
