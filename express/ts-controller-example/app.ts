// const express = require('express');
import express, { Request, Response } from 'express';
const app = express();
const port = 8000;


// const fs = require('fs');
import fs from 'fs';
/* backslash for windows, in unix it would be forward slash */
// const routes_directory = require('path').resolve(__dirname) + '\\routes\\'; 
import path from 'path';
// const routes_directory = path.resolve(__dirname) + '\\routes\\';
const routes_directory = path.resolve(__dirname) + '/routes/';

fs.readdirSync(routes_directory).forEach((route_file: string) => {
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