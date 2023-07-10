const express = require('express'); // express 임포트

const path = require('path');
const fs = require('fs');
const filePath = path.join(__dirname, 'secret', 'url.json');
const jsonData = fs.readFileSync(filePath, 'utf-8');
const data = JSON.parse(jsonData);
const server_url = data.db;

const app = express(); // app생성
const port = 3030;

const bodyParser = require('body-parser');
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());


app.get('/', function (req, res) {
  res.send('hello world!!');
});

app.post('/post', (req, res) => {
  const { name } = req.body;
  res.send(`Hello, ${name}! This is a POST request.`);
});

app.listen(port, () => console.log(`${port}포트입니다.`));
