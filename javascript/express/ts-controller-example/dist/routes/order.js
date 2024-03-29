"use strict";
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
// const router = require('express').Router();
const express_1 = __importDefault(require("express"));
const router = express_1.default.Router();
module.exports = function () {
    router.get('/order/:id', (request, response) => {
        response.send('Hello World From Order Get ' + request.params.id);
    });
    router.post('/order/:id', function (request, response) {
        response.send('Hello World From Order Post ' + request.params.id);
    });
    return router;
};
