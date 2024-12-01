const express = require("express");
const bcrypt = require("bcrypt");
const bodyParser = require("body-parser");
const path = require("path");
const expressStaticGzip = require("express-static-gzip");
require("dotenv").config();

const { database } = require("./databaseConnection");
const createDatabaseAndTables = require("./initializeDatabase");

const app = express();
const port = 3000 || process.env.PORT;

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true }));

// Ensure that WebAssembly files and Brotli files have the correct content type
app.use((req, res, next) => {
  if (req.url.endsWith(".wasm")) {
    res.setHeader("Content-Type", "application/wasm"); // For .wasm files
  }
  if (req.url.endsWith(".wasm.br")) {
    res.setHeader("Content-Type", "application/wasm"); // For Brotli compressed .wasm files
    res.setHeader("Content-Encoding", "br"); // Ensure Brotli is specified
  }
  next();
});

// Serve static files with compression support
app.use(
  expressStaticGzip(path.join(__dirname, "build"), {
    enableBrotli: true, // Enable Brotli compression support
    orderPreference: ["br", "gz"], // Prefer Brotli compression if available
    customCompressions: [
      {
        encodingName: "gzip",
        fileExtension: "gz", // Apply gzip compression to .gz files
      },
      {
        encodingName: "br",
        fileExtension: "br", // Apply Brotli compression to .br files
      },
    ],
    setHeaders: function (res, path) {
      // Add Content-Encoding: br for .br files
      if (path.endsWith(".br")) {
        res.set("Content-Encoding", "br");
      }
      // Add Content-Encoding: gzip for .gz files
      if (path.endsWith(".gz")) {
        res.set("Content-Encoding", "gzip");
      }
    },
  })
);

// Root route to serve the main HTML file
app.get("/", (req, res) => {
  res.sendFile(path.join(__dirname, "build", "index.html"));
});

// Improved database initialization
async function startServer() {
  let connection;
  try {
    connection = await database.getConnection();
    await connection.query("SELECT 1");
    console.log("Database connection successful!");

    // 데이터베이스 초기화
    await createDatabaseAndTables();
    console.log("Database and tables are ready.");

    // 서버 시작
    app.listen(port, () => {
      console.log(`Server is running on http://localhost:${port}`);
    });
  } catch (err) {
    console.error("Error initializing the database:", err.message);
    process.exit(1);
  } finally {
    if (connection) {
      connection.release();
    }
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
