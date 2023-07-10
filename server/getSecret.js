const path = require("path");
const fs = require("fs");

function getSecret(filename) {
    const filePath = path.join(__dirname, "secret", filename);
    const jsonData = fs.readFileSync(filePath, "utf-8");
    const data = JSON.parse(jsonData);
    return data;
}

module.exports = getSecret;
