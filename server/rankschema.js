const MongoClient = require('mongodb').MongoClient;

// Connection URL and database name
const url = '///';
const dbName = 'Ranking Server';

// Collection names
const usersCollection = 'Users';
const rankingsCollection = 'Rankings';

// Function to connect to the MongoDB server
async function connectToMongoDB() {
  try {
    // Connect to the MongoDB server
    const client = await MongoClient.connect(url, { useUnifiedTopology: true });
    console.log('Connected to MongoDB server');

    // Select the database
    const db = client.db(dbName);

    // Access the Users collection
    const users = db.collection(usersCollection);

    // Access the Rankings collection
    const rankings = db.collection(rankingsCollection);

    // Perform database operations...

    // Close the connection
    client.close();
    console.log('Disconnected from MongoDB server');
  } catch (err) {
    console.error('Error connecting to MongoDB:', err);
  }
}

// Example usage
connectToMongoDB();