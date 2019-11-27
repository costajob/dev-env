const cluster = require('cluster');
const http = require('http');
const numCPUs = require('os').cpus().length;
const threads = process.argv.length>2?parseInt(process.argv[2]):numCPUs;
console.log('CPU: ',numCPUs, 'threads: ',threads);
if (cluster.isMaster) {
  for (let i = 0; i < threads; i++) {
    cluster.fork();
  }
} else {
  console.log('Start server');
  http.createServer((req, res) => {
    res.writeHead(200);
    res.end('Hello World');
  }).listen(9292);
}
