import 'dart:async';
import 'dart:io';
import 'dart:isolate';

const String _HOST = '0.0.0.0';
const String _GREET = 'Hello World';

_startServer(arg) async {
  print('Start server');
  final server = await HttpServer.bind(_HOST, 9292, shared: true);
  server.listen((HttpRequest request) {
      request.response
      ..write(_GREET)
      ..close();
  });
}

void main(List<String> args) {
  final threads = (args.length>0?int.parse(args[0]):Platform.numberOfProcessors);
  print('CPU: ${Platform.numberOfProcessors}, Threads: ${threads}');
  for (int i = 1; i < threads; i++)
    Isolate.spawn(_startServer, null);
  _startServer(null);
}
