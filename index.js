const express = require("express");
const bcrypt = require("bcrypt");
const bodyParser = require("body-parser");
const path = require("path");
require("dotenv").config();

const { database } = require("./databaseConnection");
const createDatabaseAndTables = require("./initializeDatabase");

const app = express();
const port = 3000;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

app.use(express.static(path.join(__dirname, "build")));

// Root route to serve the main HTML file
app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "build", "index.html"));
});

// Improved database initialization
async function startServer() {
  try {
    // Initialize the database and tables before starting the server
    await createDatabaseAndTables();
    console.log("Database and tables are ready.");

    // Start the server after the database is ready
    app.listen(port, () => {
      console.log(`Server is running on http://localhost:${port}`);
    });
  } catch (err) {
    console.error("Error initializing the database:", err.message);
    process.exit(1); // Exit if database initialization fails
  }
}

startServer();

// Register endpoint
app.post("/register", async (req, res) => {
  const { email, username, password } = req.body;

  if (!email || !username || !password) {
    return res.status(400).json({ error: "All fields are required" });
  }

  try {
    const [existingUsers] = await database.query(
      "SELECT 1 FROM users WHERE email = ? OR username = ?",
      [email, username]
    );

    if (existingUsers.length > 0) {
      return res
        .status(400)
        .json({ error: "Email or username already exists" });
    }

    const salt = await bcrypt.genSalt(10);
    const hashedPassword = await bcrypt.hash(password, salt);

    await database.query(
      "INSERT INTO users (email, username, password) VALUES (?, ?, ?)",
      [email, username, hashedPassword]
    );

    res.status(201).json({ message: "User registered successfully" });
  } catch (err) {
    console.error("Error during registration:", err.message);
    res.status(500).json({ error: "Failed to register user" });
  }
});

// Login endpoint
app.post("/login", async (req, res) => {
  const { username, password } = req.body;

  if (!username || !password) {
    return res
      .status(400)
      .json({ error: "Username and password are required." });
  }

  try {
    const [user] = await database.query(
      "SELECT * FROM users WHERE username = ?",
      [username]
    );

    if (user.length === 0) {
      return res.status(401).json({ error: "Invalid username or password." });
    }

    const isPasswordValid = await bcrypt.compare(password, user[0].password);

    if (!isPasswordValid) {
      return res.status(401).json({ error: "Invalid username or password." });
    }

    res.status(200).json({ message: "Login successful!" });
  } catch (err) {
    console.error("Error during login:", err.message);
    res.status(500).json({ error: "Internal server error." });
  }
});
