require "http/server"

NUM_OF_WORKERS = 8

server = HTTP::Server.new("0.0.0.0", 9292) do |context|
  context.response.content_type = "text/plain"
  context.response.print "Hello World"
end

server.bind

workers = [] of Process

NUM_OF_WORKERS.times do
  worker = fork do
    server.listen
  end

  workers << worker
end

workers.each do |worker|
  worker.wait
end
