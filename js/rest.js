
function REST_ROUTER(router,client,md5) {

    var self = this;
    self.handleRoutes(router,client,md5);
}

REST_ROUTER.prototype.handleRoutes= function(router,client,md5) {
	console.log('rest');
    router.get("/",function(req,res){
        res.json({"Message" : "Hello World !"});
    })
}
module.exports = REST_ROUTER;