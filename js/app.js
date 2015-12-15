var redis = require('node-redis');
var client = redis.createClient();
client.on('connect',function () {
	console.log('connected'	)
});

client.zrangebyscore(['test', '0', '40', 'WITHSCORES'], function(err, reply){
	console.log(reply);
});