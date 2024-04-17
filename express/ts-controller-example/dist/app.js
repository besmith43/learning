"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
// const express = require('express');
const express_1 = __importDefault(require("express"));
const app = (0, express_1.default)();
const port = 8000;
// const fs = require('fs');
const fs_1 = __importDefault(require("fs"));
/* backslash for windows, in unix it would be forward slash */
// const routes_directory = require('path').resolve(__dirname) + '\\routes\\'; 
const path_1 = __importDefault(require("path"));
// const routes_directory = path.resolve(__dirname) + '\\routes\\';
const routes_directory = path_1.default.resolve(__dirname) + '/routes/';
fs_1.default.readdirSync(routes_directory).forEach((route_file) => {
    try {
        app.use('/', require(routes_directory + route_file)());
    }
    catch (error) {
        console.log(`Encountered Error initializing routes from ${route_file}`);
        console.log(error);
    }
});
app.listen(port, () => {
    console.log(`My app listening at http://localhost:${port}`);
});
