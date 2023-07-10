import { getAll, getOneByName, insertOne, deleteOneByName, updateBestScoreByName } from './api'
const dbName = "RankDB";
const collectionName = "users";


const path = require('path');
const fs = require('fs');
const filePath = path.join(__dirname, 'secret', 'url.json');
const jsonData = fs.readFileSync(filePath, 'utf-8');
const data = JSON.parse(jsonData);
const server_url = data.db;

const express = require('express'); // express 임포트
const app = express(); // app생성
const port = 3030; // 일단은 localhost:3030에서 작업

const bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

// GET: /
app.get('/', function (req, res) {
  res.send('Rank Server');
});

// POST: /user/insert/score
app.post('/user/update/score', (req, res) => {
  const { username,  } = req.body;
  res.send(`Hello, ${username}! This is a POST request.`);
});

// POST: /user/insert/profilePic
app.post('/user/update/profilePic', (req, res) => {
  const { username,  } = req.body;
  res.send(`Hello, ${username}! This is a POST request.`);
});


// POST: /user/update/score
app.post('/user/update/score', (req, res) => {
  const { username,  } = req.body;
  res.send(`Hello, ${username}! This is a POST request.`);
});

// POST: /user/update/profilePic
app.post('/user/update/profilePic', (req, res) => {
  const { username,  } = req.body;
  res.send(`Hello, ${username}! This is a POST request.`);
});



app.listen(port, () => console.log(`${port}포트입니다.`));
