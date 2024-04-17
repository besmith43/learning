const express = require('express');
const app = express();
const port = 8000;

/*
const userRouter = require('./routes/user.js')();
const orderRouter = require('./routes/order.js')();

app.use(userRouter);
app.use(orderRouter);
*/

const fs = require('fs');
/* backslash for windows, in unix it would be forward slash */
// const routes_directory = require('path').resolve(__dirname) + '\\routes\\'; 
const routes_directory = require('path').resolve(__dirname) + '/routes/'; 

fs.readdirSync(routes_directory).forEach(route_file => {
  try {
    app.use('/', require(routes_directory + route_file)());
  } catch (error) {
    console.log(`Encountered Error initializing routes from ${route_file}`);
    console.log(error);
  }
});

app.listen(port, () => {
  console.log(`My app listening at http://localhost:${port}`);
});