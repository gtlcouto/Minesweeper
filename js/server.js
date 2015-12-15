// server.js

// BASE SETUP
// =============================================================================

// call the packages we need
var express    = require('express');        // call express
var app        = express();                 // define our app using express
var bodyParser = require('body-parser');
var redis = require('node-redis');
var client = redis.createClient();
client.on('connect',function () {
	console.log('connected'	)
});


// configure app to use bodyParser()
// this will let us get the data from a POST
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());

var port = process.env.PORT || 8080;        // set our port

// ROUTES FOR OUR API
// =============================================================================
var router = express.Router();              // get an instance of the express Router

// middleware to use for all requests
router.use(function(req, res, next) {
    // do logging
    console.log('Something is happening.');
    next(); // make sure we go to the next routes and don't stop here
});

// test route to make sure everything is working (accessed at GET http://localhost:8080/api)
router.get('/', function(req, res) {
    res.json({ message: 'hooray! welcome to our api!' });   
});
router.route('/scores')
	
	.get(function(req, res) {
		client.zrevrangebyscore(['leaderboard', '+inf', '-inf', 'WITHSCORES', 'LIMIT', '0', '10'], function(err, reply){
	console.log(reply);
	res.json(reply);

	});
		
	}); //get
	
	
router.route('/scores/:facebook_id')
	
	.get(function(req, res) {
		client.zscore(['leaderboard', req.params.facebook_id], function(err, reply){
	console.log(reply);
	res.json(reply);
	});
		
	}); //get

router.route('/scores/:score/:facebook_id/:secret')

    // create a bear (accessed at POST http://localhost:8080/api/bears)
    .post(function(req, res) {
        
    client.zadd(['leaderboard', req.params.score, req.params.facebook_id], function(err, reply){
	console.log(reply);
	if(reply == 1)
	{
		res.json(1);
		console.log('entry created');	
	}else {
		res.json(0);
		console.log('entry failed');
	}
	});
        
    });

// more routes for our API will happen here

// REGISTER OUR ROUTES -------------------------------
// all of our routes will be prefixed with /api
app.use('/api', router);

// START THE SERVER
// =============================================================================
app.listen(port);
console.log('Magic happens on port ' + port);