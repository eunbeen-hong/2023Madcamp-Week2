// import { getAll, getOneByName, insertOne, deleteOneByName, updateBestScoreByName } from './api.js'
const api = require("./api.js")
const dbName = "RankDB";
const collectionName = "users";

const data = getSecret("client_secret.json");
const server_url = data.db;

const express = require("express"); // express 임포트
const app = express(); // app생성
const port = 3030; // 일단은 localhost:3030에서 작업

const bodyParser = require("body-parser");
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

// GET: /
app.get('/', async function (req, res) {
  let a = await api.getAll(dbName, collectionName);
  res.send(a);
});

// GET: /user/score
app.get('/user/score', async function (req, res) {
  const { username } = req.body;
  var a = await api.getOneByName(username, dbName, collectionName);
  res.send(JSON.parse(a).bestScore.toString());
});

// POST: /user/insert
app.post('/user/insert', (req, res) => {
  const { username, profilePic, bestScore } = req.body;
  api.insertOne(username, profilePic, parseInt(bestScore), dbName, collectionName);
  res.send(`INSERT USER: ${username}`);
  console.log('\n');
});

// POST: /user/update/score
app.post('/user/update/score', (req, res) => {
  const { username, bestScore } = req.body;
  api.updateBestScoreByName(username, parseInt(bestScore), dbName, collectionName);
  res.send(`UPDATE SCORE: ${username} -> ${parseInt(bestScore)}`);
  console.log('\n');
});


// POST: /user/update/profilePic
// app.post('/user/update/profilePic', (req, res) => {
//   const { username, profilePic } = req.body;
//   // TODO
// console.log('\n');
// });

app.listen(port, () => console.log(`${port}포트입니다.`));
