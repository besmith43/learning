// const router = require('express').Router();
import express, { Request, Response } from 'express';
const router = express.Router();

module.exports = function () {
router.get('/user/:id', (request: Request, response: Response) => {
 response.send('Hello World From User Get ' + request.params.id);
});
router.post('/user/:id', function(request: Request, response: Response) {
 response.send('Hello World From User Post ' + request.params.id);
});
return router;
}